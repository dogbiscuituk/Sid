namespace ToyGraf.Models.Commands
{
    using System;
    using ToyGraf.Models.Enumerations;

    public class GraphEnumCommand : GraphIntCommand
    {
        protected GraphEnumCommand(object value, Func<Graph, object> get, Action<Graph, object> set) :
            base((int)value, g => (int)get(g), (g, e) => set(g, e)) { }

        public override string ToString() => $"Graph {Detail}";
    }

    public class GraphElementsCommand : GraphEnumCommand
    {
        public GraphElementsCommand(Elements value) :
            base(value, g => g.Elements, (g, e) => g.Elements = (Elements)e) { }

        protected override string Detail => $"elements = {(Elements)Value}";
    }

    public class GraphInterpolationCommand : GraphEnumCommand
    {
        public GraphInterpolationCommand(Interpolation value) :
            base(value, g => g.Interpolation, (g, e) => g.Interpolation = (Interpolation)e) { }

        protected override string Detail => $"interpolation = {(Interpolation)Value}";
    }

    public class GraphOptimizationCommand : GraphEnumCommand
    {
        public GraphOptimizationCommand(Optimization value) :
            base(value, g => g.Optimization, (g, n) => g.Optimization = (Optimization)n) { }

        protected override string Detail => $"optimization = {(Optimization)Value}";
    }

    public class GraphPlotTypeCommand : GraphEnumCommand
    {
        public GraphPlotTypeCommand(PlotType value) :
            base(value, g => g.PlotType, (g, n) => g.PlotType = (PlotType)n) { }

        protected override string Detail => $"plot type = {(PlotType)Value}";
    }

    public class GraphTickStylesCommand : GraphEnumCommand
    {
        public GraphTickStylesCommand(TickStyles value) :
            base(value, g => g.TickStyles, (g, n) => g.TickStyles = (TickStyles)n) { }

        protected override string Detail => $"tick style = {(TickStyles)Value}";
    }
}
