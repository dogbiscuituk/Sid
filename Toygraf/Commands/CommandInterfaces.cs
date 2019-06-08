namespace ToyGraf.Commands
{
    using ToyGraf.Models;

    partial class GraphProxy
    {
        private interface IGraphCommand
        {
            string UndoAction { get; }
            string RedoAction { get; }

            void Do(Graph graph);
            void Invert();
            void Run(Graph Graph);
        }

        private interface IStyleCommand : IGraphCommand
        {
            int Index { get; set; }
        }

        private interface IStylePropertyCommand : IStyleCommand { }

        private interface IGraphStyleCommand : IStyleCommand
        {
            bool Add { get; set; }
            Style Style { get; set; }
        }

        private interface ITraceCommand : IGraphCommand
        {
            int Index { get; set; }
        }

        private interface ITracePropertyCommand : ITraceCommand { }

        private interface IGraphTraceCommand : ITraceCommand
        {
            bool Add { get; set; }
            Trace Trace { get; set; }
        }
    }
}