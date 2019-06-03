namespace ToyGraf.Commands
{
    using System.ComponentModel;
    using System.Drawing;
    using ToyGraf.Models.Enumerations;

    partial class GraphProxy
    {
        [Category("Colour")]
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
        public bool DomainGraphWidth
        {
            get => Graph.DomainGraphWidth;
            set => Run(new GraphDomainGraphWidthCommand(value));
        }

        [Category("Domain")]
        public float DomainMaxCartesian
        {
            get => Graph.DomainMaxCartesian;
            set => Run(new GraphDomainMaxCartesianCommand(value));
        }

        [Category("Domain")]
        public float DomainMaxPolar
        {
            get => Graph.DomainMaxPolar;
            set => Run(new GraphDomainMaxPolarCommand(value));
        }

        [Category("Domain")]
        public float DomainMinCartesian
        {
            get => Graph.DomainMinCartesian;
            set => Run(new GraphDomainMinCartesianCommand(value));
        }

        [Category("Domain")]
        public float DomainMinPolar
        {
            get => Graph.DomainMinPolar;
            set => Run(new GraphDomainMinPolarCommand(value));
        }

        [Category("Domain")]
        public bool DomainPolarDegrees
        {
            get => Graph.DomainPolarDegrees;
            set => Run(new GraphDomainPolarDegreesCommand(value));
        }

        [Category("Style")]
        public Elements Elements
        {
            get => Graph.Elements;
            set => Run(new GraphElementsCommand(value));
        }

        [Category("Colour")]
        public Color FillColour1
        {
            get => Graph.FillColour1;
            set => Run(new GraphFillColour1Command(value));
        }

        [Category("Colour")]
        public Color FillColour2
        {
            get => Graph.FillColour2;
            set => Run(new GraphFillColour2Command(value));
        }

        [Category("Colour")]
        public int FillTransparencyPercent
        {
            get => Graph.FillTransparencyPercent;
            set => Run(new GraphFillTransparencyPercentCommand(value));
        }

        [Category("Style")]
        public Interpolation Interpolation
        {
            get => Graph.Interpolation;
            set => Run(new GraphInterpolationCommand(value));
        }

        [Category("Colour")]
        public Color LimitColour
        {
            get => Graph.LimitColour;
            set => Run(new GraphLimitColourCommand(value));
        }

        [Category("Style")]
        public Optimization Optimization
        {
            get => Graph.Optimization;
            set => Run(new GraphOptimizationCommand(value));
        }

        [Category("Colour")]
        public Color PaperColour
        {
            get => Graph.PaperColour;
            set => Run(new GraphPaperColourCommand(value));
        }

        [Category("Colour")]
        public int PaperTransparencyPercent
        {
            get => Graph.PaperTransparencyPercent;
            set => Run(new GraphPaperTransparencyPercentCommand(value));
        }

        [Category("Colour")]
        public Color PenColour
        {
            get => Graph.PenColour;
            set => Run(new GraphPenColourCommand(value));
        }

        [Category("Style")]
        public PlotType PlotType
        {
            get => Graph.PlotType;
            set => Run(new GraphPlotTypeCommand(value));
        }

        [Category("Colour")]
        public Color ReticleColour
        {
            get => Graph.ReticleColour;
            set => Run(new GraphReticleColourCommand(value));
        }

        [Category("Style")]
        public int StepCount
        {
            get => Graph.StepCount;
            set => Run(new GraphStepCountCommand(value));
        }

        [Category("Style")]
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
