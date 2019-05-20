namespace ToyGraf.Models.Commands
{
    using System;

    public class GraphBoolCommand : GraphIntCommand
    {
        protected GraphBoolCommand(bool value, Func<Graph, bool> get, Action<Graph, bool> set) :
            base(value ? 1 : 0, g => get(g) ? 1 : 0, (g, b) => set(g, b != 0)) { }

        public override string ToString() =>
            $"Graph {Detail} = {Value != 0}";
    }

    public class GraphDomainGraphWidthCommand : GraphBoolCommand
    {
        public GraphDomainGraphWidthCommand(bool value) :
            base(value, g => g.DomainGraphWidth, (g, b) => g.DomainGraphWidth = b) { }

        protected override string Detail => "unlimited domain";
    }

    public class GraphDomainPolarDegreesCommand : GraphBoolCommand
    {
        public GraphDomainPolarDegreesCommand(bool value) :
            base(value, g => g.DomainPolarDegrees, (g, b) => g.DomainPolarDegrees = b) { }

        protected override string Detail => "polar domain in degrees";
    }
}
