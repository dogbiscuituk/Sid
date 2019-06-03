namespace ToyGraf.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;

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
            GraphDomainMinPolar = -180,
            GraphPenWidth = 1;

        public static readonly Color
            GraphAxisColour = Color.Black,
            GraphFillColour1 = Color.Transparent,
            GraphFillColour2 = Color.Transparent,
            GraphLimitColour = Color.DarkGray,
            GraphPaperColour = Color.White,
            GraphPenColour = Color.Black,
            GraphReticleColour = Color.LightGray;

        public const BrushType
            GraphBrushType = BrushType.Solid;

        public const DashStyle
            GraphPenStyle = DashStyle.Solid;

        public const Elements
            GraphElements = Elements.All;

        public const HatchStyle
            GraphHatchStyle = HatchStyle.Horizontal;

        public const Interpolation
            GraphInterpolation = Interpolation.Linear;

        public const LinearGradientMode
            GraphGradientMode = LinearGradientMode.Horizontal;

        public const Optimization
            GraphOptimization = Optimization.HighQuality;

        public const PlotType
            GraphPlotType = PlotType.Cartesian;

        public const TickStyles
            GraphTickStyles = TickStyles.Negative;

        public static readonly Viewport
            GraphViewport = new Viewport(new PointF(0, 0), 22);

        public const WrapMode
            GraphWrapMode = WrapMode.Tile;

        private static readonly List<Color> GraphPenColours = new List<Color>
        {
            Color.Black,
            Color.Brown,
            Color.Red,
            Color.Orange,
            Color.Gold,
            Color.Green,
            Color.Cyan,
            Color.Blue,
            Color.Violet,
            Color.Gray,
            Color.Pink
        };

        public static Color GetGraphPenColour(int index) =>
            GraphPenColours[index % GraphPenColours.Count];
    }
}
