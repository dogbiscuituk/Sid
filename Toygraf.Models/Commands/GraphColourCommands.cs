namespace ToyGraf.Models.Commands
{
    using System;
    using System.Drawing;
    using ToyGraf.Expressions;

    public class GraphColourCommand : GraphIntCommand
    {
        protected GraphColourCommand(Color colour, Func<Graph, Color> get, Action<Graph, Color> set) :
            base(colour.ToArgb(), c => get(c).ToArgb(), (g, c) => set(g, Color.FromArgb(c))) { }

        public override string ToString() =>
            $"Graph {Detail} colour = {Color.FromArgb(Value).GetName()}";
    }

    public class GraphAxisColourCommand : GraphColourCommand
    {
        public GraphAxisColourCommand(Color colour) :
            base(colour, g => g.AxisColour, (g, c) => g.AxisColour = c) { }

        protected override string Detail => "axis";
    }

    public class GraphFillColourCommand : GraphColourCommand
    {
        public GraphFillColourCommand(Color colour) :
            base(colour, g => g.FillColour, (g, c) => g.FillColour = c) { }

        protected override string Detail => "fill";
    }

    public class GraphLimitColourCommand : GraphColourCommand
    {
        public GraphLimitColourCommand(Color colour) :
            base(colour, g => g.LimitColour, (g, c) => g.LimitColour = c) { }


        protected override string Detail => "limit";
    }

    public class GraphPaperColourCommand : GraphColourCommand
    {
        public GraphPaperColourCommand(Color colour) :
            base(colour, g => g.PaperColour, (g, c) => g.PaperColour = c) { }

        protected override string Detail => "paper";
    }

    public class GraphPenColourCommand : GraphColourCommand
    {
        public GraphPenColourCommand(Color colour) :
            base(colour, g => g.PenColour, (g, c) => g.PenColour = c) { }

        protected override string Detail => "pen";
    }

    public class GraphReticleColourCommand : GraphColourCommand
    {
        public GraphReticleColourCommand(Color colour) :
            base(colour, g => g.ReticleColour, (g, c) => g.ReticleColour = c) { }

        protected override string Detail => "reticle";
    }
}
