namespace ToyGraf.Models.Commands
{
    using System;

    public class GraphFloatCommand : GraphCommand
    {
        protected GraphFloatCommand(Func<Graph, float> get, Action<Graph, float> set) :
            base()
        {
            Get = get;
            Set = set;
        }

        protected float Value;
        protected Func<Graph, float> Get;
        protected Action<Graph, float> Set;

        protected override void Do(Graph graph)
        {
            var value = Get(graph);
            Set(graph, value);
            Value = value;
        }
    }

    public class GraphDomainMaxCartesianCommand : GraphFloatCommand
    {
        public GraphDomainMaxCartesianCommand() :
            base(g => g.DomainMaxCartesian,
                (g, f) => g.DomainMaxCartesian = f)
        { }
    }

    public class GraphDomainMaxPolarCommand : GraphFloatCommand
    {
        public GraphDomainMaxPolarCommand() :
            base(g => g.DomainMaxPolar,
                (g, f) => g.DomainMaxPolar = f)
        { }
    }

    public class GraphDomainMinCartesianCommand : GraphFloatCommand
    {
        public GraphDomainMinCartesianCommand() :
            base(g => g.DomainMinCartesian,
                (g, f) => g.DomainMinCartesian = f)
        { }
    }

    public class GraphDomainMinPolarCommand : GraphFloatCommand
    {
        public GraphDomainMinPolarCommand() :
            base(g => g.DomainMinPolar,
                (g, f) => g.DomainMinPolar = f)
        { }
    }

    public class GraphWidthCommand : GraphFloatCommand
    {
        public GraphWidthCommand() :
            base(g => g.Width,
                (g, f) => g.Width = f)
        { }
    }
}
