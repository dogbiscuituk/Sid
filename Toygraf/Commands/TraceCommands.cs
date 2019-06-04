namespace ToyGraf.Commands
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using ToyGraf.Controls;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;

    partial class GraphProxy
    {
        internal GraphTrace this[int index] { get => new GraphTrace(this, index); }

        [DefaultProperty("Formula")]
        internal class GraphTrace
        {
            internal GraphTrace()
            {
                GraphProxy = GraphProxyProxy.GraphProxy;
                var index = Graph.Traces.Count;
                Run(new GraphInsertTraceCommand(index));
                Index = index;
            }

            internal GraphTrace(GraphProxy graphProxy, int index)
            {
                GraphProxy = graphProxy;
                Index = index;
            }

            private readonly GraphProxy GraphProxy;
            private readonly int Index;
            private Graph Graph => GraphProxy.Graph;
            private Trace Trace => Graph.Traces[Index];

            [Category("Style")]
            [DefaultValue(typeof(BrushType), "Solid")]
            [DisplayName("Brush type")]
            public BrushType BrushType
            {
                get => Trace.BrushType;
                set => Run(new TraceBrushTypeCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Transparent")]
            [DisplayName("Fill colour #1")]
            public Color FillColour1
            {
                get => Trace.FillColour1;
                set => Run(new TraceFillColour1Command(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Transparent")]
            [DisplayName("Fill colour #2")]
            public Color FillColour2
            {
                get => Trace.FillColour2;
                set => Run(new TraceFillColour2Command(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(0)]
            [DisplayName("Fill transparency %")]
            public int FillTransparencyPercent
            {
                get => Trace.FillTransparencyPercent;
                set => Run(new TraceFillTransparencyPercentCommand(Index, value));
            }

            [DisplayName("Formula")]
            public string Formula
            {
                get => Trace.Formula;
                set => Run(new TraceFormulaCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(LinearGradientMode), "Horizontal")]
            [DisplayName("Gradient mode")]
            public LinearGradientMode GradientMode
            {
                get => Trace.GradientMode;
                set => Run(new TraceGradientModeCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(HatchStyle), "Horizontal")]
            [DisplayName("Hatch style")]
            public HatchStyle HatchStyle
            {
                get => Trace.HatchStyle;
                set => Run(new TraceHatchStyleCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "DarkGray")]
            [DisplayName("Limit colour")]
            public Color LimitColour
            {
                get => Trace.LimitColour;
                set => Run(new TraceLimitColourCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Black")]
            [DisplayName("Pen colour")]
            public Color PenColour
            {
                get => Trace.PenColour;
                set => Run(new TracePenColourCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(DashStyle), "Solid")]
            [DisplayName("Pen style")]
            public DashStyle PenStyle
            {
                get => Trace.PenStyle;
                set => Run(new TracePenStyleCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(1.0f)]
            [DisplayName("Pen size")]
            public float PenWidth
            {
                get => Trace.PenWidth;
                set => Run(new TracePenWidthCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(1000)]
            [DisplayName("# steps")]
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
            [DisplayName("Texture file")]
            public string TexturePath
            {
                get => Trace.TexturePath;
                set => Run(new TraceTexturePathCommand(Index, value));
            }

            [DefaultValue(true)]
            [DisplayName("Visible?")]
            public bool Visible
            {
                get => Trace.Visible;
                set => Run(new TraceVisibleCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(WrapMode), "Tile")]
            [DisplayName("Wrap mode")]
            public WrapMode WrapMode
            {
                get => Trace.WrapMode;
                set => Run(new TraceWrapModeCommand(Index, value));
            }

            public override string ToString() => $"Trace f{Index}";

            private void Run(IGraphCommand command) => GraphProxy.Run(command);
        }
    }
}
