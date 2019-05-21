namespace ToyGraf.Models.Commands
{
    using System;

    public class GraphFloatCommand : GraphCommand
    {
        protected GraphFloatCommand(float value, Func<Graph, float> get, Action<Graph, float> set) :
            base()
        {
            Value = value;
            Get = get;
            Set = set;
        }

        protected new float Value { get => (float)base.Value; set => base.Value = value; }
        protected Func<Graph, float> Get;
        protected Action<Graph, float> Set;

        protected override void Do(Graph graph)
        {
            var f = Get(graph);
            Set(graph, Value);
            Value = f;
        }

        public override string ToString() => $"Graph {Detail} = {Value}";
    }

    public class GraphDomainMaxCartesianCommand : GraphFloatCommand
    {
        public GraphDomainMaxCartesianCommand(float value) :
            base(value,
                g => g.DomainMaxCartesian,
                (g, f) => g.DomainMaxCartesian = f) { }

        public override string Action => "domain edit";
        protected override string Detail => $"domain max (Cartesian)";
    }

    public class GraphDomainMaxPolarCommand : GraphFloatCommand
    {
        public GraphDomainMaxPolarCommand(float value) :
            base(value,
                g => g.DomainMaxPolar,
                (g, f) => g.DomainMaxPolar = f) { }

        public override string Action => "domain edit";
        protected override string Detail => $"domain max (polar)";
    }

    public class GraphDomainMinCartesianCommand : GraphFloatCommand
    {
        public GraphDomainMinCartesianCommand(float value) :
            base(value,
                g => g.DomainMinCartesian,
                (g, f) => g.DomainMinCartesian = f) { }

        public override string Action => "domain edit";
        protected override string Detail => $"domain min (Cartesian)";
    }

    public class GraphDomainMinPolarCommand : GraphFloatCommand
    {
        public GraphDomainMinPolarCommand(float value) :
            base(value,
                g => g.DomainMinPolar,
                (g, f) => g.DomainMinPolar = f) { }

        public override string Action => "domain edit";
        protected override string Detail => $"domain min (polar)";
    }

    public class GraphWidthCommand : GraphFloatCommand
    {
        public GraphWidthCommand(float value) :
            base(value,
                g => g.Width,
                (g, f) => g.Width = f) { }

        public override string Action => "zoom";
        protected override string Detail => $"zoom width";
    }
}
