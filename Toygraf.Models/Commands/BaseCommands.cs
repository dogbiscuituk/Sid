namespace ToyGraf.Models.Commands
{
    public class GraphCommand
    {
        public virtual string Action => "property change";
        public virtual string UndoAction => Action;
        public virtual string RedoAction => Action;

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

        protected virtual string Detail { get; }

        protected virtual void Invert() { }
        protected virtual void Run(Graph graph) { }
    }

    public class GraphPropertyCommand : GraphCommand
    {
        protected object Value { get; set; }
    }

    public class SeriesCommand : GraphCommand
    {
        public SeriesCommand(int index) : base() { Index = index; }

        public int Index { get; protected set; }
    }

    public class SeriesPropertyCommand : SeriesCommand
    {
        public SeriesPropertyCommand(int index) : base(index) { }

        protected object Value { get; set; }
    }
}
