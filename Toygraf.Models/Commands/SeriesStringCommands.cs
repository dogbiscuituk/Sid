namespace ToyGraf.Models.Commands
{
    using System;

    public class SeriesStringCommand : SeriesCommand
    {
        protected SeriesStringCommand(int index, Func<Series, string> get, Action<Series, string> set) :
            base(index)
        {
            Get = get;
            Set = set;
        }

        protected string Value;
        protected Func<Series, string> Get;
        protected Action<Series, string> Set;

        protected override void Do(Graph graph)
        {
            var value = Get(graph.Series[Index]);
            Set(graph.Series[Index], value);
            Value = value;
        }
    }

    public class SeriesFormulaCommand : SeriesStringCommand
    {
        public SeriesFormulaCommand(int index) :
            base(index,
                s => s.Formula,
                (s, n) => s.Formula = n)
        { }
    }
}
