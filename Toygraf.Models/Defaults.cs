namespace ToyGraf.Models
{
    using System.Drawing;

    public class Defaults
    {
        public const bool
            GraphDomainGraphWidth = true,
            GraphDomainPolarDegrees = true;

        public const int
            GraphFillTransparencyPercent = 0,
            GraphPaperTransparencyPercent = 0,
            GraphStepCount = 1000;

        public const float
            GraphDomainMaxCartesian = +10,
            GraphDomainMaxPolar = 180,
            GraphDomainMinCartesian = -10,
            GraphDomainMinPolar = 0;

        public static readonly Color
            GraphAxisColour = Color.Black,
            GraphFillColour = Color.Transparent,
            GraphLimitColour = Color.DarkGray,
            GraphPaperColour = Color.White,
            GraphPenColour = Color.Black,
            GraphReticleColour = Color.LightGray;

        public const Elements
            GraphElements = Elements.All;

        public const Optimization
            GraphOptimization = Optimization.HighQuality;

        public const PlotType
            GraphPlotType = PlotType.Cartesian;

        public const Interpolation
            GraphInterpolation = Interpolation.Linear;

        public const TickStyles
            GraphTickStyles = TickStyles.Negative;

        public static readonly Viewport
            GraphViewport = new Viewport(new PointF(0, 0), 22);
    }
}
