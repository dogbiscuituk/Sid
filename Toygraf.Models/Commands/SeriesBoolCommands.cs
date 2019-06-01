namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesVisibleCommand : SeriesPropertyCommand<bool>
    {
        public SeriesVisibleCommand(int index, bool value) :
            base(index, value,
                s => s.Visible,
                (s, v) => s.Visible = v) { }

        public override string Action => "show/hide function";
        protected override string Detail => "visible";
    }
}
