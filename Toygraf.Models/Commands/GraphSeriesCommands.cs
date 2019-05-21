namespace ToyGraf.Models.Commands
{
    public class GraphSeriesCommand : SeriesCommand
    {
        public GraphSeriesCommand(int index) : base(index) { }

        protected Series Series;

        protected void InsertSeries(Graph graph)
        {
            if (Series == null)
                Series = new Series(graph);
            if (Index >= 0 && Index < graph.Series.Count)
                graph.Series.Insert(Index, Series);
            else if (Index == graph.Series.Count)
                graph.Series.Add(Series);
            Series = null;
        }

        protected void RemoveSeries(Graph graph)
        {
            Series = graph.Series[Index];
            graph.Series.RemoveAt(Index);
            Series.InvalidatePoints();
        }

        public override string ToString() => Series == null
            ? $"f{Index}(x,t)"
            : $"f{Index}(x,t) = {Series.Formula}";
    }

    public class GraphInsertSeriesCommand : GraphSeriesCommand
    {
        public GraphInsertSeriesCommand(int index) : base(index) { }

        public override string Action => "function add";

        public override void Redo(Graph graph) => InsertSeries(graph);
        public override void Undo(Graph graph) => RemoveSeries(graph);
    }

    public class GraphRemoveSeriesCommand : GraphSeriesCommand
    {
        public GraphRemoveSeriesCommand(int index) : base(index) { }

        public override string Action => "function delete";

        public override void Redo(Graph graph) => RemoveSeries(graph);
        public override void Undo(Graph graph) => InsertSeries(graph);
    }
}
