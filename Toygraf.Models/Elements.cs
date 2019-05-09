using System;

namespace ToyGraf.Models
{
    [Flags]
    public enum Elements
    {
        Xaxis = 0x0001,
        Yaxis = 0x0002,
        Axes = Xaxis | Yaxis,
        HorizontalGridLines = 0x0004,
        VerticalGridLines = 0x0008,
        GridLines = HorizontalGridLines | VerticalGridLines,
        Xticks = 0x0010,
        Yticks = 0x0020,
        Ticks = Xticks | Yticks,
        Xcalibration = 0x0040,
        Ycalibration = 0x0080,
        Calibration = Xcalibration | Ycalibration,
        All = Axes | GridLines | Ticks | Calibration
    }
}
