namespace ToyGraf.Commands
{
    partial class CommandProcessor
    {
        private class GraphTitleCommand : GraphPropertyCommand<string>
        {
            internal GraphTitleCommand(string value) :
                base(value,
                    g => g.Title,
                    (g, v) => g.Title = v)
            { }

            protected override string Detail => "title";
        }
    }
}
