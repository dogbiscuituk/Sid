namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesIntCommand : SeriesCommand
    {
        protected SeriesIntCommand(int index, int value, Func<Series, int> get, Action<Series, int> set) :
            base(index)
        {
            Value = value;
            Get = get;
            Set = set;
        }

        protected int Value;
        protected Func<Series, int> Get;
        protected Action<Series, int> Set;

        protected override void Do(Graph graph)
        {
            var n = Get(graph.Series[Index]);
            Set(graph.Series[Index], Value);
            Value = n;
        }

        public override string ToString() => $"f{Index}(x,t) {Detail}";
    }

    public class SeriesFillTransparencyPercentCommand : SeriesIntCommand
    {
        public SeriesFillTransparencyPercentCommand(int index, int value) :
            base(index, value,
                s => s.FillTransparencyPercent,
                (s, n) => s.FillTransparencyPercent = n) { }

        protected override string Detail => $"fill transparency = {Value}%";
    }

    public class SeriesStepCountCommand : SeriesIntCommand
    {
        public SeriesStepCountCommand(int index, int value) :
            base(index, value,
                s => s.StepCount,
                (s, n) => s.StepCount = n) { }

        protected override string Detail => $"#steps = {Value}";
    }
}
