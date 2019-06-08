namespace ToyGraf.Commands
{
    partial class GraphProxy
    {
        private class StyleFillTransparencyPercentCommand : StylePropertyCommand<int>
        {
            public StyleFillTransparencyPercentCommand(int index, int value) :
                base(index, value,
                    s => s.FillTransparencyPercent,
                    (s, v) => s.FillTransparencyPercent = v)
            { }

            protected override string Detail => "fill transparency (%)";
        }
    }
}