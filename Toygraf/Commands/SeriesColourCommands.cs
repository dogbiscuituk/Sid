namespace ToyGraf.Commands
{
    using System.Drawing;

    partial class GraphProxy
    {
        private class SeriesFillColour1Command : SeriesPropertyCommand<Color>
        {
            public SeriesFillColour1Command(int index, Color colour) :
                base(index, colour, s => s.FillColour1, (s, v) => s.FillColour1 = v)
            { }

            protected override string Detail => "fill";
        }

        private class SeriesFillColour2Command : SeriesPropertyCommand<Color>
        {
            public SeriesFillColour2Command(int index, Color colour) :
                base(index, colour, s => s.FillColour2, (s, v) => s.FillColour2 = v)
            { }

            protected override string Detail => "secondary fill";
        }

        private class SeriesLimitColourCommand : SeriesPropertyCommand<Color>
        {
            public SeriesLimitColourCommand(int index, Color colour) :
                base(index, colour, s => s.LimitColour, (s, v) => s.LimitColour = v)
            { }

            protected override string Detail => "limit";
        }

        private class SeriesPenColourCommand : SeriesPropertyCommand<Color>
        {
            public SeriesPenColourCommand(int index, Color colour) :
                base(index, colour, s => s.PenColour, (s, v) => s.PenColour = v)
            { }

            protected override string Detail => "pen";
        }
    }
}