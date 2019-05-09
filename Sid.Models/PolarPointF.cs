namespace ToyGraf.Models
{
    using System;
    using System.Drawing;

    public struct PolarPointF
    {
        public PolarPointF(float r, float θ) { R = r; ϴ = θ; }

        public PolarPointF(PointF p) : this(
            (float)Math.Sqrt(p.X * p.X + p.Y * p.Y),
            (float)Math.Atan2(p.Y, p.X))
        { }

        public float R { get; set; }
        public float ϴ { get; set; }

        public static readonly PolarPointF Empty;

        public PointF ToPointF() => new PointF(
            (float)(R * Math.Cos(ϴ)),
            (float)(R * Math.Sin(ϴ)));

        public override string ToString() => $"{{R={R}, ϴ={ϴ}}}";
    }
}
