namespace ToyGraf.Models.Commands
{
    using System;
    using System.Drawing;

    public class GraphColourCommand : GraphIntCommand
    {
        protected GraphColourCommand(Func<Graph, Color> get, Action<Graph, Color> set) :
            base(c => get(c).ToArgb(), (g, c) => set(g, Color.FromArgb(c)))
        { }
    }

    public class GraphAxisColourCommand : GraphColourCommand
    {
        public GraphAxisColourCommand() :
            base(g => g.AxisColour, (g, c) => g.AxisColour = c)
        { }
    }

    public class GraphFillColourCommand : GraphColourCommand
    {
        public GraphFillColourCommand() :
            base(g => g.FillColour, (g, c) => g.FillColour = c)
        { }
    }

    public class GraphLimitColourCommand : GraphColourCommand
    {
        public GraphLimitColourCommand() :
            base(g => g.LimitColour, (g, c) => g.LimitColour = c)
        { }
    }

    public class GraphPaperColourCommand : GraphColourCommand
    {
        public GraphPaperColourCommand() :
            base(g => g.PaperColour, (g, c) => g.PaperColour = c)
        { }
    }

    public class GraphPenColourCommand : GraphColourCommand
    {
        public GraphPenColourCommand() :
            base(g => g.PenColour, (g, c) => g.PenColour = c)
        { }
    }

    public class GraphReticleColourCommand : GraphColourCommand
    {
        public GraphReticleColourCommand() :
            base(g => g.ReticleColour, (g, c) => g.ReticleColour = c)
        { }
    }
}
