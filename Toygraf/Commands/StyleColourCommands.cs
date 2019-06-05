namespace ToyGraf.Commands
{
    using System.Drawing;

    partial class GraphProxy
    {
        private class StyleFillColour1Command : StylePropertyCommand<Color>
        {
            public StyleFillColour1Command(int index, Color colour) :
                base(index, colour, s => s.FillColour1, (s, v) => s.FillColour1 = v)
            { }

            protected override string Detail => "fill";
        }

        private class StyleFillColour2Command : StylePropertyCommand<Color>
        {
            public StyleFillColour2Command(int index, Color colour) :
                base(index, colour, s => s.FillColour2, (s, v) => s.FillColour2 = v)
            { }

            protected override string Detail => "secondary fill";
        }

        private class StyleLimitColourCommand : StylePropertyCommand<Color>
        {
            public StyleLimitColourCommand(int index, Color colour) :
                base(index, colour, s => s.LimitColour, (s, v) => s.LimitColour = v)
            { }

            protected override string Detail => "limit";
        }

        private class StylePenColourCommand : StylePropertyCommand<Color>
        {
            public StylePenColourCommand(int index, Color colour) :
                base(index, colour, s => s.PenColour, (s, v) => s.PenColour = v)
            { }

            protected override string Detail => "pen";
        }
    }
}