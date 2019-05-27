namespace ToyGraf.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using ToyGraf.Models.Enumerations;

    public class Plotter
    {
        public void Draw(Graphics g, Pen pen, Interpolation interpolation, IEnumerable<PointF> points)
        {
            Pen = pen;
            Plot(g, interpolation, false, points);
        }

        public void Fill(Graphics g, Brush brush, PlotType plotType, Interpolation interpolation, IEnumerable<PointF> points)
        {
            Brush = brush;
            PlotType = plotType;
            Plot(g, interpolation, true, points);
        }

        private void Plot(Graphics g, Interpolation interpolation, bool filling, IEnumerable<PointF> points)
        {
            Graphics = g;
            Interpolation = interpolation;
            Filling = filling;
            Plot(points);
            Graphics = null;
            Pen = null;
            Brush = null;
        }

        private Graphics Graphics;
        private Pen Pen;
        private Brush Brush;
        private PlotType PlotType;
        private Interpolation Interpolation;
        private bool Filling;

        private void Plot(IEnumerable<PointF> points)
        {
            if (!PlotPart(points))
                PlotSplit(points);
        }

        private void PlotSplit(IEnumerable<PointF> points)
        {
            var n = points.Count();
            if (n < 7)
                return;
            var p = n / 2;
            IEnumerable<PointF>
                left = points.Take(p + 1),
                right = points.Skip(p).Take(n - p);
            if (PlotPart(left))
                PlotSplit(right);
            else
            {
                PlotSplit(left);
                Plot(right);
            }
        }

        private bool PlotPart(IEnumerable<PointF> points)
        {
            PointF[] p;
            if (Filling)
            {
                var n = points.Count();
                p = new PointF[n + 2];
                points.ToList().CopyTo(p, 1);
                float x1 = 0, x2 = 0;
                if (PlotType == PlotType.Cartesian)
                {
                    x1 = p[1].X;
                    x2 = p[n].X;
                }
                p[0] = new PointF(x1, 0);
                p[n + 1] = new PointF(x2, 0);
            }
            else
                p = points.ToArray();
            var result = false;
            //if (p.Max(q => Math.Abs(q.Y)) < 1e7) // Anything higher risks overflow
            try
            {
                switch (Interpolation)
                {
                    case Interpolation.Linear when !Filling:
                        Graphics.DrawLines(Pen, p);
                        break;
                    case Interpolation.Linear when Filling:
                        Graphics.FillPolygon(Brush, p);
                        break;
                    case Interpolation.CardinalSpline when !Filling:
                        Graphics.DrawCurve(Pen, p);
                        break;
                    case Interpolation.CardinalSpline when Filling:
                        Graphics.FillClosedCurve(Brush, p);
                        break;
                }
                result = true;
            }
            catch (OverflowException) { }
            return result;
        }
    }
}
