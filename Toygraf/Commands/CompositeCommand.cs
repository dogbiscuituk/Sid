namespace ToyGraf.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using ToyGraf.Models;

    partial class GraphProxy
    {
        private class CompositeCommand : GraphCommand<object>
        {
            protected CompositeCommand(params IGraphCommand[] commands)
            {
                Commands.Clear();
                Commands.AddRange(commands);
            }

            public override void Invert()
            {
                var commands = Commands.ToList();
                commands.Reverse();
                commands.ForEach(p => p.Invert());
            }

            public override void Run(Graph graph)
            {
                foreach (var command in Commands)
                    command.Run(graph);
            }

            private List<IGraphCommand> Commands = new List<IGraphCommand>();
            protected override string Detail => "multiple properties";
        }
    }
}
