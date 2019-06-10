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
            if (!TryPlotPointsPartial(points, paths))
                PlotPointsSplit(points, paths);
        }

        private void PlotPointsPartial(IEnumerable<PointF> points, List<GraphicsPath> paths)
        {
            var p = points.ToArray();
            var path = new GraphicsPath();
            if (Interpolation == Interpolation.Linear)
                path.AddLines(p);
            else
                path.AddCurve(p);
            PlotPath(path);
            paths.Add(path);
        }

        private void PlotPointsSplit(IEnumerable<PointF> points, List<GraphicsPath> paths)
        {
            var n = points.Count();
            if (n < 7)
                return;
            var p = n / 2;
            IEnumerable<PointF>
                left = points.Take(p + 1),
                right = points.Skip(p).Take(n - p);
            if (TryPlotPointsPartial(left, paths))
                PlotPointsSplit(right, paths);
            else
            {
                PlotPointsSplit(left, paths);
                PlotPoints(right, paths);
            }
        }

        private bool TryPlotPointsPartial(IEnumerable<PointF> points, List<GraphicsPath> paths)
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
                PlotPointsPartial(p, paths);
                return true;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        #endregion
    }
}
