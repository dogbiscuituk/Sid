namespace ToyGraf.Models.Commands
{
    using System;

    public abstract class SeriesIntCommand : SeriesPropertyCommand
    {
        protected SeriesIntCommand(int index, int value, Func<Series, int> get, Action<Series, int> set) :
            base(index)
        {
            Value = value;
            Get = get;
            Set = set;
        }

        protected new int Value { get => (int)base.Value; set => base.Value = value; }
        protected Func<Series, int> Get;
        protected Action<Series, int> Set;

        protected override void Run(Graph graph)
        {
            var n = Get(graph.Series[Index]);
            Set(graph.Series[Index], Value);
            Value = n;
        }
    }

    public class SeriesFillTransparencyPercentCommand : SeriesIntCommand
    {
        public SeriesFillTransparencyPercentCommand(int index, int value) :
            base(index, value,
                s => s.FillTransparencyPercent,
                (s, n) => s.FillTransparencyPercent = n) { }

        protected override string Detail => "fill transparency (%)";
    }

    public class SeriesStepCountCommand : SeriesIntCommand
    {
        public SeriesStepCountCommand(int index, int value) :
            base(index, value,
                s => s.StepCount,
                (s, n) => s.StepCount = n) { }

        protected override string Detail => "#steps";
    }
}
