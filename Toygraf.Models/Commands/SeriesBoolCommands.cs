namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesBoolCommand : SeriesIntCommand
    {
        protected SeriesBoolCommand(int index, Func<Series, bool> get, Action<Series, bool> set) :
            base(index, s => get(s) ? 1 : 0, (s, b) => set(s, b != 0))
        { }
    }

    public class SeriesVisibleCommand : SeriesBoolCommand
    {
        public SeriesVisibleCommand(int index) :
            base(index,
                s => s.Visible,
                (s, b) => s.Visible = b)
        { }
    }
}
