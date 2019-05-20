namespace ToyGraf.Models.Enumerations
{
    using System;

    [Flags]
    public enum Elements
    {
        Xaxis = 0x0001,
        Yaxis = 0x0002,
        Axes = Xaxis | Yaxis,
        HorizontalWires = 0x0004,
        VerticalWires = 0x0008,
        Wires = HorizontalWires | VerticalWires,
        Xticks = 0x0010,
        Yticks = 0x0020,
        Ticks = Xticks | Yticks,
        Xcalibration = 0x0040,
        Ycalibration = 0x0080,
        Calibration = Xcalibration | Ycalibration,
        All = Axes | Wires | Ticks | Calibration
    }
}
