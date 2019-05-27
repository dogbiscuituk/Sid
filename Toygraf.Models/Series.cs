namespace ToyGraf.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ToyGraf.Expressions;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;

    [Serializable]
    public class Series : Style
    {
        public Series() { Formula = "0"; }

        public Series(Graph graph): this()
        {
            BrushType = graph.BrushType;
            FillColour = graph.FillColour;
            FillColour2 = graph.FillColour2;
            FillTransparencyPercent = graph.FillTransparencyPercent;
            GradientMode = graph.GradientMode;
            HatchStyle = graph.HatchStyle;
            LimitColour = graph.LimitColour;
            PenColour = graph.PenColour;
            PenStyle = graph.PenStyle;
            PenWidth = graph.PenWidth;
            StepCount = graph.StepCount;
        }

        #region Visual Properties

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
        [NonSerialized] private Domain LastDomain;
        [NonSerialized] private double LastTime = -1;

        #endregion

        #region Formula, Expression, Proxy, Func, Derivative

        private string _formula = string.Empty;
        /// <summary>
        /// Plain text version of the algebraic expression used by the Series.
        /// </summary>
        public string Formula
        {
            get => _formula;
            set
            {
                if (Formula != value)
                {
                    Expression = new Parser().Parse(value);
                    SetFunc(Expression);
                    _formula = value;
                    OnPropertyChanged("Formula");
                }
            }
        }

        /// <summary>
        /// A tokenised representation of the algebraic expression used by the Series.
        /// This is obtained by sending the Formula to the Parser.Parse() method.
        /// </summary>
        [JsonIgnore]
        public Expression Expression { get; private set; }

        [NonSerialized]
        private Expression _proxy;

        /// <summary>
        /// The System.Linq.Expressions representation of the algebraic expression used by the Series.
        /// This is obtained from the Expression property by replacing all Udf tokens (User Defined Functions)
        /// with calls to other Series in the Graph.
        /// </summary>
        [JsonIgnore]
        public Expression Proxy
        {
            get => _proxy;
            set
            {
                _proxy = value;
                SetFunc(Proxy);
                InvalidatePoints();
            }
        }

        /// <summary>
        /// The compiled lambda expression Func(x, t) of the algebraic expression used by the Series.
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

        protected override void StepCountChanged() => InvalidatePoints();

        private List<List<PointF>> PointLists = new List<List<PointF>>();

        // Method DrawAsync is made asynchronous purely as a programming exercise.
        // All drawing must take place on the main Windows UI thread, and no time
        // is saved by multithreading the ComputePointsAsync() point computations.
        public async void DrawAsync(Graphics g, Domain domain, Viewport viewport,
            float penWidth, bool fill, double time, PlotType plotType, Interpolation interpolation)
        {
            if (fill && (FillColour == Color.Transparent || FillTransparencyPercent == 100))
                return;
            if (Func == null
                || LastDomain != domain
                || Viewport != viewport
                || LastTime != time && UsesTime
                || LastPlotType != plotType
                || !PointLists.Any())
            {
                InvalidatePoints();
                LastDomain = domain;
                Viewport = viewport;
                LastTime = time;
                LastPlotType = plotType;
                var pointLists = await ComputePointsAsync(domain, viewport, time, plotType == PlotType.Polar);
                PointLists.AddRange(pointLists);
                pointLists.Clear();
            }
            if (fill)
                using (var pen = new Pen(LimitColour, penWidth))
                {
                    pen.DashStyle = DashStyle.Dash;
                    using (var brush = CreateBrush())
                        PointLists.ForEach(p => FillSection(g, brush, plotType, interpolation, p));
                }
            else
                using (var pen = new Pen(PenColour, PenWidth * penWidth))
                {
                    pen.DashStyle = PenStyle;
                    PointLists.ForEach(p => DrawSection(g, pen, interpolation, p));
                }
        }

        private Brush CreateBrush()
        {
            Color
                paint = Utility.MakeColour(FillColour, FillTransparencyPercent),
                paint2 = Utility.MakeColour(FillColour2, FillTransparencyPercent);
            switch (BrushType)
            {
                case BrushType.Solid:
                    return new SolidBrush(paint);
                case BrushType.Hatch:
                    return new HatchBrush(HatchStyle, paint, paint2);
                case BrushType.LinearGradient:
                    return new LinearGradientBrush(Viewport.Limits, paint, paint2, GradientMode);
            }
            return new SolidBrush(paint);
        }

        private void DrawSection(Graphics g, Pen pen,
            Interpolation interpolation, List<PointF> points) =>
            new Plotter().Draw(g, pen, interpolation, points);

        private void FillSection(Graphics g, Brush brush, PlotType plotType,
            Interpolation interpolation, List<PointF> points) =>
            new Plotter().Fill(g, brush, plotType, interpolation, points);

        private Task<List<List<PointF>>> ComputePointsAsync(
            Domain domain, Viewport viewport, double time, bool polar)
        {
            var result = new List<List<PointF>>();
            List<PointF> points = null;
            float start, finish;
            if (polar)
            {
                start = domain.MinRadians;
                finish = domain.MaxRadians;
            }
            else if (domain.UseGraphWidth)
            {
                start = Viewport.Left;
                finish = Viewport.Right;
            }
            else
            {
                start = domain.MinCartesian;
                finish = domain.MaxCartesian;
            }
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

        public void InvalidatePoints() => PointLists.Clear();

        private static bool IsInvalid(float x) => float.IsInfinity(x) || float.IsNaN(x);
        private static bool IsInvalid(double x) => double.IsInfinity(x) || double.IsNaN(x);
        private static bool IsInvalid(PointF p) => p.IsEmpty || IsInvalid(p.X) || IsInvalid(p.Y);
        private static bool IsValid(float x) => !IsInvalid(x);
        private static bool IsValid(double x) => !IsInvalid(x);
        private static bool IsValid(PointF p) => !IsInvalid(p);

        #endregion
    }
}
