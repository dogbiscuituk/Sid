using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using ToyGraf.Models;
using ToyGraf.Models.Enumerations;

namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
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
            public BrushType BrushType
            {
                get => Trace.BrushType;
                set => Run(new TraceBrushTypeCommand(Index, value));
            }

            [Category("Colour")]
            public Color FillColour1
            {
                get => Trace.FillColour1;
                set => Run(new TraceFillColour1Command(Index, value));
            }

            [Category("Colour")]
            public Color FillColour2
            {
                get => Trace.FillColour2;
                set => Run(new TraceFillColour2Command(Index, value));
            }

            [Category("Colour")]
            public int FillTransparencyPercent
            {
                get => Trace.FillTransparencyPercent;
                set => Run(new TraceFillTransparencyPercentCommand(Index, value));
            }

            public string Formula
            {
                get => Trace.Formula;
                set => Run(new TraceFormulaCommand(Index, value));
            }

            [Category("Style")]
            public LinearGradientMode GradientMode
            {
                get => Trace.GradientMode;
                set => Run(new TraceGradientModeCommand(Index, value));
            }

            [Category("Style")]
            public HatchStyle HatchStyle
            {
                get => Trace.HatchStyle;
                set => Run(new TraceHatchStyleCommand(Index, value));
            }

            [Category("Colour")]
            public Color LimitColour
            {
                get => Trace.LimitColour;
                set => Run(new TraceLimitColourCommand(Index, value));
            }

            [Category("Colour")]
            public Color PenColour
            {
                get => Trace.PenColour;
                set => Run(new TracePenColourCommand(Index, value));
            }

            [Category("Style")]
            public DashStyle PenStyle
            {
                get => Trace.PenStyle;
                set => Run(new TracePenStyleCommand(Index, value));
            }

            [Category("Style")]
            public float PenWidth
            {
                get => Trace.PenWidth;
                set => Run(new TracePenWidthCommand(Index, value));
            }

            [Category("Style")]
            public int StepCount
            {
                get => Trace.StepCount;
                set => Run(new TraceStepCountCommand(Index, value));
            }

            [Category("Style")]
            public string Texture
            {
                get => Trace.Texture;
                set => Run(new TraceTextureCommand(Index, value));
            }

            [Category("Style")]
            public string TexturePath
            {
                get => Trace.TexturePath;
                set => Run(new TraceTexturePathCommand(Index, value));
            }

            public bool Visible
            {
                get => Trace.Visible;
                set => Run(new TraceVisibleCommand(Index, value));
            }

            [Category("Style")]
            public WrapMode WrapMode
            {
                get => Trace.WrapMode;
                set => Run(new TraceWrapModeCommand(Index, value));
            }

            private void Run(IGraphCommand command) => GraphProxy.Run(command);
        }
    }
}
