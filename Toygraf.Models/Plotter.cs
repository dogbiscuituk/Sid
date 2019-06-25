namespace ToyGraf.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using ToyGraf.Models.Enumerations;

    public class Plotter
    {
        #region Public Interface

        public Plotter(Graphics g, FillMode fillMode, Interpolation interpolation,
            List<PointF> points, List<GraphicsPath> drawPaths, bool usePaths)
        {
            Graphics = g;
            FillMode = fillMode;
            Interpolation = interpolation;
            Points = points;
            Paths = drawPaths;
            UsePaths = usePaths;
        }

        public void Draw(Pen pen)
        {
            Pen = pen;
            Plot(false);
        }

        public void Fill(Brush brush, PlotType plotType)
        {
            Brush = brush;
            PlotType = plotType;
            Plot(true);
        }

        #endregion

        #region Private Properties

        private Brush Brush;
        private bool Filling;
        private Pen Pen;
        private PlotType PlotType;

        private readonly FillMode FillMode;
        private readonly Graphics Graphics;
        private readonly Interpolation Interpolation;
        private readonly List<GraphicsPath> Paths;
        private readonly List<PointF> Points;
        private readonly bool UsePaths;

        #endregion

        #region Private Methods

        private void Plot(bool filling)
        {
            Filling = filling;
            if (UsePaths)
                foreach (var path in Paths)
                    PlotPath(path);
            else
                PlotPoints(Points);
        }

        private void PlotPath(GraphicsPath path)
        {
            if (Filling)
                Graphics.FillPath(Brush, path);
            else
                Graphics.DrawPath(Pen, path);
        }

        private void PlotPoints(IEnumerable<PointF> points)
        {
            if (!TryPlotPointsPartial(points))
                PlotPointsSplit(points);
        }

        private void PlotPointsPartial(IEnumerable<PointF> points)
        {
            var p = GetPointsArray(points);
            var path = new GraphicsPath(FillMode);
            if (Interpolation == Interpolation.Linear)
                path.AddLines(p);
            else
                path.AddCurve(p);
            PlotPath(path);
            Paths.Add(path);
        }

        private void PlotPointsSplit(IEnumerable<PointF> points)
        {
            var n = points.Count();
            if (n < 7)
                return;
            var p = n / 2;
            IEnumerable<PointF>
                left = points.Take(p + 1),
                right = points.Skip(p).Take(n - p);
            if (TryPlotPointsPartial(left))
                PlotPointsSplit(right);
            else
            {
                PlotPointsSplit(left);
                PlotPoints(right);
            }
        }

        private bool TryPlotPointsPartial(IEnumerable<PointF> points)
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
                p[++n] = new PointF(x2, 0);
            }
            else
                p = points.ToArray();
            try
            {
                PlotPointsPartial(p);
                return true;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        private PointF[] GetPointsArray(IEnumerable<PointF> points)
        {
            switch (Interpolation)
            {
                case Interpolation.Linear:
                    // Inspect every group of 3 adjacent points,
                    // and if their Y coordinates are all equal,
                    // remove the middle point from the list.
                    var list = new List<PointF>(points.Take(2));
                    float y0 = list[0].Y, y1 = list[1].Y;
                    foreach (var p in points.Skip(2))
                        if (p.Y == y1 && y1 == y0)
                            list[list.Count - 1] = p;
                        else
                        {
                            list.Add(p);
                            y0 = y1;
                            y1 = p.Y;
                        }
                    return list.ToArray();
                default:
                    return points.ToArray();
            }
        }

        #endregion
    }
}
