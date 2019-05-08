namespace Sid.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Sid.Expressions;

    [Serializable]
    public class Graph : INotifyPropertyChanged
    {
        public Graph() { RestoreDefaults(); }

        #region Properties

        private Color _paperColour;
        public Color PaperColour
        {
            get => _paperColour;
            set
            {
                if (PaperColour != value)
                {
                    _paperColour = value;
                    OnPropertyChanged("PaperColour");
                }
            }
        }

        private int _paperTransparencyPercent;
        public int PaperTransparencyPercent
        {
            get => _paperTransparencyPercent;
            set
            {
                if (PaperTransparencyPercent != value)
                {
                    _paperTransparencyPercent = value;
                    OnPropertyChanged("PaperTransparencyPercent");
                }
            }
        }

        private Color _axisColour;
        public Color AxisColour
        {
            get => _axisColour;
            set
            {
                if (AxisColour != value)
                {
                    _axisColour = value;
                    OnPropertyChanged("AxisColour");
                }
            }
        }

        private Color _gridColour;
        public Color GridColour
        {
            get => _gridColour;
            set
            {
                if (GridColour != value)
                {
                    _gridColour = value;
                    OnPropertyChanged("GridColour");
                }
            }
        }

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

        private Optimization _optimization;
        public Optimization Optimization
        {
            get => _optimization;
            set
            {
                if (Optimization != value)
                {
                    _optimization = value;
                    OnPropertyChanged("Optimization");
                }
            }
        }

        private PlotType _plotType;
        public PlotType PlotType
        {
            get => _plotType;
            set
            {
                if (PlotType != value)
                {
                    _plotType = value;
                    OnPropertyChanged("PlotType");
                }
            }
        }

        [JsonIgnore]
        private Domain _domain;

        public bool DomainGraphWidth
        {
            get => _domain.UseGraphWidth;
            set
            {
                if (DomainGraphWidth != value)
                {
                    _domain.UseGraphWidth = value;
                    OnPropertyChanged("DomainGraphWidth");
                }
            }
        }

        public float DomainMinCartesian
        {
            get => _domain.MinCartesian;
            set
            {
                if (DomainMinCartesian != value)
                {
                    _domain.MinCartesian = value;
                    OnPropertyChanged("DomainMinCartesian");
                }
            }
        }

        public float DomainMaxCartesian
        {
            get => _domain.MaxCartesian;
            set
            {
                if (DomainMaxCartesian != value)
                {
                    _domain.MaxCartesian = value;
                    OnPropertyChanged("DomainMaxCartesian");
                }
            }
        }

        public float DomainMinPolar
        {
            get => _domain.MinPolar;
            set
            {
                if (DomainMinPolar != value)
                {
                    _domain.MinPolar = value;
                    OnPropertyChanged("DomainMinPolar");
                }
            }
        }

        public float DomainMaxPolar
        {
            get => _domain.MaxPolar;
            set
            {
                if (DomainMaxPolar != value)
                {
                    _domain.MaxPolar = value;
                    OnPropertyChanged("DomainMaxPolar");
                }
            }
        }

        public bool DomainPolarDegrees
        {
            get => _domain.PolarDegrees;
            set
            {
                if (DomainPolarDegrees != value)
                {
                    _domain.PolarDegrees = value;
                    OnPropertyChanged("DomainPolarDegrees");
                }
            }
        }

        private PointF _location, _originalLocation;
        public PointF Location
        {
            get => _location;
            set
            {
                if (Location != value)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private SizeF _size, _originalSize;
        public SizeF Size
        {
            get => _size;
            set
            {
                if (Size != value)
                {
                    _size = value;
                    OnPropertyChanged("Size");
                }
            }
        }

        [JsonIgnore]
        public RectangleF Limits { get => new RectangleF(Location, Size); }

        private Elements _elements;
        public Elements Elements
        {
            get => _elements;
            set
            {
                if (Elements != value)
                {
                    _elements = value;
                    OnPropertyChanged("Elements");
                }
            }
        }

        private TickStyles _tickStyles;
        public TickStyles TickStyles
        {
            get => _tickStyles;
            set
            {
                if (TickStyles != value)
                {
                    _tickStyles = value;
                    OnPropertyChanged("TickStyles");
                }
            }
        }

        private bool Xaxis { get => (Elements & Elements.Xaxis) != 0; }
        private bool Yaxis { get => (Elements & Elements.Yaxis) != 0; }
        private bool Xcal { get => (Elements & Elements.Xcalibration) != 0; }
        private bool Ycal { get => (Elements & Elements.Ycalibration) != 0; }
        private bool Xticks { get => (Elements & Elements.Xticks) != 0; }
        private bool Yticks { get => (Elements & Elements.Yticks) != 0; }
        private bool Hlines { get => (Elements & Elements.HorizontalGridLines) != 0; }
        private bool Vlines { get => (Elements & Elements.VerticalGridLines) != 0; }

        private int _stepCount;
        public int StepCount
        {
            get => _stepCount;
            set
            {
                if (StepCount != value)
                {
                    _stepCount = value;
                    OnPropertyChanged("StepCount");
                    foreach (var series in Series)
                        series.StepCount = StepCount;
                }
            }
        }

        private List<Series> _series = new List<Series>();

        public List<Series> Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged("Series");
            }
        }

        private void RestoreDefaults()
        {
            // bool
            _domain.UseGraphWidth = Defaults.GraphDomainGraphWidth;
            _domain.PolarDegrees = Defaults.GraphDomainPolarDegrees;
            // int
            _fillTransparencyPercent = Defaults.GraphFillTransparencyPercent;
            _paperTransparencyPercent = Defaults.GraphPaperTransparencyPercent;
            _stepCount = Defaults.GraphStepCount;
            // float
            _domain.MaxCartesian = Defaults.GraphDomainMaxCartesian;
            _domain.MaxPolar = Defaults.GraphDomainMaxPolar;
            _domain.MinCartesian = Defaults.GraphDomainMinCartesian;
            _domain.MinPolar = Defaults.GraphDomainMinPolar;
            // Color
            _axisColour = Defaults.GraphAxisColour;
            _fillColour = Defaults.GraphFillColour;
            _gridColour = Defaults.GraphGridColour;
            _limitColour = Defaults.GraphLimitColour;
            _paperColour = Defaults.GraphPaperColour;
            _penColour = Defaults.GraphPenColour;
            // Elements
            _elements = Defaults.GraphElements;
            //Optimization
            _optimization = Optimization.Default;
            // PlotType
            _plotType = Defaults.GraphPlotType;
            // PointF
            _location = _originalLocation = Defaults.GraphLocation;
            // SizeF
            _size = _originalSize = Defaults.GraphSize;
            // TickStyles
            _tickStyles = Defaults.GraphTickStyles;
        }

        #endregion

        #region Series Management

        public Series AddSeries()
        {
            var series = new Series(this);
            Series.Add(series);
            series.PropertyChanged += Series_PropertyChanged;
            OnPropertyChanged("Series");
            return series;
        }

        public void Clear()
        {
            RemoveSeriesRange(0, Series.Count);
            RestoreDefaults();
        }

        public void RemoveSeriesAt(int index)
        {
            if (index < 0 || index >= Series.Count)
                return;
            var series = Series[index];
            series.PropertyChanged -= Series_PropertyChanged;
            Series.Remove(series);
            OnPropertyChanged("Series");
        }

        public void RemoveSeriesRange(int index, int count)
        {
            while (count-- > 0)
                RemoveSeriesAt(index + count);
        }

        #endregion

        #region Drawing

        public void Draw(Graphics g, Rectangle r, double time)
        {
            InitOptimization(g);
            g.Transform = GetMatrix(r);
            var penWidth = (Size.Width / r.Width + Size.Height / r.Height);
            InitProxies();
            for (var call = 1; call <= 2; call++)
            {
                bool fill = call == 1;
                Series.ForEach(s => 
                {
                    if (s.Visible)
                        s.DrawAsync(g, _domain, Limits, penWidth, fill, time, PlotType);
                });
                if (fill)
                    DrawGrid(g, penWidth);
            }
        }

        public void InitProxies()
        {
            var count = Series.Count;
            var hit = new bool[count, count];
            for (int row = 0; row < count; row++)
            {
                var matches = Regex.Matches(Series[row].Formula, @"[fF](\d+)");
                foreach (Match match in matches)
                {
                    var col = int.Parse(match.Groups[1].Value);
                    if (col >= 0 && col < count)
                        hit[row, col] = true;
                }
            }
            bool somethingChanged;
            do
            {
                somethingChanged = false;
                for (int row = 0; row < count; row++)
                    for (int col = 0; col < count; col++)
                        if (hit[row, col])
                            for (var r = 0; r < count; r++)
                                if (r != row && hit[r, row] && !hit[r, col])
                                {
                                    somethingChanged = true;
                                    hit[r, col] = true;
                                }
            }
            while (somethingChanged);
            var refs = Series.Select(p => p.Expression).ToArray();
            for (int index = 0; index < count; index++)
                Series[index].Proxy = hit[index, index]
                    ? Expression.Default(typeof(void))
                    : Series[index].Expression.AsProxy(Expressions.x, Expressions.t, refs);
        }

        public PointF ScreenToGraph(Point p, Rectangle r)
        {
            var points = new[] { new PointF(p.X, p.Y) };
            var matrix = GetMatrix(r);
            matrix.Invert();
            matrix.TransformPoints(points);
            return points[0];
        }

        private void BumpDown(ref double value) => value = value == 5 ? 2 : value / 2;
        private void BumpUp(ref double value) => value = value == 2 ? 5 : value * 2;

        private void DrawGrid(Graphics g, float penWidth)
        {
            var limits = Limits;
            float x1 = limits.X, y1 = limits.Bottom, x2 = limits.Right, y2 = limits.Top;
            using (Pen gridPen = new Pen(GridColour, penWidth) { DashStyle = DashStyle.Dot },
                axisPen = new Pen(AxisColour, penWidth))
            using (var font = new Font("Arial", 5 * penWidth))
            using (var format = new StringFormat(StringFormat.GenericTypographic) {
                Alignment = StringAlignment.Far })
            {
                var brush = Brushes.DarkGray;
                double
                    logX = Math.Log10(Math.Abs(x2 - x1)),
                    logY = Math.Log10(Math.Abs(y2 - y1));
                for (var phase = (GridPhase)0; (int)phase < 4; phase++)
                {
                    var vertical = phase == GridPhase.VerticalLines || phase == GridPhase.Yaxis;
                    if (phase == GridPhase.HorizontalLines || phase == GridPhase.VerticalLines)
                    {
                        var log = PlotType != PlotType.Anisotropic || vertical ? logX : logY;
                        var order = Math.Floor(log);
                        var scale = log - order;
                        double increment = scale < 0.3 ? 2 : scale < 0.7 ? 5 : 10;
                        BumpDown(ref increment);
                        BumpDown(ref increment);
                        for (var pass = (GridPass)0; (int)pass < 3; pass++)
                        {
                            var dy = increment * Math.Pow(10, order - 1);
                            for (var y = 0.0; y <= Math.Max(Math.Abs(y1), Math.Abs(y2)); y += dy)
                            {
                                var pen = pass == GridPass.Ticks ? axisPen : gridPen;
                                DrawGridLine(g, pen, font, brush, x1, x2, (float)y, format, vertical, pass);
                                if (y != 0.0)
                                    DrawGridLine(g, pen, font, brush, x1, x2, -(float)y, format, vertical, pass);
                            }
                            BumpUp(ref increment);
                        }
                    }
                    else
                        DrawGridLine(g, axisPen, font, brush, x1, x2, 0, format, vertical, GridPass.Axes);
                    var t = x1; x1 = y1; y1 = t;
                    t = x2; x2 = y2; y2 = t;
                }
            }
        }

        private void DrawGridLine(Graphics g, Pen pen, Font font, Brush brush,
            float x1, float x2, float y, StringFormat format, bool v, GridPass pass)
        {
            if (pass == GridPass.Ticks)
            {
                var tickSize = font.Size;
                x1 = (TickStyles & TickStyles.Positive) != 0 ? tickSize : 0;
                x2 = (TickStyles & TickStyles.Negative) != 0 ? tickSize : 0;
            }
            float x = 0, y1 = y, y2 = y, z = -y;
            if (v)
            {
                var t = x; x = y; y = t;
                t = x1; x1 = y1; y1 = t;
                t = x2; x2 = y2; y2 = t;
                z = -z;
            }
            switch (pass)
            {
                case GridPass.Axes when v && Yaxis || !v && Xaxis:
                case GridPass.Lines when (v && Vlines || !v && Hlines) && PlotType != PlotType.Polar:
                    g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case GridPass.Lines when v && Vlines && PlotType == PlotType.Polar:
                    g.DrawEllipse(pen, -x1, -x1, 2 * x1, 2 * x1);
                    break;
                case GridPass.Calibration when v && Xcal || !v && Ycal:
                    g.ScaleTransform(1, -1);
                    g.DrawString(z.ToString(), font, brush, x - pen.Width, y + pen.Width, format);
                    g.ScaleTransform(1, -1);
                    break;
                case GridPass.Ticks:
                    if (v && Xticks || !v && Yticks)
                        g.DrawLine(pen, x1, y1, x2, y2);
                    break;
            }
        }

        private Matrix GetMatrix(Rectangle r)
        {
            return new Matrix(Limits, new[]
            {
                new PointF(r.Left, r.Bottom),
                new PointF(r.Right, r.Bottom), r.Location
            });
        }

        private void InitOptimization(Graphics g)
        {
            switch (Optimization)
            {
                case Optimization.HighQuality:
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    return;
                case Optimization.HighSpeed:
                    g.InterpolationMode = InterpolationMode.Low;
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.SmoothingMode = SmoothingMode.HighSpeed;
                    g.TextRenderingHint = TextRenderingHint.SystemDefault;
                    g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    return;
            }
        }

        #endregion

        #region Scroll & Zoom

        public void Scroll(double xFactor, double yFactor)
        {
            Location = new PointF(
                (float)(Location.X + Size.Width * xFactor),
                (float)(Location.Y + Size.Height * yFactor));
        }

        public void ScrollBy(float xDelta, float yDelta) =>
            Location = new PointF(Location.X + xDelta, Location.Y + yDelta);

        public void ScrollTo(float x, float y) =>
            Location = new PointF(x - Size.Width / 2, y - Size.Height / 2);

        public void Zoom(double factor) => Zoom(factor, factor);

        public void Zoom(double xFactor, double yFactor)
        {
            Location = new PointF(
                (float)(Location.X - Size.Width * (xFactor - 1) / 2),
                (float)(Location.Y - Size.Height * (yFactor - 1) / 2));
            Size = new SizeF(
                (float)(Size.Width * xFactor),
                (float)(Size.Height * yFactor));
        }

        public void ZoomReset() { Location = _originalLocation; Size = _originalSize; }
        public void ZoomSet() { _originalLocation = Location; _originalSize = Size; }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Series_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Series[{Series.IndexOf((Series)sender)}].{e.PropertyName}");

        #endregion
    }
}
