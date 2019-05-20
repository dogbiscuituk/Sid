namespace ToyGraf.Models.Commands
{
    using System;
    using System.Drawing;

    public class SeriesColourCommand : SeriesIntCommand
    {
        protected SeriesColourCommand(int index, Func<Series, Color> get, Action<Series, Color> set) :
            base(index, c => get(c).ToArgb(), (g, c) => set(g, Color.FromArgb(c)))
        { }
    }

    public class SeriesFillColourCommand : SeriesColourCommand
    {
        public SeriesFillColourCommand(int index) :
            base(index, s => s.FillColour, (s, c) => s.FillColour = c)
        { }
    }

    public class SeriesLimitColourCommand : SeriesColourCommand
    {
        public SeriesLimitColourCommand(int index) :
            base(index, s => s.LimitColour, (s, c) => s.LimitColour = c)
        { }
    }

    public class SeriesPenColourCommand : SeriesColourCommand
    {
        public SeriesPenColourCommand(int index)
            : base(index, s => s.PenColour, (s, c) => s.PenColour = c)
        { }
    }
}
