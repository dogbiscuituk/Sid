namespace ToyGraf.Models.Commands
{
    public class GraphCommand
    {
        public virtual string Action => "property change";

        public virtual void Redo(Graph graph) => Do(graph);
        public virtual void Undo(Graph graph) => Do(graph);

        protected object Value { get; set; }

        protected virtual void Do(Graph graph) { }
        protected virtual string Detail { get; }
    }

    public class SeriesCommand : GraphCommand
    {
        public SeriesCommand(int index)
            : base()
        {
            Index = index;
        }

        public int Index { get; protected set; }
    }
}
