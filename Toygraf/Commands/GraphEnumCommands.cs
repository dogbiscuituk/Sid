namespace ToyGraf.Commands
{
    using ToyGraf.Models.Enumerations;

    partial class CommandProcessor
    {
        private class GraphElementsCommand : GraphPropertyCommand<Elements>
        {
            public GraphElementsCommand(Elements value) :
                base(value, g => g.Elements, (g, v) => g.Elements = v)
            { }

            protected override string Detail => "reticle elements";
        }

        private class GraphInterpolationCommand : GraphPropertyCommand<Interpolation>
        {
            public GraphInterpolationCommand(Interpolation value) :
                base(value, g => g.Interpolation, (g, v) => g.Interpolation = v)
            { }

            protected override string Detail => "interpolation";
        }

        private class GraphOptimizationCommand : GraphPropertyCommand<Optimization>
        {
            public GraphOptimizationCommand(Optimization value) :
                base(value, g => g.Optimization, (g, v) => g.Optimization = v)
            { }

            protected override string Detail => "optimization";
        }

        private class GraphPlotTypeCommand : GraphPropertyCommand<PlotType>
        {
            public GraphPlotTypeCommand(PlotType value) :
                base(value, g => g.PlotType, (g, v) => g.PlotType = v)
            { }

            protected override string Detail => "plot type";
        }

        private class GraphTickStylesCommand : GraphPropertyCommand<TickStyles>
        {
            public GraphTickStylesCommand(TickStyles value) :
                base(value, g => g.TickStyles, (g, v) => g.TickStyles = v)
            { }

            protected override string Detail => "tick style";
        }
    }
}