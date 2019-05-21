namespace ToyGraf.Models.Commands
{
    using System;
    using System.Drawing;
    using ToyGraf.Expressions;

    public class SeriesColourCommand : SeriesIntCommand
    {
        protected SeriesColourCommand(int index, Color colour, Func<Series, Color> get, Action<Series, Color> set) :
            base(index, colour.ToArgb(), c => get(c).ToArgb(), (g, c) => set(g, Color.FromArgb(c))) { }

        public override string ToString() =>
            $"f{Index} {Detail} colour = {Color.FromArgb(Value).GetName()}";
    }

    public class SeriesFillColourCommand : SeriesColourCommand
    {
        public SeriesFillColourCommand(int index, Color colour) :
            base(index, colour, s => s.FillColour, (s, c) => s.FillColour = c) { }

        public override string Action => "fill colour change";
        protected override string Detail => "fill";
    }

    public class SeriesLimitColourCommand : SeriesColourCommand
    {
        public SeriesLimitColourCommand(int index, Color colour) :
            base(index, colour, s => s.LimitColour, (s, c) => s.LimitColour = c) { }

        public override string Action => "limit colour change";
        protected override string Detail => "limit";
    }

    public class SeriesPenColourCommand : SeriesColourCommand
    {
        public SeriesPenColourCommand(int index, Color colour) :
            base(index, colour, s => s.PenColour, (s, c) => s.PenColour = c) { }

        public override string Action => "pen colour change";
        protected override string Detail => "pen";
    }
}
