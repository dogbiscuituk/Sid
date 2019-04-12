using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using FormulaBuilder;

namespace FormulaGrapher
{
    public class Series
    {
        public Series(Expression formula, int stepCount, Color lineColour, Color areaColour)
        {
            Func = formula.AsFunction();
            StepCount = stepCount;
            LineColour = lineColour;
            AreaColour = areaColour;
        }

        public Color LineColour { get; set; }
        public Color AreaColour { get; set; }

        public void Draw(Graphics g, RectangleF limits, float penWidth, bool fill)
        {
            if (fill && AreaColour == Color.Transparent)
                return;
            ComputePoints(limits);
            if (fill)
                using (var pen = new Pen(Color.DarkGray, penWidth))
                {
                    pen.DashStyle = DashStyle.Dash;
                    using (var brush = new SolidBrush(AreaColour))
                        PointLists.ForEach(p => FillArea(g, pen, brush, p));
                }
            else
                using (var pen = new Pen(LineColour, penWidth))
                    PointLists.ForEach(p => g.DrawLines(pen, p.ToArray()));
        }

        private void FillArea(Graphics g, Pen pen, Brush brush, List<PointF> p)
        {
            var n = p.Count;
            var polygon = new PointF[n + 2];
            p.CopyTo(polygon);
            polygon[n] = new PointF(polygon[n - 1].X, 0);
            polygon[n + 1] = new PointF(polygon[0].X, 0);
            g.FillPolygon(brush, polygon);
            g.DrawLine(pen, polygon[n - 1], polygon[n]);
            g.DrawLine(pen, polygon[n + 1], polygon[0]);
        }

        private Func<double, double> Func;
        private int StepCount;
        private RectangleF Limits;
        private List<List<PointF>> PointLists = new List<List<PointF>>();

        private void ComputePoints(RectangleF limits)
        {
            if (Limits == limits)
                return;
            Limits = limits;
            PointLists.Clear();
            List<PointF> points = null;
            float
                x1 = Limits.Left, y1 = Limits.Top, y2 = Limits.Bottom,
                w = Limits.Width, h = 8 * Limits.Height;
            var skip = true;
            for (var step = 0; step <= StepCount; step++)
            {
                float x = x1 + step * w / StepCount, y = (float)Func(x);
                if (float.IsInfinity(y) || float.IsNaN(y) || y < y1 - h || y > y2 + h)
                    skip = true;
                else
                {
                    if (skip)
                    {
                        skip = false;
                        points = new List<PointF>();
                        PointLists.Add(points);
                    }
                    points.Add(new PointF(x, y));
                }
            }
            // Every segment of the trace must include at least 2 points.
            PointLists.RemoveAll(p => p.Count < 2);
        }
    }
}
