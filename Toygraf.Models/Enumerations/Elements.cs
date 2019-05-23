namespace ToyGraf.Models.Enumerations
{
    using System;
    using System.ComponentModel;

    [Flags]
    public enum Elements
    {
        [Description("None")]
        None = 0x0000,
        [Description("X-axis")]
        Xaxis = 0x0001,
        [Description("Y-axis")]
        Yaxis = 0x0002,
        [Description("Both axes")]
        Axes = Xaxis | Yaxis,
        [Description("Horizontal/radial reticule")]
        HorizontalWires = 0x0004,
        [Description("Vertical/circular reticule")]
        VerticalWires = 0x0008,
        [Description("Full reticule")]
        Wires = HorizontalWires | VerticalWires,
        [Description("X ticks")]
        Xticks = 0x0010,
        [Description("Y ticks")]
        Yticks = 0x0020,
        [Description("X & Y ticks")]
        Ticks = Xticks | Yticks,
        [Description("X calibration")]
        Xcalibration = 0x0040,
        [Description("Y calibration")]
        Ycalibration = 0x0080,
        [Description("Full calibration")]
        Calibration = Xcalibration | Ycalibration,
        [Description("All elements")]
        All = Axes | Wires | Ticks | Calibration
    }
}
