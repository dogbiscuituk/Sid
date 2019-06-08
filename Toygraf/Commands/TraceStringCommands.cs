namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
        private class TraceFormulaCommand : TracePropertyCommand<string>
        {
            public TraceFormulaCommand(int index, string value) :
                base(index, value,
                    s => s.Formula,
                    (s, v) => s.Formula = v)
            { }

            protected override string Detail => "formula";
        }

        private class TraceTextureCommand : TracePropertyCommand<string>
        {
            public TraceTextureCommand(int index, string value) :
                base(index, value,
                    s => s.Texture,
                    (s, v) => s.Texture = v)
            { }

            protected override string Detail => "texture";
        }

        private class TraceTexturePathCommand : TracePropertyCommand<string>
        {
            public TraceTexturePathCommand(int index, string value) :
                base(index, value,
                    s => s.TexturePath,
                    (s, v) => s.TexturePath = v)
            { }

            protected override string Detail => "texture path";
        }

        private class TraceTitleCommand : TracePropertyCommand<string>
        {
            public TraceTitleCommand(int index, string value) :
                base(index, value,
                    s => s.Title,
                    (s, v) => s.Title = v)
            { }

            protected override string Detail => "title";
        }
    }
}