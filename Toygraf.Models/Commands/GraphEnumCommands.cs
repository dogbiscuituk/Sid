namespace ToyGraf.Models.Commands
{
    using System;
    using ToyGraf.Models.Enumerations;

    public class GraphEnumCommand : GraphIntCommand
    {
        protected GraphEnumCommand(Func<Graph, object> get, Action<Graph, object> set) :
            base(g => (int)get(g), (g, e) => set(g, e))
        { }
    }

    public class GraphElementsCommand : GraphEnumCommand
    {
        public GraphElementsCommand() :
            base(g => g.Elements,
                (g, e) => g.Elements = (Elements)e)
        { }
    }

    public class GraphInterpolationCommand : GraphEnumCommand
    {
        public GraphInterpolationCommand() :
            base(g => g.Interpolation,
                (g, e) => g.Interpolation = (Interpolation)e)
        { }
    }

    public class GraphOptimizationCommand : GraphEnumCommand
    {
        public GraphOptimizationCommand() :
            base(g => g.Optimization,
                (g, n) => g.Optimization = (Optimization)n)
        { }
    }

    public class GraphPlotTypeCommand : GraphEnumCommand
    {
        public GraphPlotTypeCommand() :
            base(g => g.PlotType,
                (g, n) => g.PlotType = (PlotType)n)
        { }
    }

    public class GraphTickStylesCommand : GraphEnumCommand
    {
        public GraphTickStylesCommand() :
            base(g => g.TickStyles,
                (g, n) => g.TickStyles = (TickStyles)n)
        { }
    }
}
