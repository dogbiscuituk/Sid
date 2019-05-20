namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesEnumCommand : SeriesIntCommand
    {
        protected SeriesEnumCommand(int index, Func<Series, object> get, Action<Series, object> set) :
            base(index, s => (int)get(s), (s, e) => set(s, e))
        { }
    }
}
