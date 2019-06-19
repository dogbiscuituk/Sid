namespace ToyGraf.Commands
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using ToyGraf.Controls;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Interfaces;

    partial class CommandProcessor
    {
        internal TraceProxy this[int index] { get => new TraceProxy(this, index); }

        [DefaultProperty("Formula")]
        internal class TraceProxy : ITrace
        {
            internal TraceProxy()
            {
                _Trace = new Trace();
                Index = -1;
                TheCollectionController.AssignStyle(_Trace);
                _Trace.Title = "New Function";
            }

            internal TraceProxy(Trace trace) => _Trace = trace;

            internal TraceProxy(CommandProcessor graphProxy, int index)
            {
                CommandProcessor = graphProxy;
                Index = index;
            }

            public int Index;

            private readonly CommandProcessor CommandProcessor;
            private Graph Graph => CommandProcessor.Graph;
            private Trace _Trace;
            private Trace Trace => _Trace ?? Graph.Traces[Index];

            [Category("Style")]
            [DefaultValue(typeof(BrushType), "Solid")]
            [Description("The type of brush used by the trace to fill in its area of integration.")]
            [DisplayName("Brush type")]
            public BrushType BrushType
            {
                get => Trace.BrushType;
                set => Run(new TraceBrushTypeCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Transparent")]
            [Description("The primary fill colour used by the trace. Applies to all brush types except \"Texture\".")]
            [DisplayName("Fill colour #1")]
            public Color FillColour1
            {
                get => Trace.FillColour1;
                set => Run(new TraceFillColour1Command(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Transparent")]
            [Description("The secondary fill colour used by the trace. Applies to brush types \"Hatch\", \"Linear Gradient\", and \"Path Gradient\".")]
            [DisplayName("Fill colour #2")]
            public Color FillColour2
            {
                get => Trace.FillColour2;
                set => Run(new TraceFillColour2Command(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(0)]
            [Description("The level of transparency applied to the trace filled areas. Applies to all brush types, including \"Texture\".")]
            [DisplayName("Fill transparency %")]
            public int FillTransparencyPercent
            {
                get => Trace.FillTransparencyPercent;
                set => Run(new TraceFillTransparencyPercentCommand(Index, value));
            }

            [Category("Trace")]
            [Description("The mathematical expression used to draw the trace.")]
            [DisplayName("Formula")]
            public string Formula
            {
                get => Trace.Formula;
                set => Run(new TraceFormulaCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(LinearGradientMode), "Horizontal")]
            [Description("The direction of the gradient between the two colours used by a \"Linear Gradient\" brush.")]
            [DisplayName("Gradient mode")]
            public LinearGradientMode GradientMode
            {
                get => Trace.GradientMode;
                set => Run(new TraceGradientModeCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(HatchStyle), "Horizontal")]
            [Description("The pattern used by a Hatch brush.")]
            [DisplayName("Hatch pattern")]
            public HatchStyle HatchStyle
            {
                get => Trace.HatchStyle;
                set => Run(new TraceHatchStyleCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "DarkGray")]
            [Description("The default colour used by the trace to draw asymptotes.")]
            [DisplayName("Limit colour")]
            public Color LimitColour
            {
                get => Trace.LimitColour;
                set => Run(new TraceLimitColourCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Black")]
            [Description("The colour of pen used by the trace.")]
            [DisplayName("Pen colour")]
            public Color PenColour
            {
                get => Trace.PenColour;
                set => Run(new TracePenColourCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(DashStyle), "Solid")]
            [Description("The style of pen (solid, dotted, dashed, etc.) used to draw the trace.")]
            [DisplayName("Pen style")]
            public DashStyle PenStyle
            {
                get => Trace.PenStyle;
                set => Run(new TracePenStyleCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(1.0f)]
            [Description("The point size of the pen used to draw the trace.")]
            [DisplayName("Pen size")]
            public float PenWidth
            {
                get => Trace.PenWidth;
                set => Run(new TracePenWidthCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(1000)]
            [Description("The minimum number of steps used in calculating points on the trace. In steep sections, this number will be increased dynamically. "
                + "Conversely, in sections where the Formula is undefined, steps will be dropped.")]
            [DisplayName("Steps")]
            public int StepCount
            {
                get => Trace.StepCount;
                set => Run(new TraceStepCountCommand(Index, value));
            }

            [Browsable(false)]
            public string Texture
            {
                get => Trace.Texture;
                set => Run(new TraceTextureCommand(Index, value));
            }

            [Category("Style")]
            [Editor(typeof(TgFileNameEditor), typeof(UITypeEditor))]
            [Description("The image file used to provide the trace's \"Texture\" brush.")]
            [DisplayName("Texture file")]
            public string TexturePath
            {
                get => Trace.TexturePath;
                set => Run(new TraceTexturePathCommand(Index, value));
            }

            [Category("Trace")]
            [Description("A title for the trace.")]
            [DisplayName("Title")]
            public string Title
            {
                get => Trace.Title;
                set => Run(new TraceTitleCommand(Index, value));
            }

            [Category("Trace")]
            [DefaultValue(true)]
            [Description("Take a wild guess.")]
            [DisplayName("Visible?")]
            public bool Visible
            {
                get => Trace.Visible;
                set => Run(new TraceVisibleCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(WrapMode), "Tile")]
            [Description("Determines how copies of a \"Texture\" brush image are tiled, or stitched together, to fill an area.")]
            [DisplayName("Wrap mode")]
            public WrapMode WrapMode
            {
                get => Trace.WrapMode;
                set => Run(new TraceWrapModeCommand(Index, value));
            }

            public override string ToString() => !string.IsNullOrWhiteSpace(Title) ? Title : $"Trace #{Index}";

            private void Run(ICommand command)
            {
                if (CommandProcessor != null)
                    CommandProcessor.Run(command);
                else if (command is ITracePropertyCommand tpc)
                    tpc.RunOn(Trace);
            }
        }
    }
}
