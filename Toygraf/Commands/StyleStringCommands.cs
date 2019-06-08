namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
        private class StyleTextureCommand : StylePropertyCommand<string>
        {
            public StyleTextureCommand(int index, string value) :
                base(index, value,
                    s => s.Texture,
                    (s, v) => s.Texture = v)
            { }

            protected override string Detail => "texture";
        }

        private class StyleTexturePathCommand : StylePropertyCommand<string>
        {
            public StyleTexturePathCommand(int index, string value) :
                base(index, value,
                    s => s.TexturePath,
                    (s, v) => s.TexturePath = v)
            { }

            protected override string Detail => "texture path";
        }

        private class StyleTitleCommand : StylePropertyCommand<string>
        {
            public StyleTitleCommand(int index, string value) :
                base(index, value,
                    s => s.Title,
                    (s, v) => s.Title = v)
            { }

            protected override string Detail => "title";
        }
    }
}