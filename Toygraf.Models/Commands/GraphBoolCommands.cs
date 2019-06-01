namespace ToyGraf.Models.Commands
{
    using System;

    public class GraphDomainGraphWidthCommand : GraphPropertyCommand<bool>
    {
        public GraphDomainGraphWidthCommand(bool value) :
            base(value, g => g.DomainGraphWidth, (g, v) => g.DomainGraphWidth = v) { }

        protected override string Detail => "unlimited domain";
    }

    public class GraphDomainPolarDegreesCommand : GraphPropertyCommand<bool>
    {
        public GraphDomainPolarDegreesCommand(bool value) :
            base(value, g => g.DomainPolarDegrees, (g, v) => g.DomainPolarDegrees = v) { }

        protected override string Detail => "polar domain in degrees";
    }
}
