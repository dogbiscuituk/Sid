namespace ToyGraf.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ToyGraf.Expressions;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Interfaces;
    using ToyGraf.Models.Structs;

    [Serializable]
    public class Trace : Style, ITrace
    {
        public Trace() : base() => RestoreDefaults();
        public Trace(Graph graph) : this() => _stepCount = graph.StepCount;

        public new Trace Clone()
        {
            var trace = new Trace();
            trace.CopyFrom(this);
            trace.PointLists = PointLists;
            return trace;
        }

        #region Visual Properties

        private Interpolation _interpolation;
        public Interpolation Interpolation
        {
            get => _interpolation;
            set
            {
                if (Interpolation != value)
                {
                    _interpolation = value;
                    OnPropertyChanged("Interpolation");
                }
            }
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set
            {
                if (Selected != value)
                {
                    _selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        [NonSerialized]
        public Viewport Viewport;

        private bool _visible;
        [DefaultValue(true)]
        public bool Visible
        {
            get => _visible;
            set
            {
                if (Visible != value)
                {
                    _visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        [NonSerialized] private PlotType LastPlotType = (PlotType)(-1);
        [NonSerialized] private DomainInfo LastDomainInfo;
        [NonSerialized] private double LastTime = -1;

        #endregion

        #region Formula, Expression, Proxy, Func, Derivative

        private string _formula = string.Empty;
        /// <summary>
        /// Plain text version of the algebraic expression used by the Trace.
        /// </summary>
        [DefaultValue("")]
        public string Formula
        {
            get => _formula;
            set
            {
                if (Formula != value)
                {
                    SetFormula(value);
                    OnPropertyChanged("Formula");
                }
            }
        }

        /// <summary>
        /// Provided to avoid calling the virtual OnPropertyChanged() from the constructor.
        /// </summary>
        /// <param name="formula">Plain text version of the algebraic expression used by the Trace.</param>
        private void SetFormula(string formula)
        {
            Expression = new Parser().Parse(formula);
            SetFunc(Expression);
            _formula = formula;
        }

        /// <summary>
        /// A tokenised representation of the algebraic expression used by the Trace.
        /// This is obtained by sending the Formula to the Parser.Parse() method.
        /// </summary>
        [JsonIgnore]
        public Expression Expression { get; private set; }

        [NonSerialized]
        private Expression _proxy;

        /// <summary>
        /// The System.Linq.Expressions representation of the algebraic expression used by the
        /// Trace. This is obtained from the Expression property by replacing all "xref" tokens
        /// with calls to other Traces in the Graph.
        /// </summary>
        [JsonIgnore]
        public Expression Proxy
        {
            get => _proxy ?? Expression;
            set
            {
                _proxy = value;
                if (_proxy != null && _proxy.ToString() != Expression.ToString())
                    SetFunc(Proxy);
                InvalidatePoints();
            }
        }

        /// <summary>
        /// The compiled lambda expression Func(x, t) of the algebraic expression used by the Trace.
        /// </summary>
        [JsonIgnore]
        public Func<double, double, double> Func { get; private set; }

        /// <summary>
        /// The first derivative of Func with respect to x. Used to detect discontinuities while plotting.
        /// </summary>
        [JsonIgnore]
        public Func<double, double, double> Derivative { get; private set; }

        public Expression DerivativeExpression { get; private set; }

        public bool UsesTime => Proxy != null && Proxy.UsesTime() || Expression.UsesTime();
        public bool UsesXref => Expression.UsesXref();

        private void SetFunc(Expression e)
        {
            Func = e.AsFunction();
            DerivativeExpression = e.Differentiate();
            Derivative = DerivativeExpression.AsFunction();
        }

        #endregion

        #region Drawing

        public List<List<PointF>> PointLists = new List<List<PointF>>();

        // Method DrawAsync is made asynchronous purely as a programming exercise.
        // All drawing must take place on the main Windows UI thread, and no time
        // is saved by multithreading the ComputePointsAsync() point computations.
        public async void DrawAsync(Graphics g, DomainInfo domainInfo, Viewport viewport,
            float penWidth, Phase phase, double time, PlotType plotType)
        {
            if (phase == Phase.Fill && BrushIsTransparent())
                return;
            if (Func == null
                || LastDomainInfo != domainInfo
                || Viewport != viewport
                || LastTime != time && UsesTime
                || LastPlotType != plotType
                || !PointLists.Any())
            {
                InvalidatePoints();
                LastDomainInfo = domainInfo;
                Viewport = viewport;
                LastTime = time;
                LastPlotType = plotType;
                var pointLists = await ComputePointsAsync(domainInfo, viewport, time, plotType == PlotType.Polar);
                PointLists.AddRange(pointLists);
                pointLists.Clear();
            }
            switch (phase)
            {
                case Phase.Fill:
                    //using (var pen = new Pen(LimitColour, penWidth) { DashStyle = DashStyle.Dash })
                    using (var brush = CreateBrush(g.Transform))
                        PointLists.ForEach(p => FillSection(g, brush, plotType, p));
                    break;
                case Phase.Draw:
                    using (var pen = new Pen(PenColour, PenWidth * penWidth) { DashStyle = PenStyle })
                        PointLists.ForEach(p => DrawSection(g, pen, p));
                    break;
            }
        }

        private bool BrushIsTransparent()
        {
            if (FillTransparencyPercent == 100)
                return true;
            switch (BrushType)
            {
                case BrushType.Solid:
                    return FillColour1.A == 0;
                case BrushType.Hatch:
                case BrushType.LinearGradient:
                case BrushType.PathGradient:
                    return FillColour1.A == 0 && FillColour2.A == 0;
                case BrushType.Texture:
                    return string.IsNullOrWhiteSpace(Texture);
            }
            return false;
        }

        private Brush CreateBrush(Matrix m)
        {
            Color
                paint1 = Utility.MakeColour(FillColour1, FillTransparencyPercent),
                paint2 = Utility.MakeColour(FillColour2, FillTransparencyPercent);
            switch (BrushType)
            {
                case BrushType.Solid:
                    return new SolidBrush(paint1);
                case BrushType.Hatch:
                    return new HatchBrush(HatchStyle, paint1, paint2);
                case BrushType.Texture:
                    if (string.IsNullOrWhiteSpace(Texture))
                        goto case BrushType.Solid;
                    m = m.Clone();
                    float offsetX = m.OffsetX, offsetY = m.OffsetY;
                    m.Invert();
                    m.Translate(offsetX, offsetY);
                    var attr = new ImageAttributes();
                    attr.SetColorMatrix(new ColorMatrix { Matrix33 = 1 - FillTransparencyPercent / 100f });
                    var bytes = Convert.FromBase64String(Texture);
                    using (var stream = new MemoryStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Seek(0, SeekOrigin.Begin);
                        using (var image = Image.FromStream(stream))
                            return new TextureBrush(image, new Rectangle(0, 0, image.Width, image.Height), attr)
                            { Transform = m, WrapMode = WrapMode };
                    }
                case BrushType.PathGradient:
                    var path = new GraphicsPath(FillMode);
                    path.AddRectangle(Viewport.Limits);
                    return new PathGradientBrush(path)
                    {
                        CenterColor = paint1,
                        SurroundColors = new Color[] { paint2 }
                    };
                case BrushType.LinearGradient:
                    return new LinearGradientBrush(Viewport.Limits, paint1, paint2, GradientMode);
            }
            return new SolidBrush(paint1);
        }

        private void DrawSection(Graphics g, Pen pen, List<PointF> points) =>
            new Plotter(g, FillMode, Interpolation, points).Draw(pen);

        private void FillSection(Graphics g, Brush brush, PlotType plotType, List<PointF> points) =>
            new Plotter(g, FillMode, Interpolation, points).Fill(brush, plotType);

        private Task<List<List<PointF>>> ComputePointsAsync(
            DomainInfo domainInfo, Viewport viewport, double time, bool polar)
        {
            var result = new List<List<PointF>>();
            var bounds = viewport.Boundary;
            float
                xmin = bounds.Left,
                ymin = bounds.Top,
                xmax = bounds.Right,
                ymax = bounds.Bottom;
            var domain = GetDomain(domainInfo, viewport, polar);
            var start = domain.Item1;
            var finish = domain.Item2;
            double step = (finish - start) / StepCount;
            double dx = step;
            var previousPoint = PointF.Empty;
            var skip = true;
            List<PointF> list = null;
            for (double x = start; x <= finish; x += dx)
            {
                var slope = Derivative(x, time);
                var pts = GetPoints(previousPoint, x, time, slope, polar);
                foreach (var p in pts)
                {
                    var q = p;
                    previousPoint = q;
                    if (polar)
                    {
                        var a = q.X;
                        q.X = (float)(q.Y * Math.Cos(a));
                        q.Y = (float)(q.Y * Math.Sin(a));
                    }
                    if (q.IsEmpty || float.IsInfinity(q.Y) || float.IsNaN(q.Y))
                        skip = true;
                    else
                    {
                        if (skip)
                        {
                            skip = false;
                            list = new List<PointF>();
                            result.Add(list);
                        }
                        if (q.X < xmin) q.X = xmin; else if (q.X > xmax) q.X = xmax;
                        if (q.Y < ymin) q.Y = ymin; else if (q.Y > ymax) q.Y = ymax;
                        list.Add(q);
                    }
                }
                dx = IsValid(slope) ? step / Math.Min(Math.Sqrt(1 + slope * slope), 10) : step;
            }
            // Every segment of the trace must include at least 2 points.
            result.RemoveAll(p => p.Count < 2);
            // Remove any redundant points.
            if (Interpolation == Interpolation.Linear)
                result = result.Select(p => Pare(p)).ToList();
            // Done.
            return Task.FromResult(result);
        }

        public Tuple<float, float> GetDomain(DomainInfo domainInfo, Viewport viewport, bool polar)
        {
            if (polar)
                return new Tuple<float, float>(domainInfo.MinRadians, domainInfo.MaxRadians);
            if (domainInfo.UseGraphWidth)
                return new Tuple<float, float>(Viewport.Left, Viewport.Right);
            return new Tuple<float, float>(domainInfo.MinCartesian, domainInfo.MaxCartesian);
        }

        private IEnumerable<PointF> GetPoints(PointF previousPoint, double x, double t, double slope, bool polar)
        {
            var p = new PointF((float)x, 0);
            double y = 0;
            try
            {
                y = Func(x, t);
                p.Y = (float)y;
            }
            catch
            {
                p = PointF.Empty;
            }
            if (IsValid(p) && IsValid(previousPoint))
            {
                float x1 = previousPoint.X, y1 = previousPoint.Y;
                // Compare the supplied angle, computed from the value of the derivative,
                // to that obtained by subtracting the current point from previous one.
                // If these two differ by an obtuse angle, then there's almost certainly
                // a discontinuity here; so send back a break.
                var angle = Math.Abs(Math.Atan(slope) - Math.Atan2(y - y1, x - x1));
                if (angle > Math.PI / 2)
                    yield return PointF.Empty;
            }
            yield return p;
        }

        public void InvalidatePoints()
        {
            PointLists.Clear();
        }

        private static List<PointF> Pare(List<PointF> list)
        {
            // Inspect every group of 3 adjacent points,
            // and if collinear, remove the middle one.
            var result = new List<PointF>(list.Take(2));
            PointF p = result[0], q = result[1];
            foreach (var r in list.Skip(2))
            {
                if ((q.X - p.X) * (r.Y - p.Y) == (r.X - p.X) * (q.Y - p.Y))
                    result[result.Count - 1] = r;
                else
                {
                    result.Add(r);
                    p = q;
                    q = r;
                }
            }
            return result;
        }

        private void RestoreDefaults()
        {
            _formula = Defaults.TraceFormula;
            _interpolation = Defaults.TraceInterpolation;
            _visible = Defaults.TraceVisible;
        }

        private static bool IsInvalid(float x) => float.IsInfinity(x) || float.IsNaN(x);
        private static bool IsInvalid(double x) => double.IsInfinity(x) || double.IsNaN(x);
        private static bool IsInvalid(PointF p) => p.IsEmpty || IsInvalid(p.X) || IsInvalid(p.Y);
        private static bool IsValid(float x) => !IsInvalid(x);
        private static bool IsValid(double x) => !IsInvalid(x);
        private static bool IsValid(PointF p) => !IsInvalid(p);

        #endregion
    }
}
