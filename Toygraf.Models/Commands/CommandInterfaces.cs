namespace ToyGraf.Models.Commands
{
    public interface IGraphCommand
    {
        bool Do(Graph graph);
        string UndoAction { get; }
        string RedoAction { get; }
    }

    public interface IGraphPropertyCommand : IGraphCommand
    {
    }

    public interface IGraphSeriesCommand : ISeriesCommand
    {
        bool Add { get; set; }
        Series Value { get; set; }
    }

    public interface ISeriesCommand : IGraphCommand
    {
        int Index { get; set; }
    }

    public interface ISeriesPropertyCommand : ISeriesCommand
    {
    }
}
