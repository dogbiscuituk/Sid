namespace ToyGraf.Commands
{
    using System.Drawing.Drawing2D;
    using ToyGraf.Models.Enumerations;

    partial class GraphProxy
    {
        private class TraceBrushTypeCommand : TracePropertyCommand<BrushType>
        {
            public TraceBrushTypeCommand(int index, BrushType value) :
                base(index, value,
                    s => s.BrushType,
                    (s, v) => s.BrushType = v)
            { }

            protected override string Detail => "fill type";
        }

        private class TraceGradientModeCommand : TracePropertyCommand<LinearGradientMode>
        {
            public TraceGradientModeCommand(int index, LinearGradientMode value) :
                base(index, value,
                    s => s.GradientMode,
                    (s, v) => s.GradientMode = v)
            { }

            protected override string Detail => "gradient type";
        }

        private class TraceHatchStyleCommand : TracePropertyCommand<HatchStyle>
        {
            public TraceHatchStyleCommand(int index, HatchStyle value) :
                base(index, value,
                    s => s.HatchStyle,
                    (s, v) => s.HatchStyle = v)
            { }

            protected override string Detail => "hatch type";
        }

        private class TracePenStyleCommand : TracePropertyCommand<DashStyle>
        {
            public TracePenStyleCommand(int index, DashStyle value) :
                base(index, value,
                    s => s.PenStyle,
                    (s, v) => s.PenStyle = v)
            { }

            protected override string Detail => "pen type";
        }

        private class TraceWrapModeCommand : TracePropertyCommand<WrapMode>
        {
            public TraceWrapModeCommand(int index, WrapMode value) :
                base(index, value,
                    s => s.WrapMode,
                    (s, v) => s.WrapMode = v)
            { }

            protected override string Detail => "wrap mode";
        }
    }
}