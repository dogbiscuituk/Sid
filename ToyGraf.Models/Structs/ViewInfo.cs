namespace ToyGraf.Models.Structs
{
    using System.Drawing;

    public struct ViewInfo
    {
        public ViewInfo(float centreX, float centreY, float width)
            : this(new PointF(centreX, centreY), width) { }

        public ViewInfo(PointF centre, float width)
        {
            Centre = centre;
            Width = width;
        }

        public PointF Centre;
        public float Width;

        public float X => Centre.X;
        public float Y => Centre.Y;

        public override string ToString() => $"({X}, {Y}, {Width})";

        public static bool operator ==(ViewInfo u, ViewInfo v) =>
            u.Centre == v.Centre && u.Width == v.Width;

        public static bool operator !=(ViewInfo u, ViewInfo v) => !(u == v);

        public override bool Equals(object obj) => obj is ViewInfo v && this == v;

        public override int GetHashCode() =>
            Centre.GetHashCode() ^ Width.GetHashCode();
    }
}
