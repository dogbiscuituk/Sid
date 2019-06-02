namespace ToyGraf.Commands
{
    partial class CommandProcessor
    {
        private class GraphDomainGraphWidthCommand : GraphPropertyCommand<bool>
        {
            public GraphDomainGraphWidthCommand(bool value) :
                base(value, g => g.DomainGraphWidth, (g, v) => g.DomainGraphWidth = v)
            { }

            protected override string Detail => "unlimited domain";
        }

        private class GraphDomainPolarDegreesCommand : GraphPropertyCommand<bool>
        {
            public GraphDomainPolarDegreesCommand(bool value) :
                base(value, g => g.DomainPolarDegrees, (g, v) => g.DomainPolarDegrees = v)
            { }

            protected override string Detail => "polar domain in degrees";
        }
    }
}