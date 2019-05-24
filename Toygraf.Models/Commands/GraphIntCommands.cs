namespace ToyGraf.Models.Commands
{
    using System;

    public class GraphIntCommand : GraphPropertyCommand
    {
        protected GraphIntCommand(int value, Func<Graph, int> get, Action<Graph, int> set) :
            base()
        {
            Value = value;
            Get = get;
            Set = set;
        }

        protected new int Value { get => (int)base.Value; set => base.Value = value; }
        protected Func<Graph, int> Get;
        protected Action<Graph, int> Set;

        protected override void Run(Graph graph)
        {
            var n = Get(graph);
            Set(graph, Value);
            Value = n;
        }

        public override string ToString() => $"Graph {Detail}";
    }

    public class GraphFillTransparencyPercentCommand : GraphIntCommand
    {
        public GraphFillTransparencyPercentCommand(int value) :
            base(value,
                g => g.FillTransparencyPercent,
                (g, n) => g.FillTransparencyPercent = n) { }

        public override string Action => "fill colour change";
        protected override string Detail => $"fill transparency = {Value}%";
    }

    public class GraphPaperTransparencyPercentCommand : GraphIntCommand
    {
        public GraphPaperTransparencyPercentCommand(int value) :
            base(value,
                g => g.PaperTransparencyPercent,
                (g, n) => g.PaperTransparencyPercent = n) { }

        public override string Action => "paper colour change";
        protected override string Detail => $"paper transparency = {Value}%";
    }

    public class GraphStepCountCommand : GraphIntCommand
    {
        public GraphStepCountCommand(int value) :
            base(value,
                g => g.StepCount,
                (g, n) => g.StepCount = n) { }

        public override string Action => "#steps change";
        protected override string Detail => $"#steps = {Value}";
    }
}
