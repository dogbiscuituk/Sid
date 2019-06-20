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
            GraphDomainPolarDegrees = true,
            TraceVisible = true;

        public const int
            GraphPaperTransparencyPercent = 0,
            StyleFillTransparencyPercent = 0,
            StyleStepCount = 1000;

        public const float
            GraphDomainMaxCartesian = +10,
            GraphDomainMaxPolar = 180,
            GraphDomainMinCartesian = -10,
            GraphDomainMinPolar = -180,
            StylePenWidth = 1;

        public const string
            StyleTexture = null,
            StyleTexturePath = null,
            StyleTitle = "",
            TraceFormula = "";

        public static readonly Color
            GraphAxisColour = Color.Black,
            GraphPaperColour = Color.White,
            GraphReticleColour = Color.LightGray,
            StyleFillColour1 = Color.Transparent,
            StyleFillColour2 = Color.Transparent,
            StyleLimitColour = Color.DarkGray,
            StylePenColour = Color.Black;

        public const BrushType
            StyleBrushType = BrushType.Solid;

        public const DashStyle
            StylePenStyle = DashStyle.Solid;

        public const Elements
            GraphElements = Elements.All;

        public const FillMode
            StyleFillMode = FillMode.Alternate;

        public const HatchStyle
            StyleHatchStyle = HatchStyle.Horizontal;

        public const Interpolation
            GraphInterpolation = Interpolation.Linear;

        public const LinearGradientMode
            StyleGradientMode = LinearGradientMode.Horizontal;

        public const Optimization
            GraphOptimization = Optimization.HighQuality;

        public const PlotType
            GraphPlotType = PlotType.Cartesian;

        public const TickStyles
            GraphTickStyles = TickStyles.Negative;

        public static readonly Viewport
            GraphViewport = new Viewport(new PointF(0, 0), 22);

        public const WrapMode
            StyleWrapMode = WrapMode.Tile;

        public static readonly List<Color> GraphPenColours = new List<Color>
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
