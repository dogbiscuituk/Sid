namespace Sid.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using Newtonsoft.Json;

    [Serializable]
    public class Graph : INotifyPropertyChanged
    {
        public Graph() { RestoreDefaults(); }

        #region Properties

        private Elements _elements = Elements.All & ~Elements.GridLines;
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

        private bool ShowPaper { get => (Elements & Elements.Paper) != 0; }
        private bool ShowXaxis { get => (Elements & Elements.AxisX) != 0; }
        private bool ShowYaxis { get => (Elements & Elements.AxisY) != 0; }
        private bool ShowXcal { get => (Elements & Elements.NumberingX) != 0; }
        private bool ShowYcal { get => (Elements & Elements.NumberingY) != 0; }
        private bool ShowHlines { get => (Elements & Elements.HorizontalGridLines) != 0; }
        private bool ShowVlines { get => (Elements & Elements.VerticalGridLines) != 0; }

        private Color _paperColour;
        [DefaultValue(typeof(Color), "LightYellow")]
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

        private Color _axisColour;
        [DefaultValue(typeof(Color), "DarkGray")]
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
        [DefaultValue(typeof(Color), "LightGray")]
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
        [DefaultValue(typeof(Color), "Black")]
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
        [DefaultValue(typeof(Color), "Transparent")]
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

        private Color _limitColour;
        [DefaultValue(typeof(Color), "DarkGray")]
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

        private PointF _location, _originalLocation = new PointF(-10, -5);
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

        private SizeF _size, _originalSize = new SizeF(20, 10);
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

        private const int DefaultStepCount = 16000;
        [DefaultValue(DefaultStepCount)]
        public int StepCount { get; set; }

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
            StepCount = DefaultStepCount;
            PaperColour = Color.LightYellow;
            AxisColour = Color.DarkGray;
            GridColour = Color.LightGray;
            PenColour = Color.Black;
            FillColour = Color.Transparent;
            LimitColour = Color.DarkGray;
        }

        #endregion

        #region Series management

        public Series AddSeries()
        {
            var series = new Series();
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

        public void Draw(Graphics g, Rectangle r)
        {
            if (r.Width == 0 || r.Height == 0)
                return; // Nothing to draw!
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Transform = GetMatrix(r);
            if (ShowPaper)
                using (var brush = new SolidBrush(PaperColour))
                    g.FillRectangle(brush, Limits);
            var penWidth = (Size.Width / r.Width + Size.Height / r.Height);
            Series.ForEach(s => { if (s.Visible) s.Draw(g, Limits, penWidth, fill: true); });
            DrawGrid(g, penWidth);
            Series.ForEach(s => { if (s.Visible) s.Draw(g, Limits, penWidth, fill: false); });
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
                for (int phase = 0; phase < 4; phase++)
                {
                    var vertical = (phase & 1) != 0;
                    if (phase < 2)
                    {
                        var size = Math.Log10(Math.Abs(y2 - y1));
                        var step = Math.Floor(size);
                        size -= step;
                        var scale = size < 0.3 ? 2 : size < 0.7 ? 5 : 10;
                        step = scale * Math.Pow(10, step - 1);
                        for (var y = 0.0; y <= Math.Max(Math.Abs(y1), Math.Abs(y2)); y += step)
                        {
                            DrawGridLine(g, gridPen, font, brush, x1, x2, (float)y, format, vertical, false);
                            if (y != 0.0)
                                DrawGridLine(g, gridPen, font, brush, x1, x2, -(float)y, format, vertical, false);
                        }
                    }
                    else
                    {
                        if (vertical && ShowYaxis || !vertical && ShowXaxis)
                            DrawGridLine(g, axisPen, font, brush, x1, x2, 0, format, vertical, true);
                    }
                    var t = x1; x1 = y1; y1 = t;
                    t = x2; x2 = y2; y2 = t;
                }
            }
        }

        private void DrawGridLine(Graphics g, Pen pen, Font font, Brush brush,
            float x1, float x2, float y, StringFormat format, bool vertical, bool isAxis)
        {
            float x = 0, y1 = y, y2 = y, z = -y;
            if (vertical)
            {
                var t = x; x = y; y = t;
                t = x1; x1 = y1; y1 = t;
                t = x2; x2 = y2; y2 = t;
                z = -z;
            }
            if (isAxis || vertical && ShowVlines || !vertical && ShowHlines)
                g.DrawLine(pen, x1, y1, x2, y2);
            g.ScaleTransform(1, -1);
            if (vertical && ShowXcal || !vertical && ShowYcal)
                g.DrawString(z.ToString(), font, brush, x - pen.Width, y + pen.Width, format);
            g.ScaleTransform(1, -1);
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
