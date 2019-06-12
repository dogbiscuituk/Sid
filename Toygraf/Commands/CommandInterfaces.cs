namespace ToyGraf.Commands
{
    using ToyGraf.Models;

    partial class CommandProcessor
    {
        private interface ICommand
        {
            int Index { get; }
            string RedoAction { get; }
            string UndoAction { get; }

            bool Do(Graph graph);
            void Invert();
            bool Run(Graph Graph);
        }

        private interface IPropertyCommand : ICommand { }

        private interface IGraphPropertyCommand : IPropertyCommand
        {
            void RunOn(Graph graph);
        }

        private interface IStylePropertyCommand : IPropertyCommand
        {
            void RunOn(Style style);
        }

        private interface ITracePropertyCommand : IPropertyCommand
        {
            void RunOn(Trace trace);
        }

        private interface ICollectionCommand : ICommand
        {
            bool Adding { get; set; }
        }

        private interface IStylesCommand : ICollectionCommand
        {
            Style Value { get; set; }
        }

        private interface ITracesCommand : ICollectionCommand
        {
            Trace Value { get; set; }
        }
    }
}
