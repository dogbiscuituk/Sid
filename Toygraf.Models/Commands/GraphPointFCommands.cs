namespace ToyGraf.Models.Commands
{
    using System;
    using System.Drawing;

    public class GraphPointFCommand : GraphCommand
    {
        protected GraphPointFCommand(Func<Graph, PointF> get, Action<Graph, PointF> set) :
            base()
        {
            Get = get;
            Set = set;
        }

        protected PointF Value;
        protected Func<Graph, PointF> Get;
        protected Action<Graph, PointF> Set;

        protected override void Do(Graph graph)
        {
            var value = Get(graph);
            Set(graph, value);
            Value = value;
        }
    }

    public class GraphCentreCommand : GraphPointFCommand
    {
        public GraphCentreCommand() :
            base(g => g.Centre,
                (g, p) => g.Centre = p)
        { }
    }
}
