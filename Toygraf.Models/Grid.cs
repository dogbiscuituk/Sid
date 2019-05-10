namespace ToyGraf.Models
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class Grid
    {
        public static void Draw(Graphics g, GridInfo info)
        {
            var limits = info.Viewport;
            float x1 = limits.Left, y1 = limits.Bottom, x2 = limits.Right, y2 = limits.Top;
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
                        DrawWire(g, info, axisPen, font, brush, x1, x2, 0, format, GridPass.Axes);
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
                                DrawWire(g, info, pen, font, brush, x0, x2, (float)y, format, pass);
                                if (y != 0.0)
                                    DrawWire(g, info, pen, font, brush, x0, x2, -(float)y, format, pass);
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

        private static void DrawWire(Graphics g, GridInfo info, Pen pen, Font font, Brush brush,
            float x1, float x2, float y, StringFormat format, GridPass pass)
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
                    var rad = y * Math.PI / 180;
                    double sin = Math.Sin(rad), cos = Math.Cos(rad);
                    float x0 = (float)(x1 * cos), y0 = (float)(x1 * sin);
                    g.DrawLine(pen, -x0, -y0, +x0, +y0);
                    break;
                case GridPass.Wires when info.Vwires && info.Polar:
                    g.DrawEllipse(pen, -x1, -x1, 2 * x1, 2 * x1);
                    break;
                case GridPass.Calibration when info.Calibration:
                    g.ScaleTransform(1, -1);
                    g.DrawString(z.ToString(), font, brush, x - pen.Width, y + pen.Width, format);
                    g.ScaleTransform(1, -1);
                    break;
                case GridPass.Ticks:
                    if (info.Ticks)
                        g.DrawLine(pen, x1, y1, x2, y2);
                    break;
            }
        }

        private static void Swap(ref float x, ref float y) { var t = x; x = y; y = t; }
    }
}
