namespace ToyGraf.Models.Structs
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Structure representing a particular view on a Graph. This class replaces an earlier property
    /// named "Limits", which being a RectF, imposed a definite aspect ratio on the viewport (and so
    /// led to the ill-advised introduction of the "Anisotropic" plot type). "Viewport" replaces the
    /// four fixed values in "Limits" with three: the viewport Centre (X, Y) and its Width. Vertical
    /// measurements use the "ratio", provided separately, of the rendering canvas height to width.
    /// </summary>
    public struct Viewport
    {
        public Viewport(PointF centre, float width, float ratio = 1)
        {
            Centre = centre;
            Width = width;
            _ratio = ratio;
        }

        public Viewport(PointF centre, float width, Size ratio)
            : this(centre, width, (float)ratio.Height / ratio.Width) { }

        public PointF Centre;
        public float Width;
        public float Height => Width * _ratio;
        public float Left => Centre.X - Width / 2;
        public float Top => Centre.Y - Height / 2;
        public float Right => Centre.X + Width / 2;
        public float Bottom => Centre.Y + Height / 2;
        public RectangleF Limits => new RectangleF(Left, Top, Width, Height);

        public PointF BottomLeft { get => new PointF(Left, Bottom); }
        public PointF BottomRight { get => new PointF(Right, Bottom); }
        public PointF TopLeft { get => new PointF(Left, Top); }
        public PointF TopRight { get => new PointF(Right, Top); }

        private float _ratio;

        public void SetRatio(float ratio) { _ratio = ratio; }
        public void SetRatio(Size size) { _ratio = (float)size.Height / size.Width; }

        public PointF[] GetIntercepts(float radians, float margin)
        {
            var r = Limits;
            r.Inflate(-margin, -margin);
            float m = (float)Math.Tan(radians);
            var result = new List<PointF>()
            {
                new PointF(r.Left, r.Left * m),
                new PointF(r.Top / m, r.Top),
                new PointF(r.Right, r.Right * m),
                new PointF(r.Bottom / m, r.Bottom)
            };
            margin /= 1000;
            r.Inflate(margin, margin);
            result.RemoveAll(p => !r.Contains(p));
            return result.ToArray();
        }

        public static bool operator ==(Viewport u, Viewport v) =>
            u.Centre == v.Centre && u.Width == v.Width && u._ratio == v._ratio;

        public static bool operator !=(Viewport u, Viewport v) => !(u == v);

        public override bool Equals(object obj) => obj is Viewport v && this == v;

        public override int GetHashCode() =>
            Centre.GetHashCode() ^ Width.GetHashCode() ^ _ratio.GetHashCode();
    }
}
