namespace ToyGraf.Commands
{
    using System.Drawing.Drawing2D;
    using ToyGraf.Models.Enumerations;

    partial class GraphProxy
    {
        private class SeriesBrushTypeCommand : SeriesPropertyCommand<BrushType>
        {
            public SeriesBrushTypeCommand(int index, BrushType value) :
                base(index, value,
                    s => s.BrushType,
                    (s, v) => s.BrushType = v)
            { }

            protected override string Detail => "fill type";
        }

        private class SeriesGradientModeCommand : SeriesPropertyCommand<LinearGradientMode>
        {
            public SeriesGradientModeCommand(int index, LinearGradientMode value) :
                base(index, value,
                    s => s.GradientMode,
                    (s, v) => s.GradientMode = v)
            { }

            protected override string Detail => "gradient type";
        }

        private class SeriesHatchStyleCommand : SeriesPropertyCommand<HatchStyle>
        {
            public SeriesHatchStyleCommand(int index, HatchStyle value) :
                base(index, value,
                    s => s.HatchStyle,
                    (s, v) => s.HatchStyle = v)
            { }

            protected override string Detail => "hatch type";
        }

        private class SeriesPenStyleCommand : SeriesPropertyCommand<DashStyle>
        {
            public SeriesPenStyleCommand(int index, DashStyle value) :
                base(index, value,
                    s => s.PenStyle,
                    (s, v) => s.PenStyle = v)
            { }

            protected override string Detail => "pen type";
        }

        private class SeriesWrapModeCommand : SeriesPropertyCommand<WrapMode>
        {
            public SeriesWrapModeCommand(int index, WrapMode value) :
                base(index, value,
                    s => s.WrapMode,
                    (s, v) => s.WrapMode = v)
            { }

            protected override string Detail => "wrap mode";
        }
    }
}