namespace ToyGraf.Models.Commands
{
    public class SeriesFormulaCommand : SeriesPropertyCommand<string>
    {
        public SeriesFormulaCommand(int index, string value) :
            base(index, value,
                s => s.Formula,
                (s, v) => s.Formula = v) { }

        protected override string Detail => "formula";
    }

    public class SeriesTextureCommand : SeriesPropertyCommand<string>
    {
        public SeriesTextureCommand(int index, string value) :
            base(index, value,
                s => s.Texture,
                (s, v) => s.Texture = v) { }

        protected override string Detail => "texture";
    }

    public class SeriesTexturePathCommand : SeriesPropertyCommand<string>
    {
        public SeriesTexturePathCommand(int index, string value) :
            base(index, value,
                s => s.TexturePath,
                (s, v) => s.TexturePath = v) { }

        protected override string Detail => "texture path";
    }
}
