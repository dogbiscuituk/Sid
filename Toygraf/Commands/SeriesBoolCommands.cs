namespace ToyGraf.Commands
{
    partial class CommandProcessor
    {
        private class SeriesVisibleCommand : SeriesPropertyCommand<bool>
        {
            public SeriesVisibleCommand(int index, bool value) :
                base(index, value,
                    s => s.Visible,
                    (s, v) => s.Visible = v)
            { }

            protected override string Action => "show/hide function";
            protected override string Detail => "visible";
        }
    }
}