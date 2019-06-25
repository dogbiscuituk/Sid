namespace ToyGraf.Commands
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using ToyGraf.Expressions;
    using ToyGraf.Expressions.Enumerations;
    using ToyGraf.Models.Enumerations;

    partial class CommandProcessor
    {
        #region Add/Remove

        private class TraceInsertCommand : TracesCommand
        {
            internal TraceInsertCommand(int index) : base(index, true) { }
        }

        private class TraceDeleteCommand : TracesCommand
        {
            internal TraceDeleteCommand(int index) : base(index, false) { }
        }

        #endregion

        #region bool

        private class TraceVisibleCommand : TracePropertyCommand<YN>
        {
            public TraceVisibleCommand(int index, YN value) : base(index, "visibility",
                value, s => s.Visible.BoolToYN(), (s, v) => s.Visible = v.BoolFromYN())
            { }
        }

        #endregion

        #region Color

        private class TraceFillColour1Command : TracePropertyCommand<Color>
        {
            public TraceFillColour1Command(int index, Color value) : base(index, "fill colour",
                value, s => s.FillColour1, (s, v) => s.FillColour1 = v)
            { }
        }

        private class TraceFillColour2Command : TracePropertyCommand<Color>
        {
            public TraceFillColour2Command(int index, Color value) : base(index, "2nd fill colour",
                value, s => s.FillColour2, (s, v) => s.FillColour2 = v)
            { }
        }

        private class TraceLimitColourCommand : TracePropertyCommand<Color>
        {
            public TraceLimitColourCommand(int index, Color value) : base(index, "limit colour",
                value, s => s.LimitColour, (s, v) => s.LimitColour = v)
            { }
        }

        private class TracePenColourCommand : TracePropertyCommand<Color>
        {
            public TracePenColourCommand(int index, Color value) : base(index, "pen colour",
                value, s => s.PenColour, (s, v) => s.PenColour = v)
            { }
        }

        #endregion

        #region enum

        private class TraceBrushTypeCommand : TracePropertyCommand<BrushType>
        {
            public TraceBrushTypeCommand(int index, BrushType value) : base(index, "fill type",
                value, s => s.BrushType, (s, v) => s.BrushType = v)
            { }
        }

        private class TraceFillModeCommand : TracePropertyCommand<FillMode>
        {
            public TraceFillModeCommand(int index, FillMode value) : base(index, "fill mode",
                value, s => s.FillMode, (s, v) => s.FillMode = v)
            { }
        }

        private class TraceGradientModeCommand : TracePropertyCommand<LinearGradientMode>
        {
            public TraceGradientModeCommand(int index, LinearGradientMode value) : base(index, "gradient type",
                value, s => s.GradientMode, (s, v) => s.GradientMode = v)
            { }
        }

        private class TraceHatchStyleCommand : TracePropertyCommand<HatchStyle>
        {
            public TraceHatchStyleCommand(int index, HatchStyle value) : base(index, "hatch type",
                value, s => s.HatchStyle, (s, v) => s.HatchStyle = v)
            { }
        }

        private class TraceInterpolationCommand : TracePropertyCommand<Interpolation>
        {
            public TraceInterpolationCommand(int index, Interpolation value) : base(index, "interpolation",
                value, s => s.Interpolation, (s, v) => s.Interpolation = v)
            { }
        }
       
        private class TracePenStyleCommand : TracePropertyCommand<DashStyle>
        {
            public TracePenStyleCommand(int index, DashStyle value) : base(index, "pen type",
                value, s => s.PenStyle, (s, v) => s.PenStyle = v)
            { }
        }

        private class TraceWrapModeCommand : TracePropertyCommand<WrapMode>
        {
            public TraceWrapModeCommand(int index, WrapMode value) : base(index, "wrap mode",
                value, s => s.WrapMode, (s, v) => s.WrapMode = v)
            { }
        }

        #endregion

        #region float

        private class TracePenWidthCommand : TracePropertyCommand<float>
        {
            public TracePenWidthCommand(int index, float value) : base(index, "pen size",
                value, s => s.PenWidth, (s, v) => s.PenWidth = v)
            { }
        }

        #endregion

        #region int

        private class TraceFillTransparencyPercentCommand : TracePropertyCommand<int>
        {
            public TraceFillTransparencyPercentCommand(int index, int value) : base(index, "fill transparency (%)",
                value, s => s.FillTransparencyPercent, (s, v) => s.FillTransparencyPercent = v)
            { }
        }

        private class TraceStepCountCommand : TracePropertyCommand<int>
        {
            public TraceStepCountCommand(int index, int value) : base(index, "steps",
                value, s => s.StepCount, (s, v) => s.StepCount = v)
            { }
        }

        #endregion

        #region string

        private class TraceFormulaCommand : TracePropertyCommand<string>
        {
            public TraceFormulaCommand(int index, string value) : base(index, "formula",
                value, s => s.Formula, (s, v) => s.Formula = v)
            { }
        }

        private class TraceTextureCommand : TracePropertyCommand<string>
        {
            public TraceTextureCommand(int index, string value) : base(index, "texture",
                value, s => s.Texture, (s, v) => s.Texture = v)
            { }
        }

        private class TraceTexturePathCommand : TracePropertyCommand<string>
        {
            public TraceTexturePathCommand(int index, string value) : base(index, "texture path",
                value, s => s.TexturePath, (s, v) => s.TexturePath = v)
            { }
        }

        private class TraceTitleCommand : TracePropertyCommand<string>
        {
            public TraceTitleCommand(int index, string value) : base(index, "title",
                value, s => s.Title, (s, v) => s.Title = v)
            { }
        }

        #endregion
    }
}
