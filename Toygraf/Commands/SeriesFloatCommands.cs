namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
        private class SeriesPenWidthCommand : SeriesPropertyCommand<float>
        {
            public SeriesPenWidthCommand(int index, float value) :
                base(index, value,
                    s => s.PenWidth,
                    (s, v) => s.PenWidth = v)
            { }

            protected override string Detail => $"pen size";
        }
    }
}