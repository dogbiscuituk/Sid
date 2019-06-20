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
        [DefaultProperty("PenColour")]
        internal class StyleProxy : IStyle
        {
            internal StyleProxy()
            {
                _Style = new Style() { Title = "New Style" };
                Index = -1;
            }

            internal StyleProxy(Style style) => _Style = style;

            internal StyleProxy(CommandProcessor graphProxy, int index)
            {
                CommandProcessor = graphProxy;
                Index = index;
            }

            public int Index;

            private readonly CommandProcessor CommandProcessor;
            private Graph Graph => CommandProcessor.Graph;
            private Style _Style;
            private Style Style => _Style ?? Graph.Styles[Index];

            [Category("Style")]
            [DefaultValue(typeof(BrushType), "Solid")]
            [Description("The type of brush used by the Style to fill in its area of integration.")]
            [DisplayName("Brush type")]
            public BrushType BrushType
            {
                get => Style.BrushType;
                set => Run(new StyleBrushTypeCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Transparent")]
            [Description("The primary fill colour used by the Style. Applies to all brush types except \"Texture\".")]
            [DisplayName("Fill colour #1")]
            public Color FillColour1
            {
                get => Style.FillColour1;
                set => Run(new StyleFillColour1Command(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Transparent")]
            [Description("The secondary fill colour used by the Style. Applies to brush types \"Hatch\", \"Linear Gradient\", and \"Path Gradient\".")]
            [DisplayName("Fill colour #2")]
            public Color FillColour2
            {
                get => Style.FillColour2;
                set => Run(new StyleFillColour2Command(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(FillMode), "Alternate")]
            [Description("How to fill and clip the interior of a closed figure.")]
            [DisplayName("Fill mode")]
            public FillMode FillMode
            {
                get => Style.FillMode;
                set => Run(new StyleFillModeCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(0)]
            [Description("The level of transparency applied to the Style filled areas. Applies to all brush types, including \"Texture\".")]
            [DisplayName("Fill transparency %")]
            public int FillTransparencyPercent
            {
                get => Style.FillTransparencyPercent;
                set => Run(new StyleFillTransparencyPercentCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(LinearGradientMode), "Horizontal")]
            [Description("The direction of the gradient between the two colours used by a \"Linear Gradient\" brush.")]
            [DisplayName("Gradient mode")]
            public LinearGradientMode GradientMode
            {
                get => Style.GradientMode;
                set => Run(new StyleGradientModeCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(HatchStyle), "Horizontal")]
            [Description("The pattern used by a Hatch brush.")]
            [DisplayName("Hatch pattern")]
            public HatchStyle HatchStyle
            {
                get => Style.HatchStyle;
                set => Run(new StyleHatchStyleCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "DarkGray")]
            [Description("The default colour used by the Style to draw asymptotes.")]
            [DisplayName("Limit colour")]
            public Color LimitColour
            {
                get => Style.LimitColour;
                set => Run(new StyleLimitColourCommand(Index, value));
            }

            [Category("Colour")]
            [DefaultValue(typeof(Color), "Black")]
            [Description("The colour of pen used by the Style.")]
            [DisplayName("Pen colour")]
            public Color PenColour
            {
                get => Style.PenColour;
                set => Run(new StylePenColourCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(DashStyle), "Solid")]
            [Description("The style of pen (solid, dotted, dashed, etc.) used to draw the Style.")]
            [DisplayName("Pen style")]
            public DashStyle PenStyle
            {
                get => Style.PenStyle;
                set => Run(new StylePenStyleCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(1f)]
            [Description("The point size of the pen used to draw the Style.")]
            [DisplayName("Pen size")]
            public float PenWidth
            {
                get => Style.PenWidth;
                set => Run(new StylePenWidthCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(1000)]
            [Description("The point size of the pen used to draw the Style.")]
            [DisplayName("Steps")]
            public int StepCount
            {
                get => Style.StepCount;
                set => Run(new StyleStepCountCommand(Index, value));
            }

            [Browsable(false)]
            public string Texture
            {
                get => Style.Texture;
                set => Run(new StyleTextureCommand(Index, value));
            }

            [Category("Style")]
            [Editor(typeof(TgFileNameEditor), typeof(UITypeEditor))]
            [DefaultValue("")]
            [Description("The image file used to provide the Style's \"Texture\" brush.")]
            [DisplayName("Texture file")]
            public string TexturePath
            {
                get => Style.TexturePath;
                set => Run(new StyleTexturePathCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue("")]
            [Description("A title for the style.")]
            [DisplayName("Title")]
            public string Title
            {
                get => Style.Title;
                set => Run(new StyleTitleCommand(Index, value));
            }

            [Category("Style")]
            [DefaultValue(typeof(WrapMode), "Tile")]
            [Description("Determines how copies of a \"Texture\" brush image are tiled, or stitched together, to fill an area.")]
            [DisplayName("Wrap mode")]
            public WrapMode WrapMode
            {
                get => Style.WrapMode;
                set => Run(new StyleWrapModeCommand(Index, value));
            }

            public override string ToString() => !string.IsNullOrWhiteSpace(Title) ? Title : $"Style #{Index}";

            private void Run(ICommand command)
            {
                if (CommandProcessor != null)
                    CommandProcessor.Run(command);
                else if (command is IStylePropertyCommand spc)
                    spc.RunOn(Style);
            }
        }
    }
}
