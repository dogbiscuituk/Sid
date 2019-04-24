namespace Sid.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    [Serializable]
    public class Graph : INotifyPropertyChanged
    {
        public Graph()
        {
            InitDefaults();
        }

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

        private RectangleF Limits => new RectangleF(Location, Size);

        public int StepCount { get; set; }

        private List<Series> _series = new List<Series>();
        private Color _paperColour;
        private Color _axisColour;
        private Color _gridColour;
        private Color _penColour;
        private Color _fillColour;
        private Color _limitColour;
        private PointF _location, _originalLocation = new PointF(-10, -5);
        private SizeF _size, _originalSize = new SizeF(20, 10);

        public List<Series> Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged("Series");
            }
        }

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
            for (int index = Series.Count; index > 0;)
                RemoveSeriesAt(--index);
            InitDefaults();
        }

        public void RemoveSeriesAt(int index)
        {
            if (index < 0 || index >= Series.Count)
                return;
            var series = Series[index];
            series.PropertyChanged -= Series_PropertyChanged;
            Series.RemoveAt(index);
            OnPropertyChanged("Series");
        }

        public void RemoveSeriesRange(int index, int count)
        {
            while (count-- > 0)
                RemoveSeriesAt(index + count);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            System.Diagnostics.Debug.WriteLine($"Graph.OnPropertyChanged(\"{propertyName}\")");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Series_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Series[{Series.IndexOf((Series)sender)}].{e.PropertyName}");

        public void Draw(Graphics g, Rectangle r)
        {
            if (r.Width == 0 || r.Height == 0)
                return; // Nothing to draw!
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Transform = GetMatrix(r);
            using (var brush = new SolidBrush(PaperColour))
                g.FillRectangle(brush, Limits);
            var penWidth = (Size.Width / r.Width + Size.Height / r.Height);
            Series.ForEach(s => { if (s.Visible) s.Draw(g, Limits, penWidth, fill: true); });
            DrawGrid(g, penWidth);
            Series.ForEach(s => { if (s.Visible) s.Draw(g, Limits, penWidth, fill: false); });
        }

        public PointF ScreenToGraph(Point p, Rectangle r)
        {
            var points = new[] { new PointF(p.X, p.Y) };
            var matrix = GetMatrix(r);
            matrix.Invert();
            matrix.TransformPoints(points);
            return points[0];
        }

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
                        step = (size < 0.3 ? 2 : size < 0.7 ? 5 : 10) * Math.Pow(10, step - 1);
                        for (var y = step; y <= Math.Max(Math.Abs(y1), Math.Abs(y2)); y += step)
                        {
                            DrawGridLine(g, gridPen, font, brush, x1, x2, (float)y, format, vertical);
                            DrawGridLine(g, gridPen, font, brush, x1, x2, -(float)y, format, vertical);
                        }
                    }
                    else
                        DrawGridLine(g, axisPen, font, brush, x1, x2, 0, format, vertical);
                    var t = x1; x1 = y1; y1 = t;
                    t = x2; x2 = y2; y2 = t;
                }
            }
        }

        private void DrawGridLine(Graphics g, Pen pen, Font font, Brush brush,
            float x1, float x2, float y, StringFormat format, bool vertical)
        {
            float x = 0, y1 = y, y2 = y, z = -y;
            if (vertical)
            {
                var t = x; x = y; y = t;
                t = x1; x1 = y1; y1 = t;
                t = x2; x2 = y2; y2 = t;
                z = -z;
            }
            g.DrawLine(pen, x1, y1, x2, y2);
            g.ScaleTransform(1, -1);
            g.DrawString(z.ToString(), font, brush, x - pen.Width, y + pen.Width, format);
            g.ScaleTransform(1, -1);
        }

        private Matrix GetMatrix(Rectangle r)
        {
            return new Matrix(Limits,
                new[] { new PointF(r.Left, r.Bottom), new PointF(r.Right, r.Bottom), r.Location });
        }

        private void InitDefaults()
        {
            ZoomReset();
            StepCount = 16000;
            PaperColour = Color.LightYellow;
            AxisColour = Color.DarkGray;
            GridColour = Color.LightGray;
            PenColour = Color.Black;
            FillColour = Color.Transparent;
            LimitColour = Color.DarkGray;
        }
    }
}
