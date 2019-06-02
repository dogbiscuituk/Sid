namespace ToyGraf.Commands
{
    partial class CommandProcessor
    {
        private class GraphDomainMaxCartesianCommand : GraphPropertyCommand<float>
        {
            public GraphDomainMaxCartesianCommand(float value) :
                base(value,
                    g => g.DomainMaxCartesian,
                    (g, v) => g.DomainMaxCartesian = v)
            { }

            protected override string Detail => "domain max (Cartesian)";
        }

        private class GraphDomainMaxPolarCommand : GraphPropertyCommand<float>
        {
            public GraphDomainMaxPolarCommand(float value) :
                base(value,
                    g => g.DomainMaxPolar,
                    (g, v) => g.DomainMaxPolar = v)
            { }

            protected override string Detail => "domain max (polar)";
        }

        private class GraphDomainMinCartesianCommand : GraphPropertyCommand<float>
        {
            public GraphDomainMinCartesianCommand(float value) :
                base(value,
                    g => g.DomainMinCartesian,
                    (g, v) => g.DomainMinCartesian = v)
            { }

            protected override string Detail => "domain min (Cartesian)";
        }

        private class GraphDomainMinPolarCommand : GraphPropertyCommand<float>
        {
            public GraphDomainMinPolarCommand(float value) :
                base(value,
                    g => g.DomainMinPolar,
                    (g, v) => g.DomainMinPolar = v)
            { }

            protected override string Detail => "domain min (polar)";
        }

        private class GraphWidthCommand : GraphPropertyCommand<float>
        {
            public GraphWidthCommand(float value) :
                base(value,
                    g => g.Width,
                    (g, v) => g.Width = v)
            { }

            protected override string Detail => "zoom width";
        }
    }
}