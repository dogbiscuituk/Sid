namespace ToyGraf.Models.Commands
{
    using System;
    using System.Drawing;

    public class GraphPointFCommand : GraphCommand
    {
        protected GraphPointFCommand(PointF value, Func<Graph, PointF> get, Action<Graph, PointF> set) :
            base()
        {
            Get = get;
            Set = set;
            Value = value;
        }

        protected PointF Value;
        protected Func<Graph, PointF> Get;
        protected Action<Graph, PointF> Set;

        protected override void Do(Graph graph)
        {
            var p = Get(graph);
            Set(graph, Value);
            Value = p;
        }

        public override string ToString() => $"Graph {Detail} = {Value}";
    }

    public class GraphCentreCommand : GraphPointFCommand
    {
        public GraphCentreCommand(PointF value) :
            base(value,
                g => g.Centre,
                (g, p) => g.Centre = p) { }

        protected override string Detail => "centre";
    }
}
