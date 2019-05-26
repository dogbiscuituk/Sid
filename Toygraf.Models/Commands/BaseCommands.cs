namespace ToyGraf.Models.Commands
{
    public abstract class GraphCommand
    {
        /// <summary>
        /// Invoke the Run method of the command, then immediately invert
        /// the command in readiness for its transfer between the Undo and
        /// Redo stacks. Since most commands are their own inverses (they
        /// just tell the Graph "Swap your property value with the one I'm
        /// carrying", the Invert() method is almost always empty. See the
        /// GraphInsertSeriesCommand and GraphDeleteSeriesCommand classes
        /// for a notable exception to this rule.
        /// </summary>
        /// <param name="graph"></param>
        public void Do(Graph graph) { Run(graph); Invert(); }

        public virtual string Action => $"{Detail} change";
        public virtual string UndoAction => Action;
        public virtual string RedoAction => Action;
        public override string ToString() => $"{Target} {Detail} = {Value}";

        protected abstract string Detail { get; }
        protected virtual void Invert() { }
        protected abstract void Run(Graph graph);
        protected virtual string Target { get => "Graph"; }
        protected virtual object Value { get; set; }
    }

    public abstract class GraphPropertyCommand : GraphCommand { }

    public abstract class SeriesCommand : GraphCommand
    {
        public SeriesCommand(int index) : base() { Index = index; }

        public int Index { get; protected set; }
    }

    public abstract class SeriesPropertyCommand : SeriesCommand
    {
        public SeriesPropertyCommand(int index) : base(index) { }

        protected override string Target => $"f{Index}";
    }
}
