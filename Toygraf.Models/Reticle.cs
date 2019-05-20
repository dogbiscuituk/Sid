namespace ToyGraf.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using ToyGraf.Expressions;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;

    public static class Reticle
    {
        /// <summary>
        /// Draw all components of a rectangular or polar reticle. In rendering order, these are
        /// the reticle "wires" (orthogonal lines in the rectangular case, arcs and radial spokes
        /// in the polar), axis tick marks, axis numbering (calibration), and the axes themselves.
        /// The rendering process comprises a total of eight stages, by Pass and Phase, in the
        /// following sequence:
        ///
        ///   Stage  Pass   Phase  What is rendered?
        ///   ----------------------------------------------------------------------------------
        ///     1    Lines    1    Horizontal dotted lines (Cartesian) or radial spokes (Polar).
        ///     2      "      2    Vertical dotted lines (Cartesian) or circular arcs (Polar).
        ///     3    Ticks    1    Ticks along the Y axis.
        ///     4      "      2    Ticks along the X axis.
        ///     5    Scale    1    Numbers on the Y axis.
        ///     6      "      2    Numbers on the X axis.
        ///     7    Axes     1    The X axis.
        ///     8      "      2    The Y axis.
        ///   ----------------------------------------------------------------------------------
        /// </summary>
        /// <param name="g">The GDI+ output Graphics object.</param>
        /// <param name="info">A struct of miscellaneous information about the Reticle.</param>
        public static void DrawReticle(this Graphics g, List<Label> labels, ReticleInfo info)
        {
            var vp = info.Viewport;
            float x1 = vp.Left, y1 = vp.Bottom, x2 = vp.Right, y2 = vp.Top;
            var log = Math.Log10(Math.Abs(x2 - x1));
            var order = Math.Floor(log);
            var scale = log - order;
            var power = Math.Pow(10, order - 1);
            using (Pen
                reticlePen = new Pen(info.ReticleColour, info.PenWidth) { DashStyle = DashStyle.Dot },
                axisPen = new Pen(info.AxisColour, info.PenWidth))
            using (var brush = new SolidBrush(info.AxisColour))
            using (var font = new Font("Segoe UI", 5 * info.PenWidth))
            using (var format = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Far })
            {
                double incmin = scale < 0.3 ? 2 : scale < 0.7 ? 5 : 10;
                var incmax = incmin;
                BumpDown(ref incmin);
                BumpDown(ref incmin);
                for (var pass = (ReticlePass)0; (int)pass <= 3; pass++)
                {
                    for (var phase = 1; phase <= 2; phase++)
                    {
                        double
                            maxAbsX = Math.Max(Math.Abs(x1), Math.Abs(x2)),
                            maxAbsY = Math.Max(Math.Abs(y1), Math.Abs(y2));
                        info.Vertical = phase == 2;
                        switch (pass)
                        {
                            case ReticlePass.Lines:
                            case ReticlePass.Ticks:
                            case ReticlePass.Scale:
                                float xa = x1, xb = x2;
                                var dy = incmin * power;
                                var ymax = maxAbsY;
                                if (info.Polar)
                                {
                                    ymax = Math.Sqrt(maxAbsX * maxAbsX + maxAbsY * maxAbsY);
                                    if (phase == 1 && pass == ReticlePass.Lines)
                                    {
                                        xa = (float)ymax;
                                        xb = (float)(incmax * power);
                                        dy = 5;
                                        ymax = 180 - dy;
                                    }
                                }
                                for (var y = 0.0; y <= ymax; y += dy)
                                {
                                    var pen = pass == ReticlePass.Ticks ? axisPen : reticlePen;
                                    g.DrawWire(labels, pen, font, brush, format, info, pass, xa, xb, (float)y);
                                    if (y != 0.0)
                                        g.DrawWire(labels, pen, font, brush, format, info, pass, xa, xb, -(float)y);
                                }
                                break;
                            case ReticlePass.Axes:
                                g.DrawWire(labels, axisPen, font, brush, format, info, ReticlePass.Axes, x1, x2, 0);
                                break;
                        }
                        Swap(ref x1, ref y1);
                        Swap(ref x2, ref y2);
                    }
                    BumpUp(ref incmin);
                }
            }
        }

        private enum ReticlePass { Lines, Ticks, Scale, Axes }

        private static void BumpDown(ref double value) => value = value == 5 ? 2 : value / 2;
        private static void BumpUp(ref double value) => value = value == 2 ? 5 : value * 2;

        private static void DrawWire(this Graphics g, List<Label> labels, Pen pen, Font font, Brush brush, StringFormat format,
            ReticleInfo info, ReticlePass pass, float x1, float x2, float y)
        {
            var vp = info.Viewport;
            var clip =
                !info.Vertical && (y < vp.Top || y > vp.Bottom) ||
                info.Vertical && (y < vp.Left || y > vp.Right);
            if (pass == ReticlePass.Ticks)
            {
                var tickSize = font.Size / 2;
                x1 = info.TickPositive ? tickSize : 0;
                x2 = info.TickNegative ? -tickSize : 0;
            }
            float x = 0, y1 = y, y2 = y, z = y;
            if (info.Vertical)
            {
                Swap(ref x, ref y);
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
            }
            switch (pass)
            {
                case ReticlePass.Axes when info.Axis:
                case ReticlePass.Lines when info.Wires && !info.Polar:
                case ReticlePass.Ticks when info.Ticks:
                    if (!clip)
                        g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case ReticlePass.Lines when info.Hwires && info.Polar:
                    if (y >= 0)
                        g.DrawWireSpoke(labels, pen, info, r1: x1, r2: x2, degrees: y);
                    break;
                case ReticlePass.Lines when info.Vwires && info.Polar:
                    if (x1 >= 0)
                        g.DrawWireArc(labels, pen, info, r: x1);
                    break;
                case ReticlePass.Scale when info.Calibration:
                    DrawWireCalibration(labels, pen.Width, x, y, label: z.ToString());
                    break;
            }
        }

        /// <summary>
        /// Draw a circular arc (or possibly a full or clipped circle) as part of a polar reticle.
        /// 
        /// If the circle's centre lies outside the viewport, we can draw an arc instead of a full circle.
        /// This is worth doing because GDI+ is slow at "drawing" clipped circles.
        ///
        /// Partition the coordinate space into nine regions: three in the X direction (left, centre, right),
        /// times three in the Y direction (top, middle, bottom). The inversion of the Y axis is necessitated
        /// by the use throughout of conventional mathematical (rather than computer graphic) axes. So yes,
        /// the "bottom" region is at the top, etc.
        ///
        /// left centre right               If the circle's centre is inside the viewport, corresponding to
        ///                                 the central region numbered 4 here, we must draw the full circle.
        ///   6  |  7  |  8    bottom       If it's outside, the shadow of the viewport as seen from the
        /// -----+-----+-----               centre of the circle is bounded by two of the viewport corners
        ///   3  |  4  |  5    middle       (possibly adjacent, possibly opposite). Which two corners? That
        /// -----+-----+-----               depends on the region, as detailed by the array of point pairs
        ///   0  |  1  |  2    top          "PointF[,] corners" in the code.
        ///
        /// Once we have those two corners, we have the starting and finishing angles of our arc: these are
        /// just the arctangents of the corners' coordinates.
        /// </summary>
        /// <param name="g">The GDI+ output Graphics object.</param>
        /// <param name="pen">The pen used to draw the reticle.</param>
        /// <param name="info">A struct containing miscellaneous information about the reticle being drawn.</param>
        /// <param name="r">The radius of the arc.</param>
        private static void DrawWireArc(this Graphics g, List<Label>labels, Pen pen, ReticleInfo info, float r)
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

        private static void DrawWireCalibration(List<Label> labels, float penWidth, float x, float y, string label) =>
            labels.Add(new Label(label, x - penWidth, y - penWidth, false));

        private static void DrawWireSpoke(this Graphics g, List<Label> labels, Pen pen,
            ReticleInfo info, float r1, float r2, float degrees)
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
                labels.Add(new Label(label, p.X, p.Y, true, d));
            }
        }

        private static void Swap(ref float x, ref float y) { var t = x; x = y; y = t; }
    }
}
