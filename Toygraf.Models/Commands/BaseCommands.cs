using System;

namespace ToyGraf.Models.Commands
{
    public class GraphCommand
    {
        public virtual string Action => "property change";
        public virtual string UndoAction => Action;
        public virtual string RedoAction => Action;
        public virtual GraphCommand Invert() { return this; }
        public virtual void Undo(Graph graph) => Run(graph);
        public virtual void Redo(Graph graph) => Run(graph);

        protected object Value { get; set; }
        protected virtual void Run(Graph graph) { }
        protected virtual string Detail { get; }
    }

    public class SeriesCommand : GraphCommand
    {
        public SeriesCommand(int index) : base() { Index = index; }
        public int Index { get; protected set; }
    }
}
