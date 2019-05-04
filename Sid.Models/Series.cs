namespace Sid.Models
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
    using Sid.Expressions;

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
                    InvalidatePoints();
                    OnPropertyChanged("Formula");
                }
            }
        }

        [JsonIgnore]
        public Func<double, double, double> Func { get; private set; }

        private RectangleF Limits;

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

        private PlotType LastPlotType = (PlotType)(-1);
        private double LastTime = -1;

        #endregion

        #region Drawing

        private List<List<PointF>> PointLists = new List<List<PointF>>();

        public async void Draw(Graphics g, RectangleF limits, float penWidth, bool fill, double time, PlotType plotType)
        {
            if (fill && (FillColour == Color.Transparent || FillTransparencyPercent == 100))
                return; // Not just an optimisation; omits vertical asymptotes too.
            if (Func == null
                || Limits != limits
                || LastTime != time && Expression.UsesTime()
                || LastPlotType != plotType
                || !PointLists.Any())
            {
                InvalidatePoints();
                Limits = limits;
                LastTime = time;
                LastPlotType = plotType;
                var pointLists = await ComputePointsAsync(limits, time, plotType);
                PointLists.AddRange(pointLists);
                pointLists.Clear();
            }
            if (fill)
                using (var pen = new Pen(LimitColour, penWidth))
                {
                    pen.DashStyle = DashStyle.Dash;
                    var paint = Utility.MakeColour(FillColour, FillTransparencyPercent);
                    using (var brush = new SolidBrush(paint))
                        PointLists.ForEach(p => FillArea(g, pen, brush, p, plotType));
                }
            else
                using (var pen = new Pen(PenColour, penWidth))
                    PointLists.ForEach(p => g.DrawLines(pen, p.ToArray()));
        }

        private Task<List<List<PointF>>> ComputePointsAsync(RectangleF limits, double time, PlotType plotType)
        {
            var result = new List<List<PointF>>();
            List<PointF> points = null;
            float
                x1 = Limits.Left, y1 = Limits.Top, y2 = Limits.Bottom,
                w = Limits.Width, h = 8 * Limits.Height;
            var skip = true;
            float x, y;
            for (var step = 0; step <= StepCount; step++)
            {
                switch (plotType)
                {
                    case PlotType.Polar:
                        var a = step * Math.PI / StepCount;
                        var r = (float)Func(a, time);
                        x = (float)(r * Math.Cos(a));
                        y = (float)(r * Math.Sin(a));
                        break;
                    default:
                        x = x1 + step * w / StepCount;
                        y = (float)Func(x, time);
                        break;
                }
                if (float.IsInfinity(y) || float.IsNaN(y) || y < y1 - h || y > y2 + h)
                    skip = true;
                else
                {
                    if (skip)
                    {
                        skip = false;
                        points = new List<PointF>();
                        result.Add(points);
                    }
                    points.Add(new PointF(x, y));
                }
            }
            // Every segment of the trace must include at least 2 points.
            result.RemoveAll(p => p.Count < 2);
            return Task.FromResult(result);
        }

        private void FillArea(Graphics g, Pen pen, Brush brush, List<PointF> p, PlotType plotType)
        {
            var n = p.Count;
            switch (plotType)
            {
                case PlotType.Cartesian:
                    var points = new PointF[n + 2];
                    p.CopyTo(points);
                    points[n] = new PointF(points[n - 1].X, 0);
                    points[n + 1] = new PointF(points[0].X, 0);
                    g.FillPolygon(brush, points);
                    // Draw vertical asymptotes iff X extremes are not Limits.
                    if (points[n].X < Limits.Right)
                        g.DrawLine(pen, points[n - 1], points[n]);
                    if (points[0].X > Limits.Left)
                        g.DrawLine(pen, points[n + 1], points[0]);
                    break;
                case PlotType.Polar:
                    g.FillPolygon(brush, p.ToArray());
                    break;
            }
        }

        private void InvalidatePoints() => PointLists.Clear();

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
