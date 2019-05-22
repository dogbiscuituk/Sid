namespace ToyGraf.Models.Commands
{
    public class GraphSeriesCommand : SeriesCommand
    {
        public GraphSeriesCommand(int index) : base(index) { }

        public Series Series;

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

        protected void DeleteSeries(Graph graph)
        {
            Series = graph.Series[Index];
            graph.Series.RemoveAt(Index);
            Series.InvalidatePoints();
        }

        protected override string Detail => Series == null ? $"f{Index}" : $"f{Index} = {Series.Formula}";
    }

    public class GraphInsertSeriesCommand : GraphSeriesCommand
    {
        public GraphInsertSeriesCommand(int index) : base(index) { }

        public override string UndoAction => "function deletion";
        public override string RedoAction => "function addition";

        public override GraphCommand Invert() => new GraphDeleteSeriesCommand(Index) { Series = Series };
        public override void Undo(Graph graph) => DeleteSeries(graph);
        public override void Redo(Graph graph) => InsertSeries(graph);
        public override string ToString() => $"Add function {Detail}";
    }

    public class GraphDeleteSeriesCommand : GraphSeriesCommand
    {
        public GraphDeleteSeriesCommand(int index) : base(index) { }

        public override string UndoAction => "function addition";
        public override string RedoAction => "function deletion";

        public override GraphCommand Invert() => new GraphInsertSeriesCommand(Index) { Series = Series };
        public override void Undo(Graph graph) => InsertSeries(graph);
        public override void Redo(Graph graph) => DeleteSeries(graph);
        public override string ToString() => $"Remove function {Detail}";
    }
}
