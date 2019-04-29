using System;

namespace Sid.Models
{
    [Flags]
    public enum Elements
    {
        Paper = 0x01,
        AxisX = 0x02,
        AxisY = 0x04,
        NumberingX = 0x08,
        NumberingY = 0x10,
        HorizontalGridLines = 0x20,
        VerticalGridLines = 0x40,
        Axes = AxisX | AxisY,
        Numbering = NumberingX | NumberingY,
        GridLines = HorizontalGridLines | VerticalGridLines,
        All = Paper | Axes | Numbering | GridLines
    }
}
