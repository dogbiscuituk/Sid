namespace ToyGraf.Models
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using ToyGraf.Expressions;

    public static class Grid
    {
        /// <summary>
        /// Draw all components of a rectangular or polar grid. In rendering order these are the grid "wires"
        /// (orthogonal lines in the rectangular case, arcs and radial spokes in the polar), axis tick marks,
        /// axis numbering (calibration), and the axes themselves. The rendering process comprises a total of
        /// eight stages, by Pass and Phase, in the following sequence:
        /// 
        ///  Stage    Pass       Phase    What is rendered?
        /// -------------------------------------------------------------------------------------------
        ///    1    GridWires      1      Horizontal dotted lines (Cartesian) or radial spokes (Polar).
        ///    2        "          2      Vertical dotted lines (Cartesian) or circular arcs (Polar).
        ///    3    AxisTicks      1      Ticks along the Y axis.
        ///    4        "          2      Ticks along the X axis.
        ///    5    Numbering      1      Numbers on the Y axis.
        ///    6        "          2      Numbers on the X axis.
        ///    7    AxisWires      1      The X axis.
        ///    8        "          2      The Y axis.
        /// -------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="g">The GDI+ output Graphics object.</param>
        /// <param name="info">A struct of miscellaneous information about the Grid.</param>
        public static void DrawGrid(this Graphics g, GridInfo info)
        {
            var vp = info.Viewport;
            float x1 = vp.Left, y1 = vp.Bottom, x2 = vp.Right, y2 = vp.Top;
            var log = Math.Log10(Math.Abs(x2 - x1));
            var order = Math.Floor(log);
            var scale = log - order;
            var power = Math.Pow(10, order - 1);
            using (Pen
                gridPen = new Pen(info.GridColour, info.PenWidth) { DashStyle = DashStyle.Dot },
                axisPen = new Pen(info.AxisColour, info.PenWidth))
            using (var brush = new SolidBrush(info.AxisColour))
            using (var font = new Font("Arial", 5 * info.PenWidth))
            using (var format = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Far })
            {
                double incmin = scale < 0.3 ? 2 : scale < 0.7 ? 5 : 10;
                var incmax = incmin;
                BumpDown(ref incmin);
                BumpDown(ref incmin);
                for (var pass = (GridPass)0; (int)pass <= 3; pass++)
                {
                    for (var phase = 1; phase <= 2; phase++)
                    {
                        double
                            maxAbsX = Math.Max(Math.Abs(x1), Math.Abs(x2)),
                            maxAbsY = Math.Max(Math.Abs(y1), Math.Abs(y2));
                        info.Vertical = phase == 2;
                        switch (pass)
                        {
                            case GridPass.GridWires:
                            case GridPass.AxisTicks:
                            case GridPass.Numbering:
                                float xa = x1, xb = x2;
                                var dy = incmin * power;
                                var ymax = maxAbsY;
                                if (info.Polar)
                                {
                                    ymax = Math.Sqrt(maxAbsX * maxAbsX + maxAbsY * maxAbsY);
                                    if (phase == 1 && pass == GridPass.GridWires)
                                    {
                                        xa = (float)ymax;
                                        xb = (float)(incmax * power);
                                        dy = 5;
                                        ymax = 180 - dy;
                                    }
                                }
                                for (var y = 0.0; y <= ymax; y += dy)
                                {
                                    var pen = pass == GridPass.AxisTicks ? axisPen : gridPen;
                                    g.DrawWire(pen, font, brush, format, info, pass, xa, xb, (float)y);
                                    if (y != 0.0)
                                        g.DrawWire(pen, font, brush, format, info, pass, xa, xb, -(float)y);
                                }
                                break;
                            case GridPass.AxisWires:
                                g.DrawWire(axisPen, font, brush, format, info, GridPass.AxisWires, x1, x2, 0);
                                break;
                        }
                        Swap(ref x1, ref y1);
                        Swap(ref x2, ref y2);
                    }
                    BumpUp(ref incmin);
                }
            }
        }

        private enum GridPass { GridWires, AxisTicks, Numbering, AxisWires }

        private static void BumpDown(ref double value) => value = value == 5 ? 2 : value / 2;
        private static void BumpUp(ref double value) => value = value == 2 ? 5 : value * 2;

        private static void DrawWire(this Graphics g, Pen pen, Font font, Brush brush, StringFormat format,
            GridInfo info, GridPass pass, float x1, float x2, float y)
        {
            var vp = info.Viewport;
            var clip =
                !info.Vertical && (y < vp.Top || y > vp.Bottom) ||
                info.Vertical && (y < vp.Left || y > vp.Right);
            if (pass == GridPass.AxisTicks)
            {
                var tickSize = font.Size / 2;
                x1 = info.TickPositive ? tickSize : 0;
                x2 = info.TickNegative ? -tickSize : 0;
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
                case GridPass.AxisWires when info.Axis:
                case GridPass.GridWires when info.Wires && !info.Polar:
                case GridPass.AxisTicks when info.Ticks:
                    if (!clip)
                        g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case GridPass.GridWires when info.Hwires && info.Polar:
                    if (y >= 0)
                        g.DrawWireSpoke(pen, brush, font, format, info, r1: x1, r2: x2, degrees: y);
                    break;
                case GridPass.GridWires when info.Vwires && info.Polar:
                    if (x1 >= 0)
                        g.DrawWireArc(pen, info, r: x1);
                    break;
                case GridPass.Numbering when info.Calibration:
                    g.DrawWireCalibration(pen, brush, font, format, x, y, label: z.ToString());
                    break;
            }
        }

        /// <summary>
        /// Draw a circular arc (or possibly a full or clipped circle) as part of a polar grid.
        /// 
        /// If the circle's centre lies outside the viewport, we can draw an arc instead of a full circle.
        /// This is worth doing because GDI+ is slow at "drawing" clipped circles.
        ///
        /// Partition the coordinate space into nine regions: three in the X direction (left, centre, right),
        /// times three in the Y direction (top, middle, bottom). The inversion of the Y axis is necessitated
        /// by the use throughout of conventional mathematical (rather than computer graphic) axes.
        ///
        /// left centre right               If the circle's centre is inside the viewport, corresponding to
        ///                                 the central region numbered 4 here, we must draw the full circle.
        ///   6  |  7  |  8    bottom       If it's outside, the shadow of the viewport as seen from the
        /// -----+-----+-----               centre of the circle is bounded by two of the viewport corners
        ///   3  |  4  |  5    middle       (possibly adjacent, possibly opposite). Which two corners? That
        /// -----+-----+-----               depends on the region, as detailed in the array of point pairs
        ///   0  |  1  |  2    top          "PointF[,] corners" in code.
        ///
        /// Once we have those two corners, we have the starting and finishing angles of our arc: these are
        /// just the arctangents of the corners' coordinates.
        /// </summary>
        /// <param name="g">The GDI+ output Graphics object.</param>
        /// <param name="pen">The pen used to draw the grid.</param>
        /// <param name="info">A struct containing miscellaneous information about the Grid being drawn.</param>
        /// <param name="r">The radius of the arc.</param>
        private static void DrawWireArc(this Graphics g, Pen pen, GridInfo info, float r)
        {
            var vp = info.Viewport;
            if (vp.Left > r || vp.Right < -r || vp.Top > r || vp.Bottom < -r)
                return; // Nothing to draw if circle's bounding square is wholly outside viewport.
            int region = (vp.Left > 0 ? 0 : vp.Right > 0 ? 1 : 2) + (vp.Top > 0 ? 0 : vp.Bottom > 0 ? 3 : 6);
            PointF[,] corners = {
                { vp.TopRight, vp.BottomLeft }, { vp.TopRight, vp.TopLeft }, { vp.BottomRight, vp.TopLeft },
                { vp.TopLeft, vp.BottomLeft }, { vp.Centre, vp.Centre }, { vp.BottomRight, vp.TopRight },
                { vp.TopLeft, vp.BottomRight }, { vp.BottomLeft, vp.BottomRight }, { vp.BottomLeft, vp.TopRight } };
            var p1 = corners[region, 0];
            if (p1 == vp.Centre) // Full circle required.
                g.DrawEllipse(pen, -r, -r, 2 * r, 2 * r);
            else // An arc will suffice.
            {
                var p2 = corners[region, 1];
                double
                    startDegrees = Math.Atan2(p1.Y, p1.X).RadiansToDegrees(),
                    sweepDegrees = Math.Atan2(p2.Y, p2.X).RadiansToDegrees() - startDegrees;
                if (sweepDegrees < 0) sweepDegrees += 360;
                g.DrawArc(pen, -r, -r, 2 * r, 2 * r, (float)startDegrees, (float)sweepDegrees);
            }
        }

        private static void DrawWireCalibration(this Graphics g, Pen pen, Brush brush, Font font,
            StringFormat format, float x, float y, string label)
        {
            format.Alignment = StringAlignment.Far;
            format.LineAlignment = StringAlignment.Near;
            g.DrawWireString(label, font, brush, x - pen.Width, y + pen.Width, format);
        }

        private static void DrawWireSpoke(this Graphics g, Pen pen, Brush brush, Font font,
            StringFormat format, GridInfo info, float r1, float r2, float degrees)
        {
            var radians = degrees.DegreesToRadians();
            double c = Math.Cos(radians), s = Math.Sin(radians);
            float x = (float)(r1 * c), y = (float)(r1 * s);
            var major = degrees % 15 == 0;
            pen.DashStyle = major ? DashStyle.Dash : DashStyle.Dot;
            if (major)
                g.DrawLine(pen, -x, -y, +x, +y);
            else
            {
                float xc = r2 * (float)c, yc = r2 * (float)s;
                g.DrawLine(pen, -x, -y, -xc, -yc);
                g.DrawLine(pen, +x, +y, +xc, +yc);
            }
            if (!major || (info.Elements & Elements.Calibration) == 0)
                return;
            var pw = 12 * pen.Width;
            var intercepts = info.Viewport.GetIntercepts(radians, pw);
            string[] rads = new[] {
                "0", "π/12", "π/6", "π/4", "π/3", "5π/12", "π/2",
                "7π/12", "2π/3", "3π/4", "5π/6", "11π/12", "π" };
            foreach (PointF p in intercepts)
            {
                var d = p.Y == 0 && p.X < 0 ? 180 : p.Y >= 0 ? (int)degrees : (int)degrees - 180;
                var label = info.Domain.PolarDegrees ? $"{d}°" : d >= 0 ? rads[d / 15] : $"-{rads[-d / 15]}";
                if (p.X < 0)
                    d -= 180;
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Far;
                g.TranslateTransform(p.X, p.Y);                     // Move origin to the point of printing.
                g.RotateTransform(d);                               // Rotate the paper.
                g.DrawWireString(label, font, brush, 0, 0, format); // Print the label at the new origin.
                g.RotateTransform(-d);                              // Rotate back.
                g.TranslateTransform(-p.X, -p.Y);                   // Return to the old origin.
            }
        }

        private static void DrawWireString(this Graphics g, string s, Font font, Brush brush,
            float x, float y, StringFormat format)
        {
            // The temporary inversion of the Y axis is necessitated by the use
            // of conventional mathematical (rather than computer graphic) axes.
            g.ScaleTransform(1, -1);
            g.DrawString(s, font, brush, x, y, format);
            g.ScaleTransform(1, -1);
        }

        private static void Swap(ref float x, ref float y) { var t = x; x = y; y = t; }
    }
}
