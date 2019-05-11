namespace ToyGraf.Models
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class Grid
    {
        /// <summary>
        /// Draw all components of a rectangular or polar grid. In rendering order these are the grid "wires"
        /// (orthogonal lines in the rectangular case, arcs and radial spokes in the polar), axis tick marks,
        /// axis numbering (calibration), and the axes themselves. The rendering process comprises a total of
        /// eight stages, controlled by the enumerations GridPhase and GridPass, in the sequence shown in the
        /// table below.
        /// 
        ///  Stage  GridPhase      GridPass     What is rendered?
        /// -------------------------------------------------------------------------------------------------
        ///    1    DomainMarks    GridWires    Horizontal dotted lines (Cartesian) or radial spokes (Polar).
        ///    2    DomainMarks    AxisTicks    Ticks along the Y axis.
        ///    3    DomainMarks    Numbering    Numbers on the Y axis.
        ///    4    RangeMarks     GridWires    Vertical dotted lines (Cartesian) or circular arcs (Polar).
        ///    5    RangeMarks     AxisTicks    Ticks along the X axis.
        ///    6    RangeMarks     Numbering    Numbers on the X axis.
        ///    7    Xaxis          AxisWires    The X axis.
        ///    8    Yaxis          AxisWires    The Y axis.
        /// -------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="g">The GDI+ output Graphics object.</param>
        /// <param name="info">A struct containing iscellaneous information about the Grid being drawn.</param>
        public static void DrawGrid(this Graphics g, GridInfo info)
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
                    info.Vertical = phase == GridPhase.DomainMarks || phase == GridPhase.Yaxis;
                    if ((phase & GridPhase.Xaxis) != 0)
                        DrawWire(g, axisPen, font, brush, format, info, GridPass.AxisWires, x1, x2, 0);
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
                                if (phase == GridPhase.RangeMarks && pass == GridPass.GridWires)
                                {
                                    x0 = (float)ymax;
                                    dy = 10;
                                    ymax = 180 - dy;
                                }
                            }
                            for (var y = 0.0; y <= ymax; y += dy)
                            {
                                var pen = pass == GridPass.AxisTicks ? axisPen : gridPen;
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

        private const float piOver180 = (float)Math.PI / 180;

        private static void BumpDown(ref double value) => value = value == 5 ? 2 : value / 2;
        private static void BumpUp(ref double value) => value = value == 2 ? 5 : value * 2;

        private static void DrawWire(Graphics g, Pen pen, Font font, Brush brush, StringFormat format,
            GridInfo info, GridPass pass, float x1, float x2, float y)
        {
            var vp = info.Viewport;
            var clip =
                !info.Vertical && (y < vp.Top || y > vp.Bottom) ||
                info.Vertical && (y < vp.Left || y > vp.Right);
            if (pass == GridPass.AxisTicks)
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
                case GridPass.AxisWires when info.Axis:
                case GridPass.GridWires when info.Wires && !info.Polar:
                case GridPass.AxisTicks when info.Ticks:
                    if (!clip)
                        g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case GridPass.GridWires when info.Hwires && info.Polar:
                    if (y >= 0)
                        DrawWireSpoke(g, pen, x1, y);
                    break;
                case GridPass.GridWires when info.Vwires && info.Polar:
                    if (x1 >= 0)
                        DrawWireArc(g,pen, info, x1);
                    break;
                case GridPass.Numbering when info.Calibration:
                    DrawWireCalibration(g, pen, brush, font, format, x, y, z);
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
        /// <param name="info">A struct containing iscellaneous information about the Grid being drawn.</param>
        /// <param name="r">The radius of the arc.</param>
        private static void DrawWireArc(Graphics g, Pen pen, GridInfo info, float r)
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
                    startDegrees = Math.Atan2(p1.Y, p1.X) / piOver180,
                    sweepDegrees = Math.Atan2(p2.Y, p2.X) / piOver180 - startDegrees;
                if (sweepDegrees < 0) sweepDegrees += 360;
                g.DrawArc(pen, -r, -r, 2 * r, 2 * r, (float)startDegrees, (float)sweepDegrees);
            }
        }

        private static void DrawWireCalibration(Graphics g, Pen pen, Brush brush, Font font,
            StringFormat format, float x, float y, float z)
        {
            // The temporary inversion of the Y axis is necessitated by the use
            // of conventional mathematical (rather than computer graphic) axes.
            g.ScaleTransform(1, -1);
            g.DrawString(z.ToString(), font, brush, x - pen.Width, y + pen.Width, format);
            g.ScaleTransform(1, -1);
        }

        private static void DrawWireSpoke(Graphics g, Pen pen, float r, float θ)
        {
            θ *= piOver180; // Convert degrees to radians.
            float x = (float)(r * Math.Cos(θ)), y = (float)(r * Math.Sin(θ));
            g.DrawLine(pen, -x, -y, +x, +y);
        }

        private static void Swap(ref float x, ref float y) { var t = x; x = y; y = t; }
    }
}
