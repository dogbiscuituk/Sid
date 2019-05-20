namespace ToyGraf.Models.Enumerations
{
    using System.ComponentModel;

    public enum Interpolation
    {
        [Description("Linear")]
        Linear,
        [Description("Cardinal Spline")]
        CardinalSpline
    }
}
