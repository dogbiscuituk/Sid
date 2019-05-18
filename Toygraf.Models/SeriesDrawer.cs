namespace ToyGraf.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class SeriesDrawer
    {
        public void Draw(Graphics g, Pen pen, Interpolation interpolation, IEnumerable<PointF> points)
        {
            Pen = pen;
            Filling = false;
            Draw(g, interpolation, points);
        }

        public void Fill(Graphics g, Brush brush, PlotType plotType, Interpolation interpolation, IEnumerable<PointF> points)
        {
            Brush = brush;
            PlotType = plotType;
            Filling = true;
            Draw(g, interpolation, points);
        }

        private void Draw(Graphics g, Interpolation interpolation, IEnumerable<PointF> points)
        {
            Graphics = g;
            Interpolation = interpolation;
            Draw(points);
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

        private void Draw(IEnumerable<PointF> points)
        {
            if (!DrawPart(points))
                DrawSplit(points);
        }

        private void DrawSplit(IEnumerable<PointF> points)
        {
            var n = points.Count();
            if (n < 7)
                return;
            var p = n / 2;
            IEnumerable<PointF>
                left = points.Take(p + 1),
                right = points.Skip(p).Take(n - p);
            if (DrawPart(left))
                DrawSplit(right);
            else
            {
                DrawSplit(left);
                Draw(right);
            }
        }

        private bool DrawPart(IEnumerable<PointF> points)
        {
            PointF[] p;
            if (Filling)
            {
                var n = points.Count();
                p = new PointF[n + 2];
                points.ToList().CopyTo(p, 1);
                switch (PlotType)
                {
                    case PlotType.Cartesian:
                        p[0] = new PointF(p[1].X, 0);
                        p[n + 1] = new PointF(p[n].X, 0);
                        break;
                    case PlotType.Polar:
                        p[0] = p[n + 1] = new PointF(0, 0);
                        break;
                }
            }
            else
                p = points.ToArray();
            var result = false;
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
