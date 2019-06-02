namespace ToyGraf.Commands
{
    partial class CommandProcessor
    {
        private class SeriesFillTransparencyPercentCommand : SeriesPropertyCommand<int>
        {
            public SeriesFillTransparencyPercentCommand(int index, int value) :
                base(index, value,
                    s => s.FillTransparencyPercent,
                    (s, v) => s.FillTransparencyPercent = v)
            { }

            protected override string Detail => "fill transparency (%)";
        }

        private class SeriesStepCountCommand : SeriesPropertyCommand<int>
        {
            public SeriesStepCountCommand(int index, int value) :
                base(index, value,
                    s => s.StepCount,
                    (s, v) => s.StepCount = v)
            { }

            protected override string Detail => "#steps";
        }
    }
}