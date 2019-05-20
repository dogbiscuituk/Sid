namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesIntCommand : SeriesCommand
    {
        protected SeriesIntCommand(int index, Func<Series, int> get, Action<Series, int> set) :
            base(index)
        {
            Get = get;
            Set = set;
        }

        protected int Value;
        protected Func<Series, int> Get;
        protected Action<Series, int> Set;

        protected override void Do(Graph graph)
        {
            var value = Get(graph.Series[Index]);
            Set(graph.Series[Index], value);
            Value = value;
        }
    }

    public class SeriesFillTransparencyPercentCommand : SeriesIntCommand
    {
        public SeriesFillTransparencyPercentCommand(int index) :
            base(index,
                s => s.FillTransparencyPercent,
                (s, n) => s.FillTransparencyPercent = n)
        { }
    }

    public class SeriesStepCountCommand : SeriesIntCommand
    {
        public SeriesStepCountCommand(int index) :
            base(index,
                s => s.StepCount,
                (s, n) => s.StepCount = n)
        { }
    }
}
