namespace ToyGraf.Commands
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Linq;
    using ToyGraf.Controls;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;

    [DefaultProperty("Traces")]
    partial class GraphProxy
    {
        [Category("Colour")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("The colour used to draw the graph's axes.")]
        [DisplayName("Axis colour")]
        public Color AxisColour
        {
            get => Graph.AxisColour;
            set => Run(new GraphAxisColourCommand(value));
        }

        [Category("View")]
        [Description("The Cartesian co-ordinates of the point at the centre of the graph display, in domain units.")]
        [DisplayName("Centre")]
        public PointF Centre
        {
            get => Graph.Centre;
            set => Run(new GraphCentreCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(true)]
        [Description("If true, Cartesian plots will try to use the graph display width as the domain, ignoring any other settings.")]
        [DisplayName("Use graph width?")]
        public bool DomainGraphWidth
        {
            get => Graph.DomainGraphWidth;
            set => Run(new GraphDomainGraphWidthCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(10f)]
        [Description("The right endpoint of a Cartesian plot's domain.")]
        [DisplayName("Cartesian maximum")]
        public float DomainMaxCartesian
        {
            get => Graph.DomainMaxCartesian;
            set => Run(new GraphDomainMaxCartesianCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(180f)]
        [Description("The finishing angle of a polar plot's domain.")]
        [DisplayName("Polar maximum")]
        public float DomainMaxPolar
        {
            get => Graph.DomainMaxPolar;
            set => Run(new GraphDomainMaxPolarCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(-10f)]
        [Description("The left endpoint of a Cartesian plot's domain.")]
        [DisplayName("Cartesian minimum")]
        public float DomainMinCartesian
        {
            get => Graph.DomainMinCartesian;
            set => Run(new GraphDomainMinCartesianCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(-180f)]
        [Description("The starting angle of a polar plot's domain.")]
        [DisplayName("Polar minimum")]
        public float DomainMinPolar
        {
            get => Graph.DomainMinPolar;
            set => Run(new GraphDomainMinPolarCommand(value));
        }

        [Category("Domain")]
        [DefaultValue(true)]
        [Description("If true, the domain of a polar plot is shown in degrees; otherwise, it is shown in radians.")]
        [DisplayName("Polar in degrees?")]
        public bool DomainPolarDegrees
        {
            get => Graph.DomainPolarDegrees;
            set => Run(new GraphDomainPolarDegreesCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(Elements), "All")]
        [Description("The set of visual elements included in the graph reticle (grid).")]
        [DisplayName("Reticle elements")]
        public Elements Elements
        {
            get => Graph.Elements;
            set => Run(new GraphElementsCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "Transparent")]
        [Description("The default primary fill colour used by traces. Applies to all brush types except \"Texture\".")]
        [DisplayName("Fill colour #1")]
        public Color FillColour1
        {
            get => Graph.FillColour1;
            set => Run(new GraphFillColour1Command(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "Transparent")]
        [Description("The default secondary fill colour used by traces. Applies to brush types \"Hatch\", \"Linear Gradient\", and \"Path Gradient\".")]
        [DisplayName("Fill colour #2")]
        public Color FillColour2
        {
            get => Graph.FillColour2;
            set => Run(new GraphFillColour2Command(value));
        }

        [Category("Colour")]
        [DefaultValue(0)]
        [Description("The default level of transparency applied to trace filled areas. Applies to all brush types, including \"Texture\".")]
        [DisplayName("Fill transparency %")]
        public int FillTransparencyPercent
        {
            get => Graph.FillTransparencyPercent;
            set => Run(new GraphFillTransparencyPercentCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(Interpolation), "Linear")]
        [Description("Traces are drawn and filled by joining up the computed points, using either straight lines (\"Linear\"), or cubic curves (\"CardinalSpline\").")]
        [DisplayName("Interpolation")]
        public Interpolation Interpolation
        {
            get => Graph.Interpolation;
            set => Run(new GraphInterpolationCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "DarkGray")]
        [Description("The default colour used by traces to draw asymptotes.")]
        [DisplayName("Limit colour")]
        public Color LimitColour
        {
            get => Graph.LimitColour;
            set => Run(new GraphLimitColourCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(Optimization), "HighQuality")]
        [Description("Controls a number of settings in the graphics drawing code.")]
        [DisplayName("Optimization")]
        public Optimization Optimization
        {
            get => Graph.Optimization;
            set => Run(new GraphOptimizationCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "White")]
        [Description("The background colour used in the graph.")]
        [DisplayName("Paper colour")]
        public Color PaperColour
        {
            get => Graph.PaperColour;
            set => Run(new GraphPaperColourCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(0)]
        [Description("The level of transparency in the background colour used in the graph.")]
        [DisplayName("Paper transparency %")]
        public int PaperTransparencyPercent
        {
            get => Graph.PaperTransparencyPercent;
            set => Run(new GraphPaperTransparencyPercentCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("The default pen colour used by traces in the graph.")]
        [DisplayName("Pen colour")]
        public Color PenColour
        {
            get => Graph.PenColour;
            set => Run(new GraphPenColourCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(PlotType), "Cartesian")]
        [Description("The type of plot produced. Cartesian is the normal x-y plot. Polar plots use x and y respectively as the angle and distance from the origin.")]
        [DisplayName("Plot type")]
        public PlotType PlotType
        {
            get => Graph.PlotType;
            set => Run(new GraphPlotTypeCommand(value));
        }

        [Category("Colour")]
        [DefaultValue(typeof(Color), "LightGray")]
        [Description("The colour used to draw the graph reticle (grid).")]
        [DisplayName("Reticle colour")]
        public Color ReticleColour
        {
            get => Graph.ReticleColour;
            set => Run(new GraphReticleColourCommand(value));
        }

        [Category("Style")]
        [DefaultValue(1000)]
        [Description("The default minimum number of steps used in calculating points on a trace. In steep sections of a trace, this number will be increased dynamically. "
            + "Conversely, in sections where the Formula is undefined, steps will be dropped.")]
        [DisplayName("# steps")]
        public int StepCount
        {
            get => Graph.StepCount;
            set => Run(new GraphStepCountCommand(value));
        }

        [Category("Style")]
        [DefaultValue(typeof(TickStyles), "Negative")]
        [Description("The style of tick mark used to indicate axis gradations.")]
        [DisplayName("Tick style")]
        public TickStyles TickStyles
        {
            get => Graph.TickStyles;
            set => Run(new GraphTickStylesCommand(value));
        }

        [Category("Graph")]
        [Description("A title for the graph.")]
        [DisplayName("Title")]
        public string Title
        {
            get => Graph.Title;
            set => Run(new GraphTitleCommand(value));
        }

        [Category("Graph")]
        [Description("The list of traces in the graph.")]
        [DisplayName("Traces")]
        [Editor(typeof(TgCollectionEditor), typeof(UITypeEditor))]
        public List<GraphTrace> Traces
        {
            get => Graph.Traces.Select(s => new GraphTrace(this, Graph.Traces.IndexOf(s))).ToList();
        }

        [Category("View")]
        [DefaultValue(22f)]
        [Description("The horizontal dimension of the graph display, in domain units.")]
        [DisplayName("Width")]
        public float Width
        {
            get => Graph.Width;
            set => Run(new GraphWidthCommand(value));
        }
    }
}
