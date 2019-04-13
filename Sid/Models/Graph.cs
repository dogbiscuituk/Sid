namespace Sid.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq.Expressions;
    using System.Xml.Serialization;
    using FormulaBuilder;

    [Serializable]
    public class Graph
    {
        public Graph() : this(new RectangleF(-10, -5, 20, 10), 16000) { }

        public Graph(RectangleF limits, int stepCount)
        {
            Location = limits.Location;
            Size = limits.Size;
            StepCount = stepCount;

            PaperColour = Color.LightYellow;
            AxisColour = Color.DarkGray;
            GridColour = Color.LightGray;
            PenColour = Color.Black;
            FillColour = Color.Transparent;
            LimitColour = Color.DarkGray;

            var x = Expressions.x;
            Expression y = x.Squared().Minus(1).Log();
            AddSeries(y, Color.Black, Color.Yellow);
            y = y.Differentiate();
            AddSeries(y, Color.Red, Color.Orange);
            y = y.Differentiate();
            AddSeries(y, Color.Green, Color.LightGreen);
            y = y.Differentiate();
            AddSeries(y, Color.Blue, Color.AliceBlue);
        }

        [XmlIgnore] public Color PaperColour { get; set; }
        [XmlIgnore] public Color AxisColour { get; set; }
        [XmlIgnore] public Color GridColour { get; set; }
        [XmlIgnore] public Color PenColour { get; set; }
        [XmlIgnore] public Color FillColour { get; set; }
        [XmlIgnore] public Color LimitColour { get; set; }

        /// <summary>
        /// XML proxy colour names are spelt incorrectly (without the "u").
        /// </summary>
        public string PaperColor { get => ColorTranslator.ToHtml(PaperColour); set => PaperColour = ColorTranslator.FromHtml(value); }
        public string AxisColor { get => ColorTranslator.ToHtml(AxisColour); set => AxisColour = ColorTranslator.FromHtml(value); }
        public string GridColor { get => ColorTranslator.ToHtml(GridColour); set => GridColour = ColorTranslator.FromHtml(value); }
        public string PenColor { get => ColorTranslator.ToHtml(PenColour); set => PenColour = ColorTranslator.FromHtml(value); }
        public string FillColor { get => ColorTranslator.ToHtml(FillColour); set => FillColour = ColorTranslator.FromHtml(value); }
        public string LimitColor { get => ColorTranslator.ToHtml(LimitColour); set => LimitColour = ColorTranslator.FromHtml(value); }

        private RectangleF Limits => new RectangleF(Location, Size);
        public PointF Location { get; set; }
        public SizeF Size { get; set; }

        public int StepCount { get; set; }

        private List<Series> _series = new List<Series>();
        public List<Series> Series
        {
            get => _series;
            set => _series = value;
        }

        public Series AddSeries(Expression formula) => AddSeries(formula, PenColour);

        public Series AddSeries(Expression formula, Color penColour) =>
            AddSeries(formula, penColour, FillColour);

        public Series AddSeries(Expression formula, Color penColour, Color fillColour) =>
            AddSeries(formula, penColour, fillColour, LimitColour);

        public Series AddSeries(Expression formula, Color penColour, Color fillColour, Color limitColour)
        {
            var series = new Series(formula, StepCount, penColour, fillColour, limitColour);
            Series.Add(series);
            return series;
        }

        public void Clear()
        {
            Series.Clear();
        }

        public void Draw(Graphics g, Rectangle r)
        {
            if (r.Width == 0 || r.Height == 0)
                return; // Nothing to draw!
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Transform = GetMatrix(r);
            using (var brush = new SolidBrush(PaperColour))
                g.FillRectangle(brush, Limits);
            var penWidth = (Size.Width / r.Width + Size.Height / r.Height);
            Series.ForEach(s => s.Draw(g, Limits, penWidth, fill: true));
            DrawGrid(g, penWidth);
            Series.ForEach(s => s.Draw(g, Limits, penWidth, fill: false));
        }

        public PointF ScreenToGraph(Point p, Rectangle r)
        {
            var points = new[] { new PointF(p.X, p.Y) };
            var matrix = GetMatrix(r);
            matrix.Invert();
            matrix.TransformPoints(points);
            return points[0];
        }

        public void Zoom(float factor)
        {
            Zoom(factor, factor);
        }

        public void Zoom(float xFactor, float yFactor)
        {
            Location = new PointF(Location.X - Size.Width * (xFactor - 1) / 2, Location.Y - Size.Height * (yFactor - 1) / 2);
            Size = new SizeF(Size.Width * xFactor, Size.Height * yFactor);
        }

        public void Scroll(float xFactor, float yFactor)
        {
            Location = new PointF(Location.X + Size.Width * xFactor, Location.Y + Size.Height * yFactor);
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
    }
}
