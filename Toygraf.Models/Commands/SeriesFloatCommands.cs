namespace ToyGraf.Models.Commands
{
    using System;

    public abstract class SeriesFloatCommand : SeriesPropertyCommand
    {
        protected SeriesFloatCommand(int index, float value, Func<Series, float> get, Action<Series, float> set) :
            base(index)
        {
            Value = value;
            Get = get;
            Set = set;
        }

        protected new float Value { get => (float)base.Value; set => base.Value = value; }
        protected Func<Series, float> Get;
        protected Action<Series, float> Set;

        protected override void Run(Graph graph)
        {
            var f = Get(graph.Series[Index]);
            Set(graph.Series[Index], Value);
            Value = f;
        }
    }

    public class SeriesPenWidthCommand : SeriesFloatCommand
    {
        public SeriesPenWidthCommand(int index, float value) :
            base(index, value,
                s => s.PenWidth,
                (s, f) => s.PenWidth = f) { }

        protected override string Detail => $"pen size";
    }
}
