namespace ToyGraf.Models.Interfaces
{
    using System.Drawing;
    using ToyGraf.Models.Enumerations;

    public interface IGraph : IStyle
    {
        Color AxisColour { get; set; }
        PointF Centre { get; set; }
        bool DomainGraphWidth { get; set; }
        float DomainMaxCartesian { get; set; }
        float DomainMaxPolar { get; set; }
        float DomainMinCartesian { get; set; }
        float DomainMinPolar { get; set; }
        bool DomainPolarDegrees { get; set; }
        Elements Elements { get; set; }
        Interpolation Interpolation { get; set; }
        Optimization Optimization { get; set; }
        Color PaperColour { get; set; }
        int PaperTransparencyPercent { get; set; }
        PlotType PlotType { get; set; }
        Color ReticleColour { get; set; }
        TickStyles TickStyles { get; set; }
        float Width { get; set; }
    }
}
