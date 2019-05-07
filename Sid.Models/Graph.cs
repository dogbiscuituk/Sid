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

        private bool _domainGraphWidth;
        public bool DomainGraphWidth
        {
            get => _domainGraphWidth;
            set
            {
                if (DomainGraphWidth != value)
                {
                    _domainGraphWidth = value;
                    OnPropertyChanged("DomainGraphWidth");
                }
            }
        }

        private float _domainMinCartesian;
        public float DomainMinCartesian
        {
            get => _domainMinCartesian;
            set
            {
                if (DomainMinCartesian != value)
                {
                    _domainMinCartesian = value;
                    OnPropertyChanged("DomainMinCartesian");
                }
            }
        }

        private float _domainMaxCartesian;
        public float DomainMaxCartesian
        {
            get => _domainMaxCartesian;
            set
            {
                if (DomainMaxCartesian != value)
                {
                    _domainMaxCartesian = value;
                    OnPropertyChanged("DomainMaxCartesian");
                }
            }
        }

        private float _domainMinPolar;
        public float DomainMinPolar
        {
            get => _domainMinPolar;
            set
            {
                if (DomainMinPolar != value)
                {
                    _domainMinPolar = value;
                    OnPropertyChanged("DomainMinPolar");
                }
            }
        }

        private float _domainMaxPolar;
        public float DomainMaxPolar
        {
            get => _domainMaxPolar;
            set
            {
                if (DomainMaxPolar != value)
                {
                    _domainMaxPolar = value;
                    OnPropertyChanged("DomainMaxPolar");
                }
            }
        }

        private bool _graphDomainPolarDegrees;
        public bool GraphDomainPolarDegrees
        {
            get => _graphDomainPolarDegrees;
            set
            {
                if (GraphDomainPolarDegrees != value)
                {
                    _graphDomainPolarDegrees = value;
                    OnPropertyChanged("GraphDomainPolarDegrees");
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

        private bool ShowXaxis { get => (Elements & Elements.Xaxis) != 0; }
        private bool ShowYaxis { get => (Elements & Elements.Yaxis) != 0; }
        private bool ShowXcal { get => (Elements & Elements.Xcalibration) != 0; }
        private bool ShowYcal { get => (Elements & Elements.Ycalibration) != 0; }
        private bool ShowXticks { get => (Elements & Elements.Xticks) != 0; }
        private bool ShowYticks { get => (Elements & Elements.Yticks) != 0; }
        private bool ShowHlines { get => (Elements & Elements.HorizontalGridLines) != 0; }
        private bool ShowVlines { get => (Elements & Elements.VerticalGridLines) != 0; }

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
            _domainGraphWidth = Defaults.GraphDomainGraphWidth;
            _graphDomainPolarDegrees = Defaults.GraphDomainPolarDegrees;
            // int
            _fillTransparencyPercent = Defaults.GraphFillTransparencyPercent;
            _paperTransparencyPercent = Defaults.GraphPaperTransparencyPercent;
            _stepCount = Defaults.GraphStepCount;
            // float
            _domainMaxCartesian = Defaults.GraphDomainMaxCartesian;
            _domainMaxPolar = Defaults.GraphDomainMaxPolar;
            _domainMinCartesian = Defaults.GraphDomainMinCartesian;
            _domainMinPolar = Defaults.GraphDomainMinPolar;
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
            if (r.Width == 0 || r.Height == 0)
                return; // Nothing to draw!
            switch (Optimization)
            {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    break;
                    g.InterpolationMode = InterpolationMode.Low;
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.SmoothingMode = SmoothingMode.HighSpeed;
                    g.TextRenderingHint = TextRenderingHint.SystemDefault;
                    g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    break;
            }
            g.Transform = GetMatrix(r);
            var penWidth = (Size.Width / r.Width + Size.Height / r.Height);
            InitProxies();
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            for (var call = 1; call <= 2; call++)
            {
                bool fill = call == 1;
                Series.ForEach(s => { if (s.Visible) s.DrawAsync(g, Limits, penWidth, fill, time, PlotType); });
                if (fill)
                    DrawGrid(g, penWidth);
            }
            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine($"Graph.Draw took {stopwatch.Elapsed}");
        }

        private void DrawGrid(Graphics g, float penWidth)
        {
            var limits = Limits;
            float x1 = limits.X, y1 = limits.Bottom, x2 = limits.Right, y2 = limits.Top;
            using (Pen gridPen = new Pen(GridColour, penWidth) { DashStyle = DashStyle.Dot },
                axisPen = new Pen(AxisColour, penWidth))
            using (var font = new Font("Arial", 5 * penWidth))
            using (var format = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Far })
            {
                var brush = Brushes.DarkGray;
                double
                    logX = Math.Log10(Math.Abs(x2 - x1)),
                    logY = Math.Log10(Math.Abs(y2 - y1));
                for (var phase = 0; phase < 4; phase++)
                {
                    var gridPhase = (GridPhase)phase;
                    var vertical = gridPhase == GridPhase.VerticalLines || gridPhase == GridPhase.Yaxis;
                    if (gridPhase == GridPhase.HorizontalLines || gridPhase == GridPhase.VerticalLines)
                    {
                        var log = PlotType != PlotType.Anisotropic || vertical ? logX : logY;
                        var order = Math.Floor(log);
                        var scale = log - order;
                        double increment = scale < 0.3 ? 2 : scale < 0.7 ? 5 : 10;
                        BumpDown(ref increment);
                        BumpDown(ref increment);
                        for (var pass = 0; pass < 3; pass++)
                        {
                            var gridPass = (GridPass)pass;
                            var dy = increment * Math.Pow(10, order - 1);
                            for (var y = 0.0; y <= Math.Max(Math.Abs(y1), Math.Abs(y2)); y += dy)
                            {
                                var pen = gridPass == GridPass.Ticks ? axisPen : gridPen;
                                DrawGridLine(g, pen, font, brush, x1, x2, (float)y, format, vertical, gridPass);
                                if (y != 0.0)
                                    DrawGridLine(g, pen, font, brush, x1, x2, -(float)y, format, vertical, gridPass);
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

        private void BumpDown(ref double value) => value = value == 5 ? 2 : value / 2;
        private void BumpUp(ref double value) => value = value == 2 ? 5 : value * 2;

        private void DrawGridLine(Graphics g, Pen pen, Font font, Brush brush,
            float x1, float x2, float y, StringFormat format, bool vertical, GridPass gridPass)
        {
            if (gridPass == GridPass.Ticks)
            {
                var tickSize = font.Size;
                x1 = (TickStyles & TickStyles.Positive) != 0 ? tickSize : 0;
                x2 = (TickStyles & TickStyles.Negative) != 0 ? tickSize : 0;
            }
            float x = 0, y1 = y, y2 = y, z = -y;
            if (vertical)
            {
                var t = x; x = y; y = t;
                t = x1; x1 = y1; y1 = t;
                t = x2; x2 = y2; y2 = t;
                z = -z;
            }
            switch (gridPass)
            {
                case GridPass.Axes when vertical && ShowYaxis || !vertical && ShowXaxis:
                case GridPass.Lines when vertical && ShowVlines || !vertical && ShowHlines:
                    g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case GridPass.Calibration when vertical && ShowXcal || !vertical && ShowYcal:
                    g.ScaleTransform(1, -1);
                    g.DrawString(z.ToString(), font, brush, x - pen.Width, y + pen.Width, format);
                    g.ScaleTransform(1, -1);
                    break;
                case GridPass.Ticks:
                    if (vertical && ShowXticks || !vertical && ShowYticks)
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
