namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
        private class TraceFillTransparencyPercentCommand : TracePropertyCommand<int>
        {
            public TraceFillTransparencyPercentCommand(int index, int value) :
                base(index, value,
                    s => s.FillTransparencyPercent,
                    (s, v) => s.FillTransparencyPercent = v)
            { }

            protected override string Detail => "fill transparency (%)";
        }

        private class TraceStepCountCommand : TracePropertyCommand<int>
        {
            public TraceStepCountCommand(int index, int value) :
                base(index, value,
                    s => s.StepCount,
                    (s, v) => s.StepCount = v)
            { }

            protected override string Detail => "#steps";
        }
    }
}