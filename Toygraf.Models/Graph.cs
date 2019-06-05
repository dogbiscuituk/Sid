namespace ToyGraf.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using ToyGraf.Expressions;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;

    [Serializable]
    public class Graph : Style, IDisposable
    {
        public Graph() { RestoreDefaults(); }

        #region Properties

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
        public DomainInfo DomainInfo;

        public bool DomainGraphWidth
        {
            get => DomainInfo.UseGraphWidth;
            set
            {
                if (DomainGraphWidth != value)
                {
                    DomainInfo.UseGraphWidth = value;
                    OnPropertyChanged("DomainGraphWidth");
                }
            }
        }

        public float DomainMinCartesian
        {
            get => DomainInfo.MinCartesian;
            set
            {
                if (DomainMinCartesian != value)
                {
                    DomainInfo.MinCartesian = value;
                    OnPropertyChanged("DomainMinCartesian");
                }
            }
        }

        public float DomainMaxCartesian
        {
            get => DomainInfo.MaxCartesian;
            set
            {
                if (DomainMaxCartesian != value)
                {
                    DomainInfo.MaxCartesian = value;
                    OnPropertyChanged("DomainMaxCartesian");
                }
            }
        }

        public float DomainMinPolar
        {
            get => DomainInfo.MinPolar;
            set
            {
                if (DomainMinPolar != value)
                {
                    DomainInfo.MinPolar = value;
                    OnPropertyChanged("DomainMinPolar");
                }
            }
        }

        public float DomainMaxPolar
        {
            get => DomainInfo.MaxPolar;
            set
            {
                if (DomainMaxPolar != value)
                {
                    DomainInfo.MaxPolar = value;
                    OnPropertyChanged("DomainMaxPolar");
                }
            }
        }

        public bool DomainPolarDegrees
        {
            get => DomainInfo.PolarDegrees;
            set
            {
                if (DomainPolarDegrees != value)
                {
                    DomainInfo.PolarDegrees = value;
                    OnPropertyChanged("DomainPolarDegrees");
                }
            }
        }

        [JsonIgnore]
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

        [JsonIgnore]
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

        private List<Style> _styles = new List<Style>();
        public List<Style> Styles
        {
            get => _styles;
            set
            {
                _styles = value;
                OnPropertyChanged("Styles");
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

        private List<Trace> _traces = new List<Trace>();
        public List<Trace> Traces
        {
            get => _traces;
            set
            {
                _traces = value;
                OnPropertyChanged("Traces");
            }
        }

        private bool _proxiesValid;

        private Bitmap Reticle;
        private List<Label> Labels = new List<Label>();

        public bool UsesTime
        {
            get
            {
                ValidateProxies();
                return Traces.Any(p => p.Visible && p.UsesTime);
            }
        }

        [NonSerialized]
        private Viewport LastViewport;

        public void Clear()
        {
            RemoveStyleRange(0, Styles.Count);
            RemoveTraceRange(0, Traces.Count);
            RestoreDefaults();
        }

        private void RestoreDefaults()
        {
            // bool
            DomainInfo.UseGraphWidth = Defaults.GraphDomainGraphWidth;
            DomainInfo.PolarDegrees = Defaults.GraphDomainPolarDegrees;
            // int
            _fillTransparencyPercent = Defaults.GraphFillTransparencyPercent;
            _paperTransparencyPercent = Defaults.GraphPaperTransparencyPercent;
            _stepCount = Defaults.GraphStepCount;
            // float
            DomainInfo.MaxCartesian = Defaults.GraphDomainMaxCartesian;
            DomainInfo.MaxPolar = Defaults.GraphDomainMaxPolar;
            DomainInfo.MinCartesian = Defaults.GraphDomainMinCartesian;
            DomainInfo.MinPolar = Defaults.GraphDomainMinPolar;
            _penWidth = Defaults.GraphPenWidth;
            // string
            _title = Defaults.GraphTitle;
            // Color
            _axisColour = Defaults.GraphAxisColour;
            _fillColour1 = Defaults.GraphFillColour1;
            _fillColour2 = Defaults.GraphFillColour2;
            _reticleColour = Defaults.GraphReticleColour;
            _limitColour = Defaults.GraphLimitColour;
            _paperColour = Defaults.GraphPaperColour;
            _penColour = Defaults.GraphPenColour;
            // BrushType
            _brushType = Defaults.GraphBrushType;
            // DashStyle
            _penStyle = Defaults.GraphPenStyle;
            // Elements
            _elements = Defaults.GraphElements;
            // HatchStyle
            _hatchStyle = Defaults.GraphHatchStyle;
            // Interpolation
            _interpolation = Defaults.GraphInterpolation;
            // LinearGradientMode
            _gradientMode = Defaults.GraphGradientMode;
            //Optimization
            _optimization = Defaults.GraphOptimization;
            // PlotType
            _plotType = Defaults.GraphPlotType;
            // PointF
            OriginalCentre = Defaults.GraphViewport.Centre;
            // SizeF
            OriginalWidth = Defaults.GraphViewport.Width;
            // TickStyles
            _tickStyles = Defaults.GraphTickStyles;
            // Viewport
            Viewport = Defaults.GraphViewport;
            // WrapMode
            _wrapMode = Defaults.GraphWrapMode;
        }

        private bool Updating;

        #endregion

        #region Style Management

        public Style AddStyle()
        {
            var Style = NewStyle();
            AddStyle(Style);
            return Style;
        }

        public void AddStyle(Style Style)
        {
            Styles.Add(Style);
            if (!Updating)
                OnPropertyChanged("Styles");
        }

        public Style InsertStyle(int index)
        {
            var style = NewStyle();
            InsertStyle(index, style);
            return style;
        }

        public void InsertStyle(int index, Style Style)
        {
            Styles.Insert(index, Style);
            OnPropertyChanged("Styles");
        }

        public Style NewStyle()
        {
            var Style = new Style(this);
            Style.PropertyChanged += Style_PropertyChanged;
            return Style;
        }

        public void RemoveStyle(int index)
        {
            if (index >= 0 && index < Styles.Count)
            {
                Styles.RemoveAt(index);
                OnPropertyChanged("Styles");
            }
        }

        public void RemoveStyleRange(int index, int count)
        {
            while (count-- > 0)
                RemoveStyle(index + count);
        }

        #endregion

        #region Trace Management

        public Trace AddTrace()
        {
            var trace = NewTrace();
            AddTrace(trace);
            return trace;
        }

        public void AddTrace(Trace trace)
        {
            Traces.Add(trace);
            if (!Updating)
                OnPropertyChanged("Traces");
        }

        public Trace InsertTrace(int index)
        {
            var trace = NewTrace();
            InsertTrace(index, trace);
            return trace;
        }

        public void InsertTrace(int index, Trace trace)
        {
            Traces.Insert(index, trace);
            OnPropertyChanged("Traces");
        }

        public Trace NewTrace()
        {
            var trace = new Trace(this);
            trace.PropertyChanged += Trace_PropertyChanged;
            return trace;
        }

        public void RemoveTrace(int index)
        {
            if (index >= 0 && index < Traces.Count)
            {
                Traces.RemoveAt(index);
                OnPropertyChanged("Traces");
            }
        }

        public void RemoveTraceRange(int index, int count)
        {
            while (count-- > 0)
                RemoveTrace(index + count);
        }

        #endregion

        #region Drawing

        public void Draw(Graphics g, Rectangle r, double time)
        {
            InitOptimization(g);
            using (var m = GetMatrix(r))
                g.Transform = m;
            Viewport.SetRatio(r.Size);
            if (LastViewport != Viewport)
            {
                InvalidateReticle();
                LastViewport = Viewport;
            }
            var penWidth = Width / r.Width + Viewport.Height / r.Height;
            ValidateProxies();
            Traces.ForEach(s =>
            {
                if (s.Visible)
                    s.DrawAsync(g, DomainInfo, Viewport, penWidth, true, time, PlotType, Interpolation);
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
            Traces.ForEach(s =>
            {
                if (s.Visible)
                    s.DrawAsync(g, DomainInfo, Viewport, penWidth, false, time, PlotType, Interpolation);
            });
        }

        public List<Expression> GetProxies()
        {
            ValidateProxies();
            return Traces.Select(p => p.Proxy).ToList();
        }

        private void InitProxies()
        {
            var count = Traces.Count;
            var hit = new bool[count, count];
            for (int row = 0; row < count; row++)
            {
                var matches = Regex.Matches(Traces[row].Formula, @"[fF](\d+)");
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
            var refs = Traces.Select(p => p.Expression).ToArray();
            for (int index = 0; index < count; index++)
            {
                Traces[index].Proxy = hit[index, index]
                    ? Expressions.DefaultVoid
                    : Traces[index].Expression.AsProxy(Expressions.x, Expressions.t, refs);
            }
        }

        public void InvalidateReticle() { DisposeReticle(); Labels.Clear(); }
        private void InvalidatePoints() => Traces.ForEach(p => p.InvalidatePaths());
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
                g2.DrawReticle(Labels, new ReticleInfo(PlotType, Viewport, DomainInfo,
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
            using (var m = GetMatrix(r))
                m.TransformPoints(points);
            var q = points[0];
            return new Point((int)Math.Round(q.X), (int)Math.Round(q.Y));
        }

        public PointF ClientToGraph(Point p, Rectangle r)
        {
            var points = new[] { new PointF(p.X, p.Y) };
            using (var m = GetMatrix(r))
            {
                m.Invert();
                m.TransformPoints(points);
            }
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

        private void InitOptimization(Graphics g) => g.SetOptimization(Optimization);

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

        protected virtual void Dispose(bool disposing) => DisposeReticle();

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

        private int UpdateCount;

        public event EventHandler BeginUpdate, EndUpdate;

        public void OnBeginUpdate()
        {
            if (UpdateCount == 0)
            {
                Updating = true;
                BeginUpdate?.Invoke(this, EventArgs.Empty);
            }
            UpdateCount++;
        }

        public void OnEndUpdate()
        {
            if (UpdateCount > 0)
            {
                UpdateCount--;
                if (UpdateCount == 0)
                {
                    Updating = false;
                    EndUpdate?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            InvalidateReticle();
            base.OnPropertyChanged(propertyName);
        }

        public void Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged($"Styles[{Styles.IndexOf((Style)sender)}].{e.PropertyName}");
        }

        public void Trace_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Formula")
                InvalidateProxies();
            OnPropertyChanged($"Traces[{Traces.IndexOf((Trace)sender)}].{e.PropertyName}");
        }

        #endregion
    }
}
