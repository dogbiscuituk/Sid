namespace ToyGraf.Commands
{
    using System.ComponentModel;
    using System.Drawing;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;

    partial class GraphProxy
    {
        [Category("Colour")]
        [DefaultValue(typeof(Color), "Black")]
        public Color AxisColour
        {
            get => Graph.AxisColour;
            set => Run(new GraphAxisColourCommand(value));
        }

        [Category("View")]
        public PointF Centre
        {
            get => Graph.Centre;
            set => Run(new GraphCentreCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(true)]
        public bool DomainGraphWidth
        {
            get => Graph.DomainGraphWidth;
            set => Run(new GraphDomainGraphWidthCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(+10.0f)]
        public float DomainMaxCartesian
        {
            get => Graph.DomainMaxCartesian;
            set => Run(new GraphDomainMaxCartesianCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(+180.0f)]
        public float DomainMaxPolar
        {
            get => Graph.DomainMaxPolar;
            set => Run(new GraphDomainMaxPolarCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(-10.0f)]
        public float DomainMinCartesian
        {
            get => Graph.DomainMinCartesian;
            set => Run(new GraphDomainMinCartesianCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(-180.0f)]
        public float DomainMinPolar
        {
            get => Graph.DomainMinPolar;
            set => Run(new GraphDomainMinPolarCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(true)]
        public bool DomainPolarDegrees
        {
            get => Graph.DomainPolarDegrees;
            set => Run(new GraphDomainPolarDegreesCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(Elements), "All")]
        public Elements Elements
        {
            get => Graph.Elements;
            set => Run(new GraphElementsCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color FillColour1
        {
            get => Graph.FillColour1;
            set => Run(new GraphFillColour1Command(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color FillColour2
        {
            get => Graph.FillColour2;
            set => Run(new GraphFillColour2Command(value));
        }

        [Category("Colour")]
        [DefaultValue(0)]
        public int FillTransparencyPercent
        {
            get => Graph.FillTransparencyPercent;
            set => Run(new GraphFillTransparencyPercentCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(Interpolation), "Linear")]
        public Interpolation Interpolation
        {
            get => Graph.Interpolation;
            set => Run(new GraphInterpolationCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "DarkGray")]
        public Color LimitColour
        {
            get => Graph.LimitColour;
            set => Run(new GraphLimitColourCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(Optimization), "HighQuality")]
        public Optimization Optimization
        {
            get => Graph.Optimization;
            set => Run(new GraphOptimizationCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "White")]
        public Color PaperColour
        {
            get => Graph.PaperColour;
            set => Run(new GraphPaperColourCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(0)]
        public int PaperTransparencyPercent
        {
            get => Graph.PaperTransparencyPercent;
            set => Run(new GraphPaperTransparencyPercentCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "Black")]
        public Color PenColour
        {
            get => Graph.PenColour;
            set => Run(new GraphPenColourCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(PlotType), "Cartesian")]
        public PlotType PlotType
        {
            get => Graph.PlotType;
            set => Run(new GraphPlotTypeCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color ReticleColour
        {
            get => Graph.ReticleColour;
            set => Run(new GraphReticleColourCommand(value));
        }

        [Category("Style")]
        [DefaultValue(1000)]
        public int StepCount
        {
            get => Graph.StepCount;
            set => Run(new GraphStepCountCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(TickStyles), "Negative")]
        public TickStyles TickStyles
        {
            get => Graph.TickStyles;
            set => Run(new GraphTickStylesCommand(value));
        }

        public string Title
        {
            get => Graph.Title;
            set => Run(new GraphTitleCommand(value));
        }

        [Category("View")]
        public float Width
        {
            get => Graph.Width;
            set => Run(new GraphWidthCommand(value));
        }
    }
}
