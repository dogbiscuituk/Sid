namespace ToyGraf.Commands
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using ToyGraf.Models.Enumerations;

    partial class CommandProcessor
    {
        #region Add/Remove

        private class StyleInsertCommand : StylesCommand
        {
            internal StyleInsertCommand(int index) : base(index, true) { }
        }

        private class StyleDeleteCommand : StylesCommand
        {
            internal StyleDeleteCommand(int index) : base(index, false) { }
        }

        #endregion

        #region Color

        private class StyleFillColour1Command : StylePropertyCommand<Color>
        {
            public StyleFillColour1Command(int index, Color value) : base(index, "fill colour",
                value, s => s.FillColour1, (s, v) => s.FillColour1 = v)
            { }
        }

        private class StyleFillColour2Command : StylePropertyCommand<Color>
        {
            public StyleFillColour2Command(int index, Color value) : base(index, "2nd fill colour",
                value, s => s.FillColour2, (s, v) => s.FillColour2 = v)
            { }
        }

        private class StyleLimitColourCommand : StylePropertyCommand<Color>
        {
            public StyleLimitColourCommand(int index, Color value) : base(index, "limit colour",
                value, s => s.LimitColour, (s, v) => s.LimitColour = v)
            { }
        }

        private class StylePenColourCommand : StylePropertyCommand<Color>
        {
            public StylePenColourCommand(int index, Color value) : base(index, "pen colour",
                value, s => s.PenColour, (s, v) => s.PenColour = v)
            { }
        }

        #endregion

        #region enum

        private class StyleBrushTypeCommand : StylePropertyCommand<BrushType>
        {
            public StyleBrushTypeCommand(int index, BrushType value) : base(index, "fill type",
                value, s => s.BrushType, (s, v) => s.BrushType = v)
            { }
        }

        private class StyleFillModeCommand : StylePropertyCommand<FillMode>
        {
            public StyleFillModeCommand(int index, FillMode value) : base(index, "fill mode",
                value, s => s.FillMode, (s, v) => s.FillMode = v)
            { }
        }

        private class StyleGradientModeCommand : StylePropertyCommand<LinearGradientMode>
        {
            public StyleGradientModeCommand(int index, LinearGradientMode value) : base(index, "gradient type",
                value, s => s.GradientMode, (s, v) => s.GradientMode = v)
            { }
        }

        private class StyleHatchStyleCommand : StylePropertyCommand<HatchStyle>
        {
            public StyleHatchStyleCommand(int index, HatchStyle value) : base(index, "hatch type",
                value, s => s.HatchStyle, (s, v) => s.HatchStyle = v)
            { }
        }

        private class StylePenStyleCommand : StylePropertyCommand<DashStyle>
        {
            public StylePenStyleCommand(int index, DashStyle value) : base(index, "pen type",
                value, s => s.PenStyle, (s, v) => s.PenStyle = v)
            { }
        }

        private class StyleWrapModeCommand : StylePropertyCommand<WrapMode>
        {
            public StyleWrapModeCommand(int index, WrapMode value) : base(index, "wrap mode",
                value, s => s.WrapMode, (s, v) => s.WrapMode = v)
            { }
        }

        #endregion

        #region float

        private class StylePenWidthCommand : StylePropertyCommand<float>
        {
            public StylePenWidthCommand(int index, float value) : base(index, "pen size",
                value, s => s.PenWidth, (s, v) => s.PenWidth = v)
            { }
        }

        #endregion

        #region int

        private class StyleFillTransparencyPercentCommand : StylePropertyCommand<int>
        {
            public StyleFillTransparencyPercentCommand(int index, int value) : base(index, "fill transparency (%)",
                value, s => s.FillTransparencyPercent, (s, v) => s.FillTransparencyPercent = v)
            { }
        }

        private class StyleStepCountCommand : StylePropertyCommand<int>
        {
            public StyleStepCountCommand(int index, int value) : base(index, "step count",
                value, s => s.StepCount, (s, v) => s.StepCount = v)
            { }
        }

        #endregion

        #region string

        private class StyleTextureCommand : StylePropertyCommand<string>
        {
            public StyleTextureCommand(int index, string value) : base(index, "texture",
                value, s => s.Texture, (s, v) => s.Texture = v)
            { }
        }

        private class StyleTexturePathCommand : StylePropertyCommand<string>
        {
            public StyleTexturePathCommand(int index, string value) : base(index, "texture path",
                value, s => s.TexturePath, (s, v) => s.TexturePath = v)
            { }
        }

        private class StyleTitleCommand : StylePropertyCommand<string>
        {
            public StyleTitleCommand(int index, string value) : base(index, "title",
                value, s => s.Title, (s, v) => s.Title = v)
            { }
        }

        #endregion
    }
}
