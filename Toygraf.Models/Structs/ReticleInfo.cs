namespace ToyGraf.Models.Structs
{
    using System.Drawing;
    using Newtonsoft.Json;
    using ToyGraf.Models.Enumerations;

    public struct ReticleInfo
    {
        public ReticleInfo(PlotType plotType, Viewport viewport, DomainInfo domainInfo, Color axisColour, Color reticleColour,
            float penWidth, Elements elements, TickStyles tickStyles, bool vertical = false)
        {
            PlotType = plotType;
            Viewport = viewport;
            DomainInfo = domainInfo;
            AxisColour = axisColour;
            ReticleColour = reticleColour;
            PenWidth = penWidth;
            Elements = elements;
            TickStyles = tickStyles;
            Vertical = vertical;
        }

        public PlotType PlotType { get; set; }
        public Viewport Viewport { get; set; }
        public DomainInfo DomainInfo { get; set; }
        public Color AxisColour { get; set; }
        public Color ReticleColour { get; set; }
        public float PenWidth { get; set; }
        public Elements Elements { get; set; }
        public TickStyles TickStyles { get; set; }
        [JsonIgnore] public bool Vertical { get; set; }

        public bool Xaxis { get => !Vertical && (Elements & Elements.Xaxis) != 0; }
        public bool Yaxis { get => Vertical && (Elements & Elements.Yaxis) != 0; }
        public bool Axis { get => Xaxis || Yaxis; }

        public bool Hwires { get => !Vertical && (Elements & Elements.HorizontalWires) != 0; }
        public bool Vwires { get => Vertical && (Elements & Elements.VerticalWires) != 0; }
        public bool Wires { get => Hwires || Vwires; }

        public bool Xticks { get => Vertical && (Elements & Elements.Xticks) != 0; }
        public bool Yticks { get => !Vertical && (Elements & Elements.Yticks) != 0; }
        public bool Ticks { get => Xticks || Yticks; }

        public bool Xcalibration { get => Vertical && (Elements & Elements.Xcalibration) != 0; }
        public bool Ycalibration { get => !Vertical && (Elements & Elements.Ycalibration) != 0; }
        public bool Calibration { get => Xcalibration || Ycalibration; }

        public bool Polar { get => PlotType == PlotType.Polar; }
        public bool TickPositive { get => (TickStyles & TickStyles.Positive) != 0; }
        public bool TickNegative { get => (TickStyles & TickStyles.Negative) != 0; }
    }
}
