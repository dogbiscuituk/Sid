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

        public Plotter(Graphics g, FillMode fillMode, Interpolation interpolation, List<PointF> points)
        {
            Graphics = g;
            FillMode = fillMode;
            Interpolation = interpolation;
            Points = points;
        }

        public void Draw(Pen pen)
        {
            Pen = pen;
            Plot(Phase.Draw);
        }

        public void Fill(Brush brush, PlotType plotType)
        {
            Brush = brush;
            PlotType = plotType;
            Plot(Phase.Fill);
        }

        #endregion

        #region Private Properties

        private Brush Brush;
        private Pen Pen;
        private Phase Phase;
        private PlotType PlotType;

        private readonly FillMode FillMode;
        private readonly Graphics Graphics;
        private readonly Interpolation Interpolation;
        private readonly List<PointF> Points;

        #endregion

        #region Private Methods

        private void Plot(Phase phase)
        {
            Phase = phase;
            PlotPoints(Points);
        }

        private void PlotPath(GraphicsPath path)
        {
            switch (Phase)
            {
                case Phase.Fill:
                    Graphics.FillPath(Brush, path);
                    break;
                case Phase.Draw:
                    Graphics.DrawPath(Pen, path);
                    break;
            }
        }

        private void PlotPoints(IEnumerable<PointF> points)
        {
            if (!TryPlotPointsPartial(points))
                PlotPointsSplit(points);
        }

        private void PlotPointsPartial(IEnumerable<PointF> points)
        {
            var p = points.ToArray();
            var path = new GraphicsPath(FillMode);
            if (Interpolation == Interpolation.Linear)
                path.AddLines(p);
            else
                path.AddCurve(p);
            PlotPath(path);
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
            PointF[] p = null;
            switch (Phase)
            {
                case Phase.Fill:
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
                    break;
                case Phase.Draw:
                    p = points.ToArray();
                    break;
            }
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

        #endregion
    }
}
