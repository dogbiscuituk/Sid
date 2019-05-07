namespace Sid.Models
{
    using System.ComponentModel;

    public enum PlotType
    {
        [Description("Cartesian - isotropic")]
        Cartesian,
        [Description("Polar - isotropic")]
        Polar,
        [Description("Anisotropic")]
        Anisotropic
    }
}
