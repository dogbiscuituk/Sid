namespace Sid.Models
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
            GraphStepCount = 16000;

        public const float
            GraphDomainMaxCartesian = +10,
            GraphDomainMaxPolar = 180,
            GraphDomainMinCartesian = -10,
            GraphDomainMinPolar = 0;

        public static readonly Color
            GraphAxisColour = Color.DarkGray,
            GraphFillColour = Color.Transparent,
            GraphGridColour = Color.LightGray,
            GraphLimitColour = Color.DarkGray,
            GraphPaperColour = Color.White,
            GraphPenColour = Color.Black;

        public const Elements
            GraphElements = Elements.All;

        public const Optimization
            GraphOptimization = Optimization.Default;

        public const PlotType
            GraphPlotType = PlotType.Cartesian;

        public static readonly PointF
            GraphLocation = new PointF(-10, -5);

        public static readonly SizeF
            GraphSize = new SizeF(20, 10);

        public const TickStyles
            GraphTickStyles = TickStyles.Positive;
    }
}
