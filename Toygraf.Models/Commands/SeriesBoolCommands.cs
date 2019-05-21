namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesBoolCommand : SeriesIntCommand
    {
        protected SeriesBoolCommand(int index, bool value, Func<Series, bool> get, Action<Series, bool> set) :
            base(index, value ? 1 : 0, s => get(s) ? 1 : 0, (s, b) => set(s, b != 0)) { }

        public override string ToString() =>
            $"f{Index} {Detail} = {Value != 0}";
    }

    public class SeriesVisibleCommand : SeriesBoolCommand
    {
        public SeriesVisibleCommand(int index, bool value) :
            base(index, value,
                s => s.Visible,
                (s, b) => s.Visible = b) { }

        public override string Action => "show/hide function";

        protected override string Detail => "visible";
    }
}
