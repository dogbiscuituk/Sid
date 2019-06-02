namespace ToyGraf.Commands
{
    using System.Drawing;

    partial class CommandProcessor
    {
        private class GraphAxisColourCommand : GraphPropertyCommand<Color>
        {
            public GraphAxisColourCommand(Color colour) :
                base(colour, g => g.AxisColour, (g, v) => g.AxisColour = v)
            { }

            protected override string Detail => "axis colour";
        }

        private class GraphFillColour1Command : GraphPropertyCommand<Color>
        {
            public GraphFillColour1Command(Color colour) :
                base(colour, g => g.FillColour1, (g, v) => g.FillColour1 = v)
            { }

            protected override string Detail => "primary fill colour";
        }

        private class GraphFillColour2Command : GraphPropertyCommand<Color>
        {
            public GraphFillColour2Command(Color colour) :
                base(colour, g => g.FillColour2, (g, v) => g.FillColour2 = v)
            { }

            protected override string Detail => "secondary fill colour";
        }

        private class GraphLimitColourCommand : GraphPropertyCommand<Color>
        {
            public GraphLimitColourCommand(Color colour) :
                base(colour, g => g.LimitColour, (g, v) => g.LimitColour = v)
            { }

            protected override string Detail => "limit colour";
        }

        private class GraphPaperColourCommand : GraphPropertyCommand<Color>
        {
            public GraphPaperColourCommand(Color colour) :
                base(colour, g => g.PaperColour, (g, v) => g.PaperColour = v)
            { }

            protected override string Detail => "paper colour";
        }

        private class GraphPenColourCommand : GraphPropertyCommand<Color>
        {
            public GraphPenColourCommand(Color colour) :
                base(colour, g => g.PenColour, (g, v) => g.PenColour = v)
            { }

            protected override string Detail => "pen colour";
        }

        private class GraphReticleColourCommand : GraphPropertyCommand<Color>
        {
            public GraphReticleColourCommand(Color colour) :
                base(colour, g => g.ReticleColour, (g, v) => g.ReticleColour = v)
            { }

            protected override string Detail => "reticle colour";
        }
    }
}