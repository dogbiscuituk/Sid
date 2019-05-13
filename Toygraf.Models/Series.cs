namespace ToyGraf.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ToyGraf.Expressions;

    [Serializable]
    public class Series: INotifyPropertyChanged
    {
        public Series() { }

        public Series(Graph graph): this()
        {
            FillColour = graph.FillColour;
            LimitColour = graph.LimitColour;
            PenColour = graph.PenColour;
            StepCount = graph.StepCount;
            FillTransparencyPercent = graph.FillTransparencyPercent;
            Formula = "0";
        }

        #region Properties

        private Color _penColour;
        public Color PenColour
        {
            get => _penColour;
            set
            {
                if (PenColour != value)
                {
                    _penColour = value;
                    OnPropertyChanged("PenColour");
                }
            }
        }

        private Color _fillColour;
        public Color FillColour
        {
            get => _fillColour;
            set
            {
                if (FillColour != value)
                {
                    _fillColour = value;
                    OnPropertyChanged("FillColour");
                }
            }
        }

        private int _fillTransparencyPercent;
        public int FillTransparencyPercent
        {
            get => _fillTransparencyPercent;
            set
            {
                if (FillTransparencyPercent != value)
                {
                    _fillTransparencyPercent = value;
                    OnPropertyChanged("FillTransparencyPercent");
                }
            }
        }

        private Color _limitColour;
        public Color LimitColour
        {
            get => _limitColour;
            set
            {
                if (LimitColour != value)
                {
                    _limitColour = value;
                    OnPropertyChanged("LimitColour");
                }
            }
        }

        [JsonIgnore]
        public Expression Expression { get; private set; }

        [NonSerialized]
        private Expression _proxy;
        [JsonIgnore]
        public Expression Proxy
        {
            get => _proxy;
            set
            {
                _proxy = value;
                Func = Proxy.AsFunction();
                InvalidatePoints();
            }
        }

        private string _formula = string.Empty;
        public string Formula
        {
            get => _formula;
            set
            {
                if (Formula != value)
                {
                    Expression = new Parser().Parse(value);
                    Func = Expression.AsFunction();
                    _formula = value;
                    OnPropertyChanged("Formula");
                }
            }
        }

        [JsonIgnore]
        public Func<double, double, double> Func { get; private set; }

        [NonSerialized]
        private Viewport Viewport;

        private int _stepCount;
        public int StepCount
        {
            get => _stepCount;
            set
            {
                if (StepCount != value)
                {
                    _stepCount = value;
                    InvalidatePoints();
                    OnPropertyChanged("StepCount");
                }
            }
        }

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

        #region Drawing

        private List<List<PointF>> PointLists = new List<List<PointF>>();

        // Method DrawAsync is made asynchronous purely as a programming exercise.
        // All drawing must take place on the main Windows UI thread, and no time
        // is saved by multithreading the ComputePointsAsync() point computations.

        public async void DrawAsync(Graphics g, Domain domain, Viewport viewport,
            float penWidth, bool fill, double time, PlotType plotType, FitType fitType)
        {
            if (fill && (FillColour == Color.Transparent || FillTransparencyPercent == 100))
                return; // Not just an optimisation; omits vertical asymptotes too.
            if (Func == null
                || LastDomain != domain
                || Viewport != viewport
                || LastTime != time && Expression.UsesTime()
                || LastPlotType != plotType
                || !PointLists.Any())
            {
                InvalidatePoints();
                LastDomain = domain;
                Viewport = viewport;
                LastTime = time;
                LastPlotType = plotType;
                var pointLists = await ComputePointsAsync(domain, viewport, time, plotType);
                PointLists.AddRange(pointLists);
                pointLists.Clear();
            }
            if (fill)
                using (var pen = new Pen(LimitColour, penWidth))
                {
                    pen.DashStyle = DashStyle.Dash;
                    var paint = Utility.MakeColour(FillColour, FillTransparencyPercent);
                    using (var brush = new SolidBrush(paint))
                        PointLists.ForEach(p => FillArea(g, pen, brush, p, plotType, fitType));
                }
            else
                using (var pen = new Pen(PenColour, penWidth))
                    PointLists.ForEach(p => DrawSection(g, pen, p.ToArray(), fitType));
        }

        private void DrawSection(Graphics g, Pen pen, PointF[] points, FitType fitType)
        {
            switch (fitType)
            {
                case FitType.StraightLines:
                    g.DrawLines(pen, points);
                    break;
                case FitType.CardinalSplines:
                    g.DrawCurve(pen, points);
                    break;
            }
        }

        private void FillArea(Graphics g, Pen pen, Brush brush, List<PointF> p, PlotType plotType, FitType fitType)
        {
            switch (plotType)
            {
                case PlotType.Cartesian:
                    var n = p.Count;
                    var points = new PointF[n + 2];
                    p.CopyTo(points);
                    points[n] = new PointF(points[n - 1].X, 0);
                    points[n + 1] = new PointF(points[0].X, 0);
                    FillSection(g, brush, points, fitType);
                    // Draw vertical asymptotes iff X extremes are not Limits.
                    if (points[n].X < Viewport.Right)
                        g.DrawLine(pen, points[n - 1], points[n]);
                    if (points[0].X > Viewport.Left)
                        g.DrawLine(pen, points[n + 1], points[0]);
                    break;
                case PlotType.Polar:
                    FillSection(g, brush, p.ToArray(), fitType);
                    break;
            }
        }

        private void FillSection(Graphics g, Brush brush, PointF[] points, FitType fitType)
        {
            switch (fitType)
            {
                case FitType.StraightLines:
                    g.FillPolygon(brush, points);
                    break;
                case FitType.CardinalSplines:
                    g.FillClosedCurve(brush, points);
                    break;
            }
        }

        private Task<List<List<PointF>>> ComputePointsAsync(
            Domain domain, Viewport viewport, double time, PlotType plotType)
        {
            var result = new List<List<PointF>>();
            List<PointF> points = null;
            float start, length;
            if (plotType == PlotType.Polar)
            {
                start = domain.MinRadians;
                length = domain.MaxRadians - start;

            }
            else if (domain.UseGraphWidth)
            {
                start = Viewport.Left;
                length = Viewport.Width;
            }
            else
            {
                start = domain.MinCartesian;
                length = domain.MaxCartesian;
            }
            var skip = true;
            double x, y;
            for (var step = 0; step <= StepCount; step++)
            {
                switch (plotType)
                {
                    case PlotType.Polar:
                        var θ = start + step * length / StepCount;
                        var r = GetY(θ, time);
                        x = (float)(r * Math.Cos(θ));
                        y = (float)(r * Math.Sin(θ));
                        break;
                    default:
                        x = start + step * length / StepCount;
                        y = GetY(x, time);
                        break;
                }
                if (double.IsInfinity(y) || double.IsNaN(y))
                    skip = true;
                else
                {
                    if (skip)
                    {
                        skip = false;
                        points = new List<PointF>();
                        result.Add(points);
                    }
                    points.Add(new PointF((float)x, (float)y));
                }
            }
            // Every segment of the trace must include at least 2 points.
            result.RemoveAll(p => p.Count < 2);
            return Task.FromResult(result);
        }

        private double GetY(double x, double t)
        {
            try { return Func(x, t); }
            catch { return double.NaN; }
        }

        public void InvalidatePoints() => PointLists.Clear();

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
