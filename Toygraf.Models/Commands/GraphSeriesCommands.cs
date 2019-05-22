namespace ToyGraf.Models.Commands
{
    /// <summary>
    /// Common ancestor for GraphInsertSeriesCommand and GraphDeleteSeriesCommand.
    /// These two descendant classes differ only in the value of a private bool flag,
    /// "Add", which controls their appearance and behaviour. Whereas most commands
    /// are their own inverses, since they just tell the Graph "Swap your property
    /// value with the one I'm carrying", the Invert() method is usually empty. In
    /// the case of these two commands, toggling the Add flag converts one into the
    /// other, prior to the CommandProcessor transfering them between the Undo and
    /// Redo stacks.
    /// </summary>
    public class GraphSeriesCommand : SeriesCommand
    {
        public GraphSeriesCommand(int index, bool add) : base(index) { Add = add; }

        public override string UndoAction => GetAction(true);
        public override string RedoAction => GetAction(false);
        protected override string Detail => Series == null ? string.Empty : $" = {Series.Formula}";
        private Series Series;
        private bool Add;

        protected override void Invert() { Add = !Add; }
        
        public override string ToString()
        {
            var verb = Add ? "Add" : "Remove";
            return $"{verb} function f{Index}{Detail}";
        }

        protected override void Run(Graph graph)
        {
            if (Add)
            {
                if (Series == null)
                    Series = new Series(graph);
                if (Index >= 0 && Index < graph.Series.Count)
                    graph.Series.Insert(Index, Series);
                else if (Index == graph.Series.Count)
                    graph.Series.Add(Series);
                Series = null;
            }
            else
            {
                Series = graph.Series[Index];
                graph.Series.RemoveAt(Index);
                Series.InvalidatePoints();
            }
        }

        private string GetAction(bool undo) => Add ^ undo ? "function addition" : "function removal";
    }

    public class GraphInsertSeriesCommand : GraphSeriesCommand
    {
        public GraphInsertSeriesCommand(int index) : base(index, true) { }
    }

    public class GraphDeleteSeriesCommand : GraphSeriesCommand
    {
        public GraphDeleteSeriesCommand(int index) : base(index, false) { }
    }
}
