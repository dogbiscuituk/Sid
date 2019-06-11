using System.Drawing;
using ToyGraf.Models.Enumerations;
using ToyGraf.Models.Structs;

namespace ToyGraf.Commands
{
    partial class CommandProcessor
    {
        #region bool

        private class GraphDomainGraphWidthCommand : GraphPropertyCommand<bool>
        {
            public GraphDomainGraphWidthCommand(bool value) : base("unlimited domain",
                value, g => g.DomainGraphWidth, (g, v) => g.DomainGraphWidth = v)
            { }
        }

        private class GraphDomainPolarDegreesCommand : GraphPropertyCommand<bool>
        {
            public GraphDomainPolarDegreesCommand(bool value) : base("polar domain in degrees",
                value, g => g.DomainPolarDegrees, (g, v) => g.DomainPolarDegrees = v)
            { }
        }

        #endregion

        #region Color

        private class GraphAxisColourCommand : GraphPropertyCommand<Color>
        {
            public GraphAxisColourCommand(Color value) : base("axis colour",
                value, g => g.AxisColour, (g, v) => g.AxisColour = v)
            { }
        }

        private class GraphFillColour1Command : GraphPropertyCommand<Color>
        {
            public GraphFillColour1Command(Color value) : base("fill colour",
                value, g => g.FillColour1, (g, v) => g.FillColour1 = v)
            { }
        }

        private class GraphFillColour2Command : GraphPropertyCommand<Color>
        {
            public GraphFillColour2Command(Color value) : base("2nd fill colour",
                value, g => g.FillColour2, (g, v) => g.FillColour2 = v)
            { }
        }

        private class GraphLimitColourCommand : GraphPropertyCommand<Color>
        {
            public GraphLimitColourCommand(Color value) : base("limit colour",
                value, g => g.LimitColour, (g, v) => g.LimitColour = v)
            { }
        }

        private class GraphPaperColourCommand : GraphPropertyCommand<Color>
        {
            public GraphPaperColourCommand(Color value) : base("paper colour",
                value, g => g.PaperColour, (g, v) => g.PaperColour = v)
            { }
        }

        private class GraphPenColourCommand : GraphPropertyCommand<Color>
        {
            public GraphPenColourCommand(Color value) : base("pen colour",
                value, g => g.PenColour, (g, v) => g.PenColour = v)
            { }
        }

        private class GraphReticleColourCommand : GraphPropertyCommand<Color>
        {
            public GraphReticleColourCommand(Color value) : base("reticle colour",
                value, g => g.ReticleColour, (g, v) => g.ReticleColour = v)
            { }
        }

        #endregion

        #region enum

        private class GraphElementsCommand : GraphPropertyCommand<Elements>
        {
            public GraphElementsCommand(Elements value) : base("reticle elements",
                value, g => g.Elements, (g, v) => g.Elements = v)
            { }
        }

        private class GraphInterpolationCommand : GraphPropertyCommand<Interpolation>
        {
            public GraphInterpolationCommand(Interpolation value) : base("interpolation",
                value, g => g.Interpolation, (g, v) => g.Interpolation = v)
            { }
        }

        private class GraphOptimizationCommand : GraphPropertyCommand<Optimization>
        {
            public GraphOptimizationCommand(Optimization value) : base("optimization",
                value, g => g.Optimization, (g, v) => g.Optimization = v)
            { }
        }

        private class GraphPlotTypeCommand : GraphPropertyCommand<PlotType>
        {
            public GraphPlotTypeCommand(PlotType value) : base("plot type",
                value, g => g.PlotType, (g, v) => g.PlotType = v)
            { }
        }

        private class GraphTickStylesCommand : GraphPropertyCommand<TickStyles>
        {
            public GraphTickStylesCommand(TickStyles value) : base("tick style",
                value, g => g.TickStyles, (g, v) => g.TickStyles = v)
            { }
        }

        #endregion

        #region float

        private class GraphDomainMaxCartesianCommand : GraphPropertyCommand<float>
        {
            public GraphDomainMaxCartesianCommand(float value) : base("domain max (Cartesian)",
                value, g => g.DomainMaxCartesian, (g, v) => g.DomainMaxCartesian = v)
            { }
        }

        private class GraphDomainMaxPolarCommand : GraphPropertyCommand<float>
        {
            public GraphDomainMaxPolarCommand(float value) : base("domain max (polar)",
                value, g => g.DomainMaxPolar, (g, v) => g.DomainMaxPolar = v)
            { }
        }

        private class GraphDomainMinCartesianCommand : GraphPropertyCommand<float>
        {
            public GraphDomainMinCartesianCommand(float value) : base("domain min (Cartesian)",
                value, g => g.DomainMinCartesian, (g, v) => g.DomainMinCartesian = v)
            { }
        }

        private class GraphDomainMinPolarCommand : GraphPropertyCommand<float>
        {
            public GraphDomainMinPolarCommand(float value) : base("domain min (polar)",
                value, g => g.DomainMinPolar, (g, v) => g.DomainMinPolar = v)
            { }
        }

        #endregion

        #region int

        private class GraphFillTransparencyPercentCommand : GraphPropertyCommand<int>
        {
            public GraphFillTransparencyPercentCommand(int value) : base("fill transparency (%)",
                value, g => g.FillTransparencyPercent, (g, v) => g.FillTransparencyPercent = v)
            { }
        }

        private class GraphPaperTransparencyPercentCommand : GraphPropertyCommand<int>
        {
            public GraphPaperTransparencyPercentCommand(int value) : base("paper transparency (%)",
                value, g => g.PaperTransparencyPercent, (g, v) => g.PaperTransparencyPercent = v)
            { }
        }

        private class GraphStepCountCommand : GraphPropertyCommand<int>
        {
            public GraphStepCountCommand(int value) : base("steps",
                value, g => g.StepCount, (g, n) => g.StepCount = n)
            { }
        }

        #endregion

        #region ViewInfo

        private class GraphViewCommand : GraphPropertyCommand<ViewInfo>
        {
            public GraphViewCommand(ViewInfo value) : base("view",
                value, g => g.ViewInfo, (g, v) => g.ViewInfo = v)
            { }

            public GraphViewCommand(PointF centre, float width) : this(new ViewInfo(centre, width)) { }
            public GraphViewCommand(float x, float y, float width) : this(new ViewInfo(x, y, width)) { }
        }

        #endregion

        #region string

        private class GraphTitleCommand : GraphPropertyCommand<string>
        {
            internal GraphTitleCommand(string value) : base("title",
                value, g => g.Title, (g, v) => g.Title = v)
            { }
        }

        #endregion
    }
}
