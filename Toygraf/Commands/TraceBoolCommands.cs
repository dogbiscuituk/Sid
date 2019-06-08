namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
        private class TraceVisibleCommand : TracePropertyCommand<bool>
        {
            public TraceVisibleCommand(int index, bool value) :
                base(index, value,
                    s => s.Visible,
                    (s, v) => s.Visible = v)
            { }

            protected override string Action => "show/hide function";
            protected override string Detail => "visible";
        }
    }
}