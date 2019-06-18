namespace ToyGraf.Models.Interfaces
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using ToyGraf.Models.Enumerations;

    public interface IStyle
    {
        BrushType BrushType { get; set; }
        Color FillColour1 { get; set; }
        Color FillColour2 { get; set; }
        int FillTransparencyPercent { get; set; }
        LinearGradientMode GradientMode { get; set; }
        HatchStyle HatchStyle { get; set; }
        Color LimitColour { get; set; }
        Color PenColour { get; set; }
        DashStyle PenStyle { get; set; }
        float PenWidth { get; set; }
        int StepCount { get; set; }
        string Texture { get; set; }
        string TexturePath { get; set; }
        string Title { get; set; }
        WrapMode WrapMode { get; set; }
    }
}
