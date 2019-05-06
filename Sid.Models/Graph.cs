namespace Sid.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Linq.Expressions;
    using Newtonsoft.Json;
    using Sid.Expressions;

    [Serializable]
    public class Graph : INotifyPropertyChanged
    {
        public Graph() { RestoreDefaults(); }

        #region Properties

        private Color _paperColour = Defaults.GraphPaperColour;
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

        private int _paperTransparencyPercent = Defaults.GraphPaperTransparencyPercent;
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

        private Color _axisColour = Defaults.GraphAxisColour;
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

        private Color _gridColour = Defaults.GraphGridColour;
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

        private Color _penColour = Defaults.GraphPenColour;
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

        private Color _fillColour = Defaults.GraphFillColour;
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

        private int _fillTransparencyPercent = Defaults.GraphFillTransparencyPercent;
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

        private Color _limitColour = Defaults.GraphLimitColour;
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

        private PointF _location, _originalLocation = Defaults.GraphLocation;
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

        private SizeF _size, _originalSize = Defaults.GraphSize;
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

        private bool _isotropic = Defaults.GraphIsotropic;
        public bool Isotropic
        {
            get => _isotropic;
            set
            {
                if (Isotropic != value)
                {
                    _isotropic = value;
                    OnPropertyChanged("Isotropic");
                }
            }
        }

        [JsonIgnore]
        public RectangleF Limits { get => new RectangleF(Location, Size); }

        private Elements _elements = Defaults.GraphElements;
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

        private TickStyles _tickStyles = Defaults.GraphTickStyles;
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

        private int _stepCount = Defaults.GraphStepCount;
        public int StepCount
        {
            get => _stepCount;
            set
            {
                if (StepCount != value)
                {
                    _stepCount = value;
                    OnPropertyChanged("StepCount");
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
            ZoomReset();
            AxisColour = Defaults.GraphAxisColour;
            FillColour = Defaults.GraphFillColour;
            GridColour = Defaults.GraphGridColour;
            LimitColour = Defaults.GraphLimitColour;
            PaperColour = Defaults.GraphPaperColour;
            PenColour = Defaults.GraphPenColour;
            StepCount = Defaults.GraphStepCount;
            FillTransparencyPercent = Defaults.GraphFillTransparencyPercent;
            PaperTransparencyPercent = Defaults.GraphPaperTransparencyPercent;
            Location = Defaults.GraphLocation;
            Size = Defaults.GraphSize;
            Isotropic = Defaults.GraphIsotropic;
            Elements = Defaults.GraphElements;
            TickStyles = Defaults.GraphTickStyles;
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

        #region Composition

        private List<Expression> Proxies = new List<Expression>();

        private void Fixup()
        {
            Proxies.Clear();
            Proxies.AddRange(Series.Select(p => Fixup(p.Expression, Expressions.x, Expressions.t)));
            return;
        }

        private Expression Fixup(Expression e, Expression x, Expression t)
        {
            if (e == Expressions.x)
                return x == Expressions.x ? x : Fixup(x, Expressions.x, Expressions.t);
            if (e == Expressions.t)
                return t == Expressions.t ? t : Fixup(t, Expressions.x, Expressions.t);
            if (e == Expressions.t) return Fixup(t, Expressions.x, Expressions.t);
            if (e is UnaryExpression u)
                return Expression.MakeUnary(u.NodeType, Fixup(u.Operand, x, t), u.Type);
            if (e is MethodCallExpression m)
            {
                var methodName = m.Method.Name;
                if (methodName == "Udf")
                    return Fixup(
                        Series[(int)((ConstantExpression)m.Arguments[0]).Value].Expression,
                        m.Arguments[1],
                        m.Arguments[2]);
                return methodName.Function(Fixup(m.Arguments[0], x, t));
            }
            if (e is BinaryExpression b)
                return Expression.MakeBinary(b.NodeType, Fixup(b.Left, x, t), Fixup(b.Right, x, t));
            return e;
        }

        #endregion

        #region Drawing

        public void Draw(Graphics g, Rectangle r, double time)
        {
            // && (LastTime == time || !Expression.UsesTime())

            if (r.Width == 0 || r.Height == 0)
                return; // Nothing to draw!
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Transform = GetMatrix(r);
            using (var brush = new SolidBrush(PaperColour))
                g.FillRectangle(brush, Limits);
            var penWidth = (Size.Width / r.Width + Size.Height / r.Height);

            Fixup();

            Series.ForEach(s => { if (s.Visible) s.Draw(g, Limits, penWidth, true, time, PlotType); });
            DrawGrid(g, penWidth);
            Series.ForEach(s => { if (s.Visible) s.Draw(g, Limits, penWidth, false, time, PlotType); });
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
                        var log = Isotropic || vertical ? logX : logY;
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
