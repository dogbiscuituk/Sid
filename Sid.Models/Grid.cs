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
            using (Pen gridPen = new Pen(info.GridColour, info.PenWidth) { DashStyle = DashStyle.Dot },
                axisPen = new Pen(info.AxisColour, info.PenWidth))
            using (var font = new Font("Arial", 5 * info.PenWidth))
            using (var format = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Far })
            {
                var brush = Brushes.DarkGray;
                for (var phase = (GridPhase)0; (int)phase < 4; phase++)
                {
                    info.Vertical = phase == GridPhase.VerticalLines || phase == GridPhase.Yaxis;
                    if (phase == GridPhase.HorizontalLines || phase == GridPhase.VerticalLines)
                    {
                        double increment = scale < 0.3 ? 2 : scale < 0.7 ? 5 : 10;
                        BumpDown(ref increment);
                        BumpDown(ref increment);
                        for (var pass = (GridPass)0; (int)pass < 3; pass++)
                        {
                            var dy = increment * Math.Pow(10, order - 1);
                            for (var y = 0.0; y <= Math.Max(Math.Abs(y1), Math.Abs(y2)); y += dy)
                            {
                                var pen = pass == GridPass.Ticks ? axisPen : gridPen;
                                DrawLine(g, info, pen, font, brush, x1, x2, (float)y, format, pass);
                                if (y != 0.0)
                                    DrawLine(g, info, pen, font, brush, x1, x2, -(float)y, format, pass);
                            }
                            BumpUp(ref increment);
                        }
                    }
                    else
                        DrawLine(g, info, axisPen, font, brush, x1, x2, 0, format, GridPass.Axes);
                    var t = x1; x1 = y1; y1 = t;
                    t = x2; x2 = y2; y2 = t;
                }
            }
        }

        private static void BumpDown(ref double value) => value = value == 5 ? 2 : value / 2;
        private static void BumpUp(ref double value) => value = value == 2 ? 5 : value * 2;

        private static void DrawLine(Graphics g, GridInfo info, Pen pen, Font font, Brush brush,
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
                var t = x; x = y; y = t;
                t = x1; x1 = y1; y1 = t;
                t = x2; x2 = y2; y2 = t;
                z = -z;
            }
            switch (pass)
            {
                case GridPass.Axes when info.Axis:
                case GridPass.Lines when info.Lines && !info.Polar:
                    g.DrawLine(pen, x1, y1, x2, y2);
                    break;
                case GridPass.Lines when info.Vlines && info.Polar:
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
    }
}
