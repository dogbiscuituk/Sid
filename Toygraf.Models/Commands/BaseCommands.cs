namespace ToyGraf.Models.Commands
{
    public class GraphCommand
    {
        public virtual void Redo(Graph graph) => Do(graph);
        public virtual void Undo(Graph graph) => Do(graph);

        protected virtual void Do(Graph graph) { }
    }

    public class SeriesCommand : GraphCommand
    {
        public SeriesCommand(int index)
            : base()
        {
            Index = index;
        }

        protected int Index;
    }
}
