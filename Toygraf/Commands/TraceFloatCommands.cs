namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
        private class TracePenWidthCommand : TracePropertyCommand<float>
        {
            public TracePenWidthCommand(int index, float value) :
                base(index, value,
                    s => s.PenWidth,
                    (s, v) => s.PenWidth = v)
            { }

            protected override string Detail => $"pen size";
        }
    }
}