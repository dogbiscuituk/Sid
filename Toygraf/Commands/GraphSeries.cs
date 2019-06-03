using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using ToyGraf.Models;
using ToyGraf.Models.Enumerations;

namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
        internal class GraphSeries
        {
            internal GraphSeries()
            {
                GraphProxy = GraphProxyProxy.GraphProxy;
                var index = Graph.Series.Count;
                Run(new GraphInsertSeriesCommand(index));
                Index = index;
            }

            internal GraphSeries(GraphProxy graphProxy, int index)
            {
                GraphProxy = graphProxy;
                Index = index;
            }

            private readonly GraphProxy GraphProxy;
            private readonly int Index;
            private Graph Graph => GraphProxy.Graph;
            private Series Series => Graph.Series[Index];

            public BrushType BrushType
            {
                get => Series.BrushType;
                set => Run(new SeriesBrushTypeCommand(Index, value));
            }

            [Category("Colours")]
            public Color FillColour1
            {
                get => Series.FillColour1;
                set => Run(new SeriesFillColour1Command(Index, value));
            }

            [Category("Colours")]
            public Color FillColour2
            {
                get => Series.FillColour2;
                set => Run(new SeriesFillColour2Command(Index, value));
            }

            [Category("Colours")]
            public int FillTransparencyPercent
            {
                get => Series.FillTransparencyPercent;
                set => Run(new SeriesFillTransparencyPercentCommand(Index, value));
            }

            public string Formula
            {
                get => Series.Formula;
                set => Run(new SeriesFormulaCommand(Index, value));
            }

            public LinearGradientMode GradientMode
            {
                get => Series.GradientMode;
                set => Run(new SeriesGradientModeCommand(Index, value));
            }

            public HatchStyle HatchStyle
            {
                get => Series.HatchStyle;
                set => Run(new SeriesHatchStyleCommand(Index, value));
            }

            [Category("Colours")]
            public Color LimitColour
            {
                get => Series.LimitColour;
                set => Run(new SeriesLimitColourCommand(Index, value));
            }

            [Category("Colours")]
            public Color PenColour
            {
                get => Series.PenColour;
                set => Run(new SeriesPenColourCommand(Index, value));
            }

            public DashStyle PenStyle
            {
                get => Series.PenStyle;
                set => Run(new SeriesPenStyleCommand(Index, value));
            }

            public float PenWidth
            {
                get => Series.PenWidth;
                set => Run(new SeriesPenWidthCommand(Index, value));
            }

            public int StepCount
            {
                get => Series.StepCount;
                set => Run(new SeriesStepCountCommand(Index, value));
            }

            public string Texture
            {
                get => Series.Texture;
                set => Run(new SeriesTextureCommand(Index, value));
            }

            public string TexturePath
            {
                get => Series.TexturePath;
                set => Run(new SeriesTexturePathCommand(Index, value));
            }

            public bool Visible
            {
                get => Series.Visible;
                set => Run(new SeriesVisibleCommand(Index, value));
            }

            public WrapMode WrapMode
            {
                get => Series.WrapMode;
                set => Run(new SeriesWrapModeCommand(Index, value));
            }

            private void Run(IGraphCommand command) => GraphProxy.Run(command);
        }
    }
}
