using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using FormulaBuilder;

namespace FormulaGrapher
{
    public class Graph
    {
        public Graph(RectangleF limits, int stepCount)
        {
            Limits = limits;
            StepCount = stepCount;
        }

        public Series AddSeries(Expression formula)
        {
            Debug.WriteLine(formula.AsString());
            var series = new Series(formula, StepCount);
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
            g.FillRectangle(Brushes.LightYellow, Limits);
            var penWidth = (Limits.Width / r.Width + Limits.Height / r.Height);
            DrawGrid(g, penWidth);
            Series.ForEach(s => s.Draw(g, Limits, penWidth));
        }

        public PointF ScreenToGraph(Point p, Rectangle r)
        {
            var points = new[] { new PointF(p.X, p.Y) };
            var matrix = GetMatrix(r);
            matrix.Invert();
            matrix.TransformPoints(points);
            return points[0];
        }

        private RectangleF Limits;
        private int StepCount;
        private List<Series> Series = new List<Series>();

        private void DrawGrid(Graphics g, float penWidth)
        {
            float x1 = Limits.Left, y1 = Limits.Bottom, x2 = Limits.Right, y2 = Limits.Top;
            using (Pen gridPen = new Pen(Color.LightGray, penWidth) { DashStyle = DashStyle.Dot },
                axisPen = new Pen(Color.DarkGray, penWidth))
            using (var font = new Font("Arial", 5 * penWidth))
            using (var format = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Far })
            {
                var brush = Brushes.DarkGray;
                for (int phase = 0; phase < 4; phase++)
                {
                    var vertical = (phase & 1) != 0;
                    if (phase < 2)
                    {
                        var size = Math.Log10(Math.Abs(x2 - x1));
                        var step = Math.Floor(size);
                        size -= step;
                        step = Math.Pow(10, step);
                        if (size < 0.3) step /= 5;
                        else if (size < 0.7) step /= 2;
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
