namespace ToyGraf.Controllers
{
    using System.Collections.Generic;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;

    public class CommandController
    {
        public CommandController(AppController parent)
        {
            Parent = parent;
        }

        public void Execute(GraphCommand command)
        {
            Redo(command);
            RedoStack.Clear();
        }

        public void Redo()
        {
            Redo(RedoStack.Pop());
        }

        public void Undo()
        {
            Undo(UndoStack.Pop());
        }

        private AppController Parent;
        private Graph Graph => Parent.Graph;

        private readonly Stack<GraphCommand> RedoStack = new Stack<GraphCommand>();
        private readonly Stack<GraphCommand> UndoStack = new Stack<GraphCommand>();

        private void Redo(GraphCommand command)
        {
            command.Redo(Graph);
            UndoStack.Push(command);
        }

        private void Undo(GraphCommand command)
        {
            command.Undo(Graph);
            RedoStack.Push(command);
        }
    }
}
