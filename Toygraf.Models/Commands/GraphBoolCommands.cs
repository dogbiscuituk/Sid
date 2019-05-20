namespace ToyGraf.Models.Commands
{
    using System;

    public class GraphBoolCommand : GraphIntCommand
    {
        protected GraphBoolCommand(Func<Graph, bool> get, Action<Graph, bool> set) :
            base(g => get(g) ? 1 : 0, (g, b) => set(g, b != 0))
        { }
    }

    public class GraphDomainGraphWidthCommand : GraphBoolCommand
    {
        public GraphDomainGraphWidthCommand() :
            base(g => g.DomainGraphWidth,
                (g, b) => g.DomainGraphWidth = b)
        { }
    }

    public class GraphDomainPolarDegreesCommand : GraphBoolCommand
    {
        public GraphDomainPolarDegreesCommand() :
            base(g => g.DomainPolarDegrees,
                (g, b) => g.DomainPolarDegrees = b)
        { }
    }
}
