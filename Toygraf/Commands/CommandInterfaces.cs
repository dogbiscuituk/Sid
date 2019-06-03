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