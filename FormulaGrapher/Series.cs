using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using FormulaBuilder;

namespace FormulaGrapher
{
    public class Series
    {
        public Series(Expression formula, int stepCount)
        {
            Func = formula.AsFunction();
            StepCount = stepCount;
        }

        public Color PenColour { get; set; }

        public void Draw(Graphics g, RectangleF limits, float penWidth)
        {
            if (Limits != limits)
            {
                Limits = limits;
                ComputePoints();
            }
            using (var pen = new Pen(PenColour, penWidth))
                PointLists.ForEach(p => g.DrawLines(pen, p.ToArray()));
        }

        private Func<double, double> Func;
        private int StepCount;
        private RectangleF Limits;
        private List<List<PointF>> PointLists = new List<List<PointF>>();

        private void ComputePoints()
        {
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
