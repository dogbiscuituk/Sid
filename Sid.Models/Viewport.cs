namespace Sid.Models
{
    using System.Drawing;

    /// <summary>
    /// Structure representing a particular view on a Graph. This class replaces an earlier property
    /// named "Limits", which being a RectF, imposed a definite aspect ratio on the viewport (and so
    /// led to the ill-advised introduction of the "Anisotropic" plot type). "Viewport" replaces the
    /// four fixed values in "Limits" with just three: the Centre of the viewport, and its Width. If
    /// independently scaled axes are required at a later date, such feature will be provided by new
    /// Scale Factor properties, applicable to both axes.
    /// </summary>
    public struct Viewport
    {
        public PointF Centre;
        public float Width;
    }
}
