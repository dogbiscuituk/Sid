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

        public void Draw(Graphics g, Pen pen, Interpolation interpolation,
            IEnumerable<PointF> points, List<GraphicsPath> paths, bool usePaths)
        {
            Pen = pen;
            Plot(g, interpolation, false, points, paths, usePaths);
        }

        public void Fill(Graphics g, Brush brush, PlotType plotType, Interpolation interpolation,
            IEnumerable<PointF> points, List<GraphicsPath> paths, bool usePaths)
        {
            Brush = brush;
            PlotType = plotType;
            Plot(g, interpolation, true, points, paths, usePaths);
        }

        #endregion

        #region Private Properties

        private Graphics Graphics;
        private Pen Pen;
        private Brush Brush;
        private PlotType PlotType;
        private Interpolation Interpolation;
        private bool Filling;

        #endregion

        #region Private Methods

        private void Plot(Graphics g, Interpolation interpolation, bool filling,
            IEnumerable<PointF> points, List<GraphicsPath> paths, bool usePaths)
        {
            Graphics = g;
            Interpolation = interpolation;
            Filling = filling;
            if (usePaths)
                foreach (var path in paths)
                    PlotPath(path);
            else
                PlotPoints(points, paths);
            Graphics = null;
            Pen = null;
            Brush = null;
        }

        private void PlotPath(GraphicsPath path)
        {
            if (Filling)
                Graphics.FillPath(Brush, path);
            else
                Graphics.DrawPath(Pen, path);
        }

        private void PlotPoints(IEnumerable<PointF> points, List<GraphicsPath> paths)
        {
            if (!TryPlotPart(points, paths))
                PlotSplit(points, paths);
        }

        private bool PlotPart(IEnumerable<PointF> points, List<GraphicsPath> paths)
        {
            var p = points.ToArray();
            var path = new GraphicsPath();
            if (Interpolation == Interpolation.Linear)
                path.AddLines(p);
            else
                path.AddCurve(p);
            PlotPath(path);
            paths.Add(path);
            return true;
        }

        private void PlotSplit(IEnumerable<PointF> points, List<GraphicsPath> paths)
        {
            var n = points.Count();
            if (n < 7)
                return;
            var p = n / 2;
            IEnumerable<PointF>
                left = points.Take(p + 1),
                right = points.Skip(p).Take(n - p);
            if (TryPlotPart(left, paths))
                PlotSplit(right, paths);
            else
            {
                PlotSplit(left, paths);
                PlotPoints(right, paths);
            }
        }

        private bool TryPlotPart(IEnumerable<PointF> points, List<GraphicsPath> paths)
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
            try { return PlotPart(p, paths); }
            catch (OverflowException) { return false; }
        }

        #endregion
    }
}
