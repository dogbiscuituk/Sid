namespace ToyGraf.Models.Commands
{
    public class GraphInsertSeriesCommand : SeriesCommand
    {
        public GraphInsertSeriesCommand(int index) :
            base(index)
        { }

        public override void Redo(Graph graph)
        {
            graph.Series.Insert(Index, new Series(graph));
        }

        public override void Undo(Graph graph)
        {
            graph.Series.RemoveAt(Index);
        }
    }

    public class GraphRemoveSeriesCommand : SeriesCommand
    {
        public GraphRemoveSeriesCommand(int index) :
            base(index)
        { }

        private Series Series;

        public override void Redo(Graph graph)
        {
            Series = graph.Series[Index];
            graph.Series.RemoveAt(Index);
            Series.InvalidatePoints();
        }

        public override void Undo(Graph graph)
        {
            graph.Series.Insert(Index, Series);
        }
    }
}
