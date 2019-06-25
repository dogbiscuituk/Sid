namespace ToyGraf.Models
{
    using ToyGraf.Models.Interfaces;

    public static class Extensions
    {
        public static void CopyFrom(this IGraph target, IGraph source) => source.CopyTo(target);
        public static void CopyFrom(this IStyle target, IStyle source) => source.CopyTo(target);
        public static void CopyFrom(this ITrace target, ITrace source) => source.CopyTo(target);

        public static void CopyTo(this IGraph source, IGraph target)
        {
            ((IStyle)source).CopyTo(target);
            target.AxisColour = source.AxisColour;
            target.Centre = source.Centre;
            target.DomainGraphWidth = source.DomainGraphWidth;
            target.DomainMaxCartesian = source.DomainMaxCartesian;
            target.DomainMaxPolar = source.DomainMaxPolar;
            target.DomainMinCartesian = source.DomainMinCartesian;
            target.DomainMinPolar = source.DomainMinPolar;
            target.DomainPolarDegrees = source.DomainPolarDegrees;
            target.Elements = source.Elements;
            target.Interpolation = source.Interpolation;
            target.Optimization = source.Optimization;
            target.PaperColour = source.PaperColour;
            target.PaperTransparencyPercent = source.PaperTransparencyPercent;
            target.PlotType = source.PlotType;
            target.ReticleColour = source.ReticleColour;
            target.TickStyles = source.TickStyles;
            target.Width = source.Width;
        }

        public static void CopyTo(this IStyle source, IStyle target)
        {
            target.BrushType = source.BrushType;
            target.FillColour1 = source.FillColour1;
            target.FillColour2 = source.FillColour2;
            target.FillMode = source.FillMode;
            target.FillTransparencyPercent = source.FillTransparencyPercent;
            target.GradientMode = source.GradientMode;
            target.HatchStyle = source.HatchStyle;
            target.LimitColour = source.LimitColour;
            target.PenColour = source.PenColour;
            target.PenStyle = source.PenStyle;
            target.PenWidth = source.PenWidth;
            target.StepCount = source.StepCount;
            target.Texture = source.Texture;
            target.TexturePath = source.TexturePath;
            target.Title = source.Title;
            target.WrapMode = source.WrapMode;
        }

        public static void CopyTo(this ITrace source, ITrace target)
        {
            ((IStyle)source).CopyTo(target);
            target.Formula = source.Formula;
            target.Interpolation = source.Interpolation;
            target.Visible = source.Visible;
        }
    }
}
