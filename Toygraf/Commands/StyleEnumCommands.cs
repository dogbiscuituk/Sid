namespace ToyGraf.Commands
{
    using System.Drawing.Drawing2D;
    using ToyGraf.Models.Enumerations;

    partial class GraphProxy
    {
        private class StyleBrushTypeCommand : StylePropertyCommand<BrushType>
        {
            public StyleBrushTypeCommand(int index, BrushType value) :
                base(index, value,
                    s => s.BrushType,
                    (s, v) => s.BrushType = v)
            { }

            protected override string Detail => "fill type";
        }

        private class StyleGradientModeCommand : StylePropertyCommand<LinearGradientMode>
        {
            public StyleGradientModeCommand(int index, LinearGradientMode value) :
                base(index, value,
                    s => s.GradientMode,
                    (s, v) => s.GradientMode = v)
            { }

            protected override string Detail => "gradient type";
        }

        private class StyleHatchStyleCommand : StylePropertyCommand<HatchStyle>
        {
            public StyleHatchStyleCommand(int index, HatchStyle value) :
                base(index, value,
                    s => s.HatchStyle,
                    (s, v) => s.HatchStyle = v)
            { }

            protected override string Detail => "hatch type";
        }

        private class StylePenStyleCommand : StylePropertyCommand<DashStyle>
        {
            public StylePenStyleCommand(int index, DashStyle value) :
                base(index, value,
                    s => s.PenStyle,
                    (s, v) => s.PenStyle = v)
            { }

            protected override string Detail => "pen type";
        }

        private class StyleWrapModeCommand : StylePropertyCommand<WrapMode>
        {
            public StyleWrapModeCommand(int index, WrapMode value) :
                base(index, value,
                    s => s.WrapMode,
                    (s, v) => s.WrapMode = v)
            { }

            protected override string Detail => "wrap mode";
        }
    }
}