namespace ToyGraf.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ToyGraf.Expressions;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;

    [Serializable]
    public class Trace : Style
    {
        public Trace() { SetFormula("0"); }

        public Trace(Graph graph): this()
        {
            _brushType = graph.BrushType;
            _fillColour1 = graph.FillColour1;
            _fillColour2 = graph.FillColour2;
            _fillTransparencyPercent = graph.FillTransparencyPercent;
            _gradientMode = graph.GradientMode;
            _hatchStyle = graph.HatchStyle;
            _limitColour = graph.LimitColour;
            _penColour = graph.PenColour;
            _penStyle = graph.PenStyle;
            _penWidth = graph.PenWidth;
            _stepCount = graph.StepCount;
        }

        #region Visual Properties

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
        private Viewport Viewport;

        private bool _visible = true;
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
        /// <param name="formula"></param>
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
        /// The System.Linq.Expressions representation of the algebraic expression used by the Trace.
        /// This is obtained from the Expression property by replacing all Udf tokens (User Defined Functions)
        /// with calls to other Traces in the Graph.
        /// </summary>
        [JsonIgnore]
        public Expression Proxy
        {
            get => _proxy ?? Expression;
            set
            {
                _proxy = value;
                SetFunc(Proxy);
                InvalidatePaths();
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

        public bool UsesTime => Proxy != null && Proxy.UsesTime() || Expression.UsesTime();

        private void SetFunc(Expression e)
        {
            Func = e.AsFunction();
            Derivative = e.Differentiate().AsFunction();
        }

        #endregion

        #region Drawing

        protected override void StepCountChanged() => InvalidatePaths();

        private List<List<PointF>> PointLists = new List<List<PointF>>();
        private readonly List<GraphicsPath>
            DrawPaths = new List<GraphicsPath>(),
            FillPaths = new List<GraphicsPath>();

        // Method DrawAsync is made asynchronous purely as a programming exercise.
        // All drawing must take place on the main Windows UI thread, and no time
        // is saved by multithreading the ComputePointsAsync() point computations.
        public async void DrawAsync(Graphics g, DomainInfo domainInfo, Viewport viewport,
            float penWidth, bool fill, double time, PlotType plotType, Interpolation interpolation)
        {
            if (Func == null
                || LastDomainInfo != domainInfo
                || Viewport != viewport
                || LastTime != time && UsesTime
                || LastPlotType != plotType
                || !PointLists.Any())
            {
                InvalidatePaths();
                LastDomainInfo = domainInfo;
                Viewport = viewport;
                LastTime = time;
                LastPlotType = plotType;
                var pointLists = await ComputePointsAsync(domainInfo, viewport, time, plotType == PlotType.Polar);
                PointLists.AddRange(pointLists);
                pointLists.Clear();
            }
            bool usePaths;
            if (fill)
                using (var pen = new Pen(LimitColour, penWidth))
                {
                    usePaths = FillPaths.Any();
                    pen.DashStyle = DashStyle.Dash;
                    using (var brush = CreateBrush(g.Transform))
                        PointLists.ForEach(p => FillSection(g, brush, plotType, interpolation, p, usePaths));
                }
            else
                using (var pen = new Pen(PenColour, PenWidth * penWidth))
                {
                    usePaths = DrawPaths.Any();
                    pen.DashStyle = PenStyle;
                    PointLists.ForEach(p => DrawSection(g, pen, interpolation, p, usePaths));
                }
            if (DrawPaths.Any() && FillPaths.Any())
                InvalidatePoints();
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
                    if (Texture == null)
                        goto case BrushType.Solid;
                    var bytes = Convert.FromBase64String(Texture);
                    using (var stream = new MemoryStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Seek(0, SeekOrigin.Begin);
                        using (var image = Image.FromStream(stream))
                        {
                            var α = 1 - FillTransparencyPercent / 100f;
                            var attr = new ImageAttributes();
                            attr.SetColorMatrix(new ColorMatrix(new[]
                            {
                                new float[]{ 1, 0, 0, 0, 0},
                                new float[]{ 0, 1, 0, 0, 0},
                                new float[]{ 0, 0, 1, 0, 0},
                                new float[]{ 0, 0, 0, α, 0},
                                new float[]{ 0, 0, 0, 0, 1},
                            }));
                            m = m.Clone();
                            m.Invert();
                            return new TextureBrush(image, new Rectangle(new Point(0, 0), image.Size), attr)
                            {
                                Transform = m,
                                WrapMode = WrapMode
                            };
                        }
                    }
                case BrushType.PathGradient:
                    var path = new GraphicsPath();
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

        private void DrawSection(Graphics g, Pen pen, Interpolation interpolation,
            List<PointF> points, bool usePaths) =>
            new Plotter().Draw(g, pen, interpolation, points, DrawPaths, usePaths);

        private void FillSection(Graphics g, Brush brush, PlotType plotType, Interpolation interpolation,
            List<PointF> points, bool usePaths) =>
            new Plotter().Fill(g, brush, plotType, interpolation, points, FillPaths, usePaths);

        private Task<List<List<PointF>>> ComputePointsAsync(
            DomainInfo domainInfo, Viewport viewport, double time, bool polar)
        {
            var result = new List<List<PointF>>();
            List<PointF> points = null;
            var domain = GetDomain(domainInfo, viewport, polar);
            var start = domain.Item1;
            var finish = domain.Item2;
            double step = (finish - start) / StepCount;
            double dx = step;
            var previousPoint = PointF.Empty;
            var skip = true;
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
                            points = new List<PointF>();
                            result.Add(points);
                        }
                        points.Add(q);
                    }
                }
                if (IsValid(slope))
                    dx = step / Math.Min(Math.Sqrt(1 + slope * slope), 10);
                else
                    dx = step;
            }
            // Every segment of the trace must include at least 2 points.
            result.RemoveAll(p => p.Count < 2);
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
                // Otherwise, if there's a sign change between two points in a Cartesian
                // plot, send a break to connect the two segments more cleanly to the x-
                // axis (this improves cardinal spline rendering of areas of integration,
                // and is unnecessary for polar plots).
                else if (!polar && Math.Sign(y) != Math.Sign(y1))
                {
                    var q = new PointF((float)(x1 - y1 * (x - x1) / (y - y1)), 0);
                    yield return q;
                    yield return PointF.Empty;
                    yield return q;
                }
            }
            yield return p;
        }

        public void InvalidatePaths()
        {
            InvalidatePoints();
            DrawPaths.Clear();
            FillPaths.Clear();
        }

        private void InvalidatePoints()
        {
            PointLists.Clear();
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
