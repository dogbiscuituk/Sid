namespace ToyGraf.Commands
{
    using System.Drawing;

    partial class GraphProxy
    {
        private class TraceFillColour1Command : TracePropertyCommand<Color>
        {
            public TraceFillColour1Command(int index, Color colour) :
                base(index, colour, s => s.FillColour1, (s, v) => s.FillColour1 = v)
            { }

            protected override string Detail => "fill";
        }

        private class TraceFillColour2Command : TracePropertyCommand<Color>
        {
            public TraceFillColour2Command(int index, Color colour) :
                base(index, colour, s => s.FillColour2, (s, v) => s.FillColour2 = v)
            { }

            protected override string Detail => "secondary fill";
        }

        private class TraceLimitColourCommand : TracePropertyCommand<Color>
        {
            public TraceLimitColourCommand(int index, Color colour) :
                base(index, colour, s => s.LimitColour, (s, v) => s.LimitColour = v)
            { }

            protected override string Detail => "limit";
        }

        private class TracePenColourCommand : TracePropertyCommand<Color>
        {
            public TracePenColourCommand(int index, Color colour) :
                base(index, colour, s => s.PenColour, (s, v) => s.PenColour = v)
            { }

            protected override string Detail => "pen";
        }
    }
}