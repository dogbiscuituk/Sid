namespace ToyGraf.Models.Commands
{
    using System;

    public class GraphIntCommand : GraphCommand
    {
        protected GraphIntCommand(Func<Graph, int> get, Action<Graph, int> set) :
            base()
        {
            Get = get;
            Set = set;
        }

        protected int Value;
        protected Func<Graph, int> Get;
        protected Action<Graph, int> Set;

        protected override void Do(Graph graph)
        {
            var value = Get(graph);
            Set(graph, value);
            Value = value;
        }
    }

    public class GraphFillTransparencyPercentCommand : GraphIntCommand
    {
        public GraphFillTransparencyPercentCommand() :
            base(g => g.FillTransparencyPercent,
                (g, n) => g.FillTransparencyPercent = n)
        { }
    }

    public class GraphPaperTransparencyPercentCommand : GraphIntCommand
    {
        public GraphPaperTransparencyPercentCommand() :
            base(g => g.PaperTransparencyPercent,
                (g, n) => g.PaperTransparencyPercent = n)
        { }
    }

    public class GraphStepCountCommand : GraphIntCommand
    {
        public GraphStepCountCommand() :
            base(g => g.StepCount,
                (g, n) => g.StepCount = n)
        { }
    }
}
