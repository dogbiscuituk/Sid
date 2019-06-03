namespace ToyGraf.Commands
{
    using ToyGraf.Models;

    partial class GraphProxy
    {
        private interface IGraphCommand
        {
            bool Do(Graph graph);
            string UndoAction { get; }
            string RedoAction { get; }
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