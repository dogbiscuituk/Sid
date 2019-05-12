namespace ToyGraf.Models
{
    using System;
    using System.ComponentModel;

    [Flags]
    public enum PlotType
    {
        [Description("Cartesian")]
        Cartesian,
        [Description("Polar")]
        Polar
    }
}
