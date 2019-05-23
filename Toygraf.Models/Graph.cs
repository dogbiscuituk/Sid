namespace ToyGraf.Models
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
    using ToyGraf.Expressions;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;

    [Serializable]
    public class Graph : IDisposable
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

        private Color _reticleColour;
        public Color ReticleColour
        {
            get => _reticleColour;
            set
            {
                if (ReticleColour != value)
                {
                    _reticleColour = value;
                    OnPropertyChanged("ReticleColour");
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

        [JsonIgnore, NonSerialized]
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

        public PointF OriginalCentre { get; private set; }
        public PointF Centre
        {
            get => Viewport.Centre;
            set
            {
                if (Centre != value)
                {
                    Viewport.Centre = value;
                    OnPropertyChanged("Centre");
                }
            }
        }

        public float OriginalWidth { get; private set; }
        public float Width
        {
            get => Viewport.Width;
            set
            {
                if (Width != value)
                {
                    Viewport.Width = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        [JsonIgnore, NonSerialized]
        public Viewport Viewport = Defaults.GraphViewport;

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

        private bool _proxiesValid;

        private Bitmap Reticle;
        private List<Label> Labels = new List<Label>();

        [NonSerialized]
        private Viewport LastViewport;

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
            _reticleColour = Defaults.GraphReticleColour;
            _limitColour = Defaults.GraphLimitColour;
            _paperColour = Defaults.GraphPaperColour;
            _penColour = Defaults.GraphPenColour;
            // Elements
            _elements = Defaults.GraphElements;
            //Optimization
            _optimization = Defaults.GraphOptimization;
            // PlotType
            _plotType = Defaults.GraphPlotType;
            // Interpolation
            _interpolation = Defaults.GraphInterpolation;
            // PointF
            OriginalCentre = Defaults.GraphViewport.Centre;
            // SizeF
            OriginalWidth = Defaults.GraphViewport.Width;
            // TickStyles
            _tickStyles = Defaults.GraphTickStyles;
            // Viewport
            Viewport = Defaults.GraphViewport;
        }

        #endregion

        #region Series Management

        internal Series NewSeries()
        {
            var series = new Series(this);
            series.PropertyChanged += Series_PropertyChanged;
            return series;
        }

        public Series AddSeries()
        {
            var series = NewSeries();
            AddSeries(series);
            return series;
        }

        public void AddSeries(Series series)
        {
            Series.Add(series);
            OnPropertyChanged("Series");
        }

        public void Clear()
        {
            RemoveSeriesRange(0, Series.Count);
            RestoreDefaults();
        }

        public Series InsertSeries(int index)
        {
            var series = NewSeries();
            InsertSeries(index);
            return series;
        }

        public void InsertSeries(int index, Series series)
        {
            Series.Insert(index, series);
            OnPropertyChanged("Series");
        }

        public void RemoveSeries(int index)
        {
            if (index >= 0 && index < Series.Count)
            {
                Series.RemoveAt(index);
                OnPropertyChanged("Series");
            }
        }

        public void RemoveSeriesRange(int index, int count)
        {
            while (count-- > 0)
                RemoveSeries(index + count);
        }

        #endregion

        #region Drawing

        public void Draw(Graphics g, Rectangle r, double time)
        {
            InitOptimization(g);
            g.Transform = GetMatrix(r);
            Viewport.SetRatio(r.Size);
            if (LastViewport != Viewport)
            {
                InvalidateReticle();
                LastViewport = Viewport;
            }
            var penWidth = Width / r.Width + Viewport.Height / r.Height;
            ValidateProxies();
            Series.ForEach(s =>
            {
                if (s.Visible) s.DrawAsync(g, _domain, Viewport, penWidth, true, time, PlotType, Interpolation);
            });
            ValidateReticle(g, r, penWidth);
            var transform = g.Transform;
            g.ResetTransform();
            g.DrawImageUnscaled(Reticle, 0, 0);
            g.MultiplyTransform(transform);
            using (var brush = new SolidBrush(AxisColour))
            using (var font = new Font("Arial", 5 * penWidth))
            using (var format = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Far })
                Labels.ForEach(p => p.Draw(g, brush, font, format));
            Series.ForEach(s =>
            {
                if (s.Visible) s.DrawAsync(g, _domain, Viewport, penWidth, false, time, PlotType, Interpolation);
            });
        }

        public List<Expression> GetProxies()
        {
            ValidateProxies();
            return Series.Select(p => p.Proxy).ToList();
        }

        private void InitProxies()
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
            {
                Series[index].Proxy = hit[index, index]
                    ? Expressions.DefaultVoid
                    : Series[index].Expression.AsProxy(Expressions.x, Expressions.t, refs);
            }
        }

        public void InvalidateReticle() { DisposeReticle(); Labels.Clear(); }
        private void InvalidatePoints() => Series.ForEach(p => p.InvalidatePoints());
        public void InvalidateProxies() => _proxiesValid = false;

        private void ValidateReticle(Graphics g, Rectangle r, float penWidth)
        {
            if (Reticle == null)
            {
                Reticle = new Bitmap(r.Width, r.Height, g);
                Reticle.MakeTransparent();
                var g2 = Graphics.FromImage(Reticle);
                InitOptimization(g2);
                g2.Transform = g.Transform;
                g2.DrawReticle(Labels, new ReticleInfo(PlotType, Viewport, _domain,
                    AxisColour, ReticleColour, penWidth, Elements, TickStyles));
            }
        }

        public void ValidateProxies()
        {
            if (!_proxiesValid)
            {
                InitProxies();
                _proxiesValid = true;
            }
        }

        public Point GraphToClient(PointF p, Rectangle r)
        {
            var points = new[] { p };
            var matrix = GetMatrix(r);
            matrix.TransformPoints(points);
            var q = points[0];
            return new Point((int)Math.Round(q.X), (int)Math.Round(q.Y));
        }

        public PointF ClientToGraph(Point p, Rectangle r)
        {
            var points = new[] { new PointF(p.X, p.Y) };
            var matrix = GetMatrix(r);
            matrix.Invert();
            matrix.TransformPoints(points);
            return points[0];
        }

        private Matrix GetMatrix(Rectangle r)
        {
            Viewport.SetRatio(r.Size);
            return new Matrix(Viewport.Limits, new[]
            {
                new PointF(r.Left, r.Bottom),
                new PointF(r.Right, r.Bottom), r.Location
            });
        }

        private void InitOptimization(Graphics g)
        {
            switch (Optimization)
            {
                case Optimization.HighSpeed:
                    g.InterpolationMode = InterpolationMode.Low;
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.SmoothingMode = SmoothingMode.HighSpeed;
                    g.TextRenderingHint = TextRenderingHint.SystemDefault;
                    g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    break;
                case Optimization.HighQuality:
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    break;
                default:
                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.CompositingQuality = CompositingQuality.Default;
                    g.SmoothingMode = SmoothingMode.None;
                    g.TextRenderingHint = TextRenderingHint.SystemDefault;
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                    break;
            }
        }

        #endregion

        #region Scroll & Zoom

        public void ZoomSet() { OriginalCentre = Centre; OriginalWidth = Width; }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            DisposeReticle();
        }

        private void DisposeReticle()
        {
            if (Reticle != null)
            {
                Reticle.Dispose();
                Reticle = null;
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            InvalidateReticle();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Series_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Formula")
                InvalidateProxies();
            OnPropertyChanged($"Series[{Series.IndexOf((Series)sender)}].{e.PropertyName}");
        }

        #endregion
    }
}
