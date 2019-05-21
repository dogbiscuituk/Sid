namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesEnumCommand : SeriesIntCommand
    {
        protected SeriesEnumCommand(int index, object value, Func<Series, object> get, Action<Series, object> set) :
            base(index, (int)value, s => (int)get(s), (s, e) => set(s, e)) { }

        public override string ToString() => $"f{Index} {Detail}";
    }
}
