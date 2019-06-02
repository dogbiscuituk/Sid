namespace ToyGraf.Commands
{
    using ToyGraf.Models;

    partial class CommandProcessor
    {
        private interface IGraphCommand
        {
            bool Do(Graph graph);
            string UndoAction { get; }
            string RedoAction { get; }
        }

        private interface ISeriesCommand : IGraphCommand
        {
            int Index { get; set; }
        }

        private interface ISeriesPropertyCommand : ISeriesCommand { }

        private interface IGraphSeriesCommand : ISeriesCommand
        {
            bool Add { get; set; }
            Series Series { get; set; }
        }
    }
}