using System;

namespace Sid.Models
{
    [Flags]
    public enum Elements
    {
        Paper = 0x0001,
        Xaxis = 0x0002,
        Yaxis = 0x0004,
        Xcalibration = 0x0008,
        Ycalibration = 0x0010,
        Xticks = 0x0020,
        Yticks = 0x0040,
        HorizontalGridLines = 0x0080,
        VerticalGridLines = 0x0100,
        Axes = Xaxis | Yaxis,
        Calibration = Xcalibration | Ycalibration,
        Ticks = Xticks | Yticks,
        GridLines = HorizontalGridLines | VerticalGridLines,
        All = Paper | Axes | Calibration | Ticks | GridLines
    }
}
