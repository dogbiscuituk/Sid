namespace ToyGraf.Models
{
    public static class Extensions
    {
        public static void CopyFrom(this Graph target, Graph source) => source.CopyTo(target);
        public static void CopyFrom(this Style target, Style source) => source.CopyTo(target);
        public static void CopyFrom(this Trace target, Trace source) => source.CopyTo(target);

        public static void CopyTo(this Graph source, Graph target)
        {
            ((Style)source).CopyTo(target);
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
            target.StepCount = source.StepCount;
            target.TickStyles = source.TickStyles;
            target.Width = source.Width;
        }

        public static void CopyTo(this Style source, Style target)
        {
            target.BrushType = source.BrushType;
            target.FillColour1 = source.FillColour1;
            target.FillColour2 = source.FillColour2;
            target.FillTransparencyPercent = source.FillTransparencyPercent;
            target.GradientMode = source.GradientMode;
            target.HatchStyle = source.HatchStyle;
            target.LimitColour = source.LimitColour;
            target.PenColour = source.PenColour;
            target.PenStyle = source.PenStyle;
            target.PenWidth = source.PenWidth;
            target.Texture = source.Texture;
            target.TexturePath = source.TexturePath;
            target.Title = source.Title;
            target.WrapMode = source.WrapMode;
        }

        public static void CopyTo(this Trace source, Trace target)
        {
            ((Style)source).CopyTo(target);
            target.Formula = source.Formula;
            target.StepCount = source.StepCount;
            target.Visible = source.Visible;
        }
    }
}
