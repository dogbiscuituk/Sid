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
            private Trace trace => Graph.Traces[Index];

            public BrushType BrushType
            {
                get => trace.BrushType;
                set => Run(new TraceBrushTypeCommand(Index, value));
            }

            [Category("Colours")]
            public Color FillColour1
            {
                get => trace.FillColour1;
                set => Run(new TraceFillColour1Command(Index, value));
            }

            [Category("Colours")]
            public Color FillColour2
            {
                get => trace.FillColour2;
                set => Run(new TraceFillColour2Command(Index, value));
            }

            [Category("Colours")]
            public int FillTransparencyPercent
            {
                get => trace.FillTransparencyPercent;
                set => Run(new TraceFillTransparencyPercentCommand(Index, value));
            }

            public string Formula
            {
                get => trace.Formula;
                set => Run(new TraceFormulaCommand(Index, value));
            }

            public LinearGradientMode GradientMode
            {
                get => trace.GradientMode;
                set => Run(new TraceGradientModeCommand(Index, value));
            }

            public HatchStyle HatchStyle
            {
                get => trace.HatchStyle;
                set => Run(new TraceHatchStyleCommand(Index, value));
            }

            [Category("Colours")]
            public Color LimitColour
            {
                get => trace.LimitColour;
                set => Run(new TraceLimitColourCommand(Index, value));
            }

            [Category("Colours")]
            public Color PenColour
            {
                get => trace.PenColour;
                set => Run(new TracePenColourCommand(Index, value));
            }

            public DashStyle PenStyle
            {
                get => trace.PenStyle;
                set => Run(new TracePenStyleCommand(Index, value));
            }

            public float PenWidth
            {
                get => trace.PenWidth;
                set => Run(new TracePenWidthCommand(Index, value));
            }

            public int StepCount
            {
                get => trace.StepCount;
                set => Run(new TraceStepCountCommand(Index, value));
            }

            public string Texture
            {
                get => trace.Texture;
                set => Run(new TraceTextureCommand(Index, value));
            }

            public string TexturePath
            {
                get => trace.TexturePath;
                set => Run(new TraceTexturePathCommand(Index, value));
            }

            public bool Visible
            {
                get => trace.Visible;
                set => Run(new TraceVisibleCommand(Index, value));
            }

            public WrapMode WrapMode
            {
                get => trace.WrapMode;
                set => Run(new TraceWrapModeCommand(Index, value));
            }

            private void Run(IGraphCommand command) => GraphProxy.Run(command);
        }
    }
}
