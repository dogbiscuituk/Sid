namespace ToyGraf.Models
{
    using System.Drawing;

    public class Label
    {
        public Label(string text, float x, float y, bool polar = false, float degrees = 0)
        {
            Text = text;
            Location = new PointF(x, y);
            Polar = polar;
            Degrees = degrees;
        }

        public void Draw(Graphics g, Brush brush, Font font, StringFormat format)
        {
            format.Alignment = Polar ? StringAlignment.Center : StringAlignment.Far;
            format.LineAlignment = Polar ? StringAlignment.Far : StringAlignment.Near;
            g.TranslateTransform(Location.X, Location.Y);
            if (Polar) g.RotateTransform(Degrees);
            // The temporary inversion of the Y axis is necessitated by the use
            // of conventional mathematical (rather than computer graphic) axes.
            g.ScaleTransform(1, -1);
            g.DrawString(Text, font, brush, 0, 0, format);
            g.ScaleTransform(1, -1);
            if (Polar) g.RotateTransform(-Degrees);
            g.TranslateTransform(-Location.X, -Location.Y);
        }

        public PointF Location { get; set; }
        public bool Polar { get; set; }
        public float Degrees { get; set; }
        public string Text { get; set; }
    }
}
