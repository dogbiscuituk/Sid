namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesStringCommand : SeriesCommand
    {
        protected SeriesStringCommand(int index, string value, Func<Series, string> get, Action<Series, string> set) :
            base(index)
        {
            Value = value;
            Get = get;
            Set = set;
        }

        protected new string Value { get => (string)base.Value; set => base.Value = value; }
        protected Func<Series, string> Get;
        protected Action<Series, string> Set;

        protected override void Do(Graph graph)
        {
            var s = Get(graph.Series[Index]);
            Set(graph.Series[Index], Value);
            Value = s;
        }

        public override string ToString() => $"f{Index}(x,t) {Detail} = \"{Value}\"";
    }

    public class SeriesFormulaCommand : SeriesStringCommand
    {
        public SeriesFormulaCommand(int index, string value) :
            base(index, value,
                s => s.Formula,
                (s, n) => s.Formula = n) { }

        public override string Action => "formula change";
        protected override string Detail => "formula";
    }
}
