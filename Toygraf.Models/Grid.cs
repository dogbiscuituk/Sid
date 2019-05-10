namespace ToyGraf.Models
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class Grid
    {
        public static void Draw(Graphics g, GridInfo info)
        {
            var vp = info.Viewport;
            float x1 = vp.Left, y1 = vp.Bottom, x2 = vp.Right, y2 = vp.Top;
            var log = Math.Log10(Math.Abs(x2 - x1));
            var order = Math.Floor(log);
            var scale = log - order;
            using (Pen
                gridPen = new Pen(info.GridColour, info.PenWidth) { DashStyle = DashStyle.Dot },
                axisPen = new Pen(info.AxisColour, info.PenWidth))
            using (var brush = new SolidBrush(info.AxisColour))
            using (var font = new Font("Arial", 5 * info.PenWidth))
            using (var format = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Far })
                for (var phase = (GridPhase)0; (int)phase < 4; phase++)
                {
                    double
                        maX = Math.Max(Math.Abs(x1), Math.Abs(x2)),
                        maY = Math.Max(Math.Abs(y1), Math.Abs(y2));
                    info.Vertical = phase == GridPhase.VerticalWires || phase == GridPhase.Yaxis;
                    if ((phase & GridPhase.Xaxis) != 0)
                        DrawWire(g, axisPen, font, brush, format, info, GridPass.Axes, x1, x2, 0);
                    else
                    {
                        double increment = scale < 0.3 ? 2 : scale < 0.7 ? 5 : 10;
                        BumpDown(ref increment);
                        BumpDown(ref increment);
                        for (var pass = (GridPass)0; (int)pass < 3; pass++)
                        {
                            var x0 = x1;
                            var dy = increment * Math.Pow(10, order - 1);
                            var ymax = maY;
                            if (info.Polar)
                            {
                                ymax = Math.Sqrt(maX * maX + maY * maY);
                                if (phase == GridPhase.HorizontalWires && pass == GridPass.Wires)
                                {
                                    x0 = (float)ymax;
                                    dy = 10;
                                    ymax = 180 - dy;
                                }
                            }
                            for (var y = 0.0; y <= ymax; y += dy)
                            {
                                var pen = pass == GridPass.Ticks ? axisPen : gridPen;
                                DrawWire(g, pen, font, brush, format, info, pass, x0, x2, (float)y);
                                if (y != 0.0)
                                    DrawWire(g, pen, font, brush, format, info, pass, x0, x2, -(float)y);
                            }
                            BumpUp(ref increment);
                        }
                    }
                    Swap(ref x1, ref y1);
                    Swap(ref x2, ref y2);
                }
        }

        private static void BumpDown(ref double value) => value = value == 5 ? 2 : value / 2;
        private static void BumpUp(ref double value) => value = value == 2 ? 5 : value * 2;

        private static void DrawWire(Graphics g, Pen pen, Font font, Brush brush, StringFormat format,
            GridInfo info, GridPass pass, float x1, float x2, float y)
        {
            if (pass == GridPass.Ticks)
            {
                var tickSize = font.Size;
                x1 = info.TickPositive ? tickSize : 0;
                x2 = info.TickNegative ? tickSize : 0;
            }
            float x = 0, y1 = y, y2 = y, z = -y;
            if (info.Vertical)
            {
                Swap(ref x, ref y);
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
                z = -z;
            }
            switch (pass)
            {
                case GridPass.Axes when info.Axis:
                case GridPass.Wires when info.Wires && !info.Polar:
                    g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case GridPass.Wires when info.Hwires && info.Polar:
                    DrawRadialWire(g, pen, x1, y);
                    break;
                case GridPass.Wires when info.Vwires && info.Polar:
                    DrawCircularWire(g,pen, info, x1);
                    break;
                case GridPass.Calibration when info.Calibration:
                    DrawCalibration(g, pen, brush, font, format, x, y, z);
                    break;
                case GridPass.Ticks:
                    if (info.Ticks)
                        g.DrawLine(pen, x1, y1, x2, y2);
                    break;
            }
        }

        private static void DrawCalibration(
            Graphics g, Pen pen, Brush brush, Font font, StringFormat format,
            float x, float y, float z)
        {
            g.ScaleTransform(1, -1);
            g.DrawString(z.ToString(), font, brush, x - pen.Width, y + pen.Width, format);
            g.ScaleTransform(1, -1);
        }

        private static void DrawCircularWire(Graphics g, Pen pen, GridInfo info, float r)
        {
            var vp = info.Viewport;

            // If the circle's bounding square lies wholly outside the viewport, there's nothing to draw!

            if (vp.Left > r || vp.Right < -r || vp.Top > r || vp.Bottom < -r)
                return;

            /*
               If the circle's centre lies outside the viewport, we can draw an arc instead of a full circle.
               This is well worth doing, as GDI+ is slow at "drawing" clipped circles.

               Partition the coordinate space into nine regions: three in the X direction (left, centre, right),
               times three in the Y direction (top, middle, bottom). Note the inversion of the Y axis caused by
               the use of conventional mathematical, rather than computer graphic, axes.

               left centre right               If the circle's centre is inside the viewport, corresponding to
                                               the central region numbered 4 here, we must draw the full circle.
                 6  |  7  |  8    bottom       If it's outside, the shadow of the viewport as seen from the
               -----+-----+-----               centre of the circle is bounded by two of the viewport corners
                 3  |  4  |  5    middle       (possibly adjacent, possibly opposite). Which two corners? That
               -----+-----+-----               depends on the region, as detailed in the array of point pairs
                 0  |  1  |  2    top          "PointF[,] corners" shown below.

               Once we have these two corners, we have the starting and finishing angles of our arc: these are
               just the arctangents of the corners' coordinates.
            */

            int region =
                (vp.Left > 0 ? 0 : vp.Right > 0 ? 1 : 2) +
                (vp.Top > 0 ? 0 : vp.Bottom > 0 ? 3 : 6);
            PointF[,] corners = {
                { vp.TopRight, vp.BottomLeft },
                { vp.TopRight, vp.TopLeft },
                { vp.BottomRight, vp.TopLeft },
                { vp.TopLeft, vp.BottomLeft },
                { vp.Centre, vp.Centre },
                { vp.BottomRight, vp.TopRight },
                { vp.TopLeft, vp.BottomRight },
                { vp.BottomLeft, vp.BottomRight },
                { vp.BottomLeft, vp.TopRight } };
            var p1 = corners[region, 0];
            if (p1 == vp.Centre) // Full circle required!
                g.DrawEllipse(pen, -r, -r, 2 * r, 2 * r);
            else // An arc will suffice.
            {
                var p2 = corners[region, 1];
                double
                    start = Math.Atan2(p1.Y, p1.X) * 180 / Math.PI,
                    sweep = Math.Atan2(p2.Y, p2.X) * 180 / Math.PI - start;
                if (sweep < 0) sweep += 360;
                g.DrawArc(pen, -r, -r, 2 * r, 2 * r, (float)start, (float)sweep);
            }
        }

        private static void DrawRadialWire(Graphics g, Pen pen, float x, float y)
        {
            var θ = y * Math.PI / 180;
            double sin = Math.Sin(θ), cos = Math.Cos(θ);
            y = (float)(x * sin);
            x = (float)(x * cos);
            g.DrawLine(pen, -x, -y, +x, +y);
        }

        private static void Swap(ref float x, ref float y) { var t = x; x = y; y = t; }
    }
}
