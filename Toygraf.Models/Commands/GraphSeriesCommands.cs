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
    public class GraphSeriesCommand : SeriesCommand<Series>, IGraphSeriesCommand
    {
        public GraphSeriesCommand(int index, bool add) : base(index) { Add = add; }

        public bool Add { get; set; }

        public override string UndoAction => GetAction(true);
        public override string RedoAction => GetAction(false);
        public override string ToString() => $"{(Add ? "Add" : "Remove")} function f{Index} = {Detail}";
        protected override string Detail => $"{base.Value?.Formula}";
        protected override void Invert() { Add = !Add; }
        
        protected override bool Run(Graph graph)
        {
            if (Add)
            {
                if (base.Value == null)
                    base.Value = graph.NewSeries();
                if (Index >= 0 && Index < graph.Series.Count)
                    graph.InsertSeries(Index, base.Value);
                else if (Index == graph.Series.Count)
                    graph.AddSeries(base.Value);
            }
            else
            {
                base.Value = graph.Series[Index];
                graph.RemoveSeries(Index);
                base.Value.InvalidatePaths();
            }
            return true;
        }

        private string GetAction(bool undo) => $"function {(Add ^ undo ? "addition" : "removal")}";
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
