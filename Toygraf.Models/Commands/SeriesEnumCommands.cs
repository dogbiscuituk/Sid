namespace ToyGraf.Models.Commands
{
    using System;
    using System.Drawing.Drawing2D;
    using ToyGraf.Models.Enumerations;

    public abstract class SeriesEnumCommand : SeriesIntCommand
    {
        protected SeriesEnumCommand(int index, object value, Func<Series, object> get, Action<Series, object> set) :
            base(index, (int)value, s => (int)get(s), (s, e) => set(s, e)) { }
    }

    public class SeriesBrushTypeCommand : SeriesEnumCommand
    {
        public SeriesBrushTypeCommand(int index, BrushType value) :
            base(index, value,
                s => s.BrushType,
                (s, n) => s.BrushType = (BrushType)n) { }

        protected override string Detail => "fill type";
    }

    public class SeriesGradientModeCommand : SeriesEnumCommand
    {
        public SeriesGradientModeCommand(int index, LinearGradientMode value) :
            base(index, value,
                s => s.GradientMode,
                (s, n) => s.GradientMode = (LinearGradientMode)n) { }

        protected override string Detail => "gradient type";
    }

    public class SeriesHatchStyleCommand : SeriesEnumCommand
    {
        public SeriesHatchStyleCommand(int index, HatchStyle value) :
            base(index, value,
                s => s.HatchStyle,
                (s, n) => s.HatchStyle = (HatchStyle)n) { }

        protected override string Detail => "hatch type";
    }

    public class SeriesPenStyleCommand : SeriesEnumCommand
    {
        public SeriesPenStyleCommand(int index, DashStyle value) :
            base(index, value,
                s => s.PenStyle,
                (s, n) => s.PenStyle = (DashStyle)n) { }

        protected override string Detail => "pen type";
    }
}
