namespace Sid.Models
{
    using System.Drawing;

    public class Defaults
    {
        public static readonly Color
            GraphAxisColour = Color.DarkGray,
            GraphFillColour = Color.Transparent,
            GraphGridColour = Color.LightGray,
            GraphLimitColour = Color.DarkGray,
            GraphPaperColour = Color.White,
            GraphPenColour = Color.Black;

        public const int
            GraphStepCount = 16000,
            GraphFillTransparencyPercent = 0,
            GraphPaperTransparencyPercent = 0;

        public static readonly PointF
            GraphLocation = new PointF(-10, -5);

        public static readonly SizeF
            GraphSize = new SizeF(20, 10);

        public const bool
            GraphIsotropic = false;

        public const Elements
            GraphElements = Elements.All;

        public const TickStyles
            GraphTickStyles = TickStyles.Positive;
    }
}
