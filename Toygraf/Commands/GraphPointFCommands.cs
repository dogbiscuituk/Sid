namespace ToyGraf.Commands
{
    using System.Drawing;

    partial class CommandProcessor
    {
        private class GraphCentreCommand : GraphPropertyCommand<PointF>
        {
            public GraphCentreCommand(PointF value) :
                base(value, g => g.Centre, (g, v) => g.Centre = v)
            { }

            public GraphCentreCommand(float x, float y) : this(new PointF(x, y)) { }

            protected override string Detail => "centre";
        }
    }
}