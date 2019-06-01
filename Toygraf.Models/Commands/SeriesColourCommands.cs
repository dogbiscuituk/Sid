namespace ToyGraf.Models.Commands
{
    using System.Drawing;

    public class SeriesFillColour1Command : SeriesPropertyCommand<Color>
    {
        public SeriesFillColour1Command(int index, Color colour) :
            base(index, colour, s => s.FillColour1, (s, v) => s.FillColour1 = v) { }

        protected override string Detail => "fill";
    }

    public class SeriesFillColour2Command : SeriesPropertyCommand<Color>
    {
        public SeriesFillColour2Command(int index, Color colour) :
            base(index, colour, s => s.FillColour2, (s, v) => s.FillColour2 = v) { }

        protected override string Detail => "secondary fill";
    }

    public class SeriesLimitColourCommand : SeriesPropertyCommand<Color>
    {
        public SeriesLimitColourCommand(int index, Color colour) :
            base(index, colour, s => s.LimitColour, (s, v) => s.LimitColour = v) { }

        protected override string Detail => "limit";
    }

    public class SeriesPenColourCommand : SeriesPropertyCommand<Color>
    {
        public SeriesPenColourCommand(int index, Color colour) :
            base(index, colour, s => s.PenColour, (s, v) => s.PenColour = v) { }

        protected override string Detail => "pen";
    }
}
