namespace ToyGraf.Models.Commands
{
    public class GraphFillTransparencyPercentCommand : GraphPropertyCommand<int>
    {
        public GraphFillTransparencyPercentCommand(int value) :
            base(value,
                g => g.FillTransparencyPercent,
                (g, v) => g.FillTransparencyPercent = v) { }

        protected override string Detail => "fill transparency (%)";
    }

    public class GraphPaperTransparencyPercentCommand : GraphPropertyCommand<int>
    {
        public GraphPaperTransparencyPercentCommand(int value) :
            base(value,
                g => g.PaperTransparencyPercent,
                (g, v) => g.PaperTransparencyPercent = v) { }

        protected override string Detail => "paper transparency (%)";
    }

    public class GraphStepCountCommand : GraphPropertyCommand<int>
    {
        public GraphStepCountCommand(int value) :
            base(value,
                g => g.StepCount,
                (g, n) => g.StepCount = n) { }

        protected override string Detail => "#steps";
    }
}
