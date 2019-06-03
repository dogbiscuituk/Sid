﻿namespace ToyGraf.Commands
{
    using ToyGraf.Models;

    partial class GraphProxy
    {
        /// <summary>
        /// Common ancestor for GraphInsertSeriesCommand and GraphDeleteSeriesCommand.
        /// These two descendant classes differ only in the value of a private bool flag,
        /// "Add", which controls their appearance and behaviour. Whereas most commands
        /// are their own inverses, since they just tell the Graph "Swap your property
        /// value with the one I'm carrying", the Invert() method is usually empty. In
        /// the case of these two commands, toggling the Add flag converts one into the
        /// other, prior to the CommandProcessor (now GraphProxy) transfering them
        /// between the Undo and Redo stacks.
        /// </summary>
        private class GraphSeriesCommand : SeriesCommand<Series>, IGraphSeriesCommand
        {
            internal GraphSeriesCommand(int index, bool add) : base(index) { Add = add; }

            public bool Add { get; set; }
            public Series Series { get => Value; set => Value = value; }

            public override string UndoAction => GetAction(true);
            public override string RedoAction => GetAction(false);
            public override string ToString() => $"{(Add ? "Add" : "Remove")} function f{Index} = {Detail}";

            protected override string Detail => $"{base.Value?.Formula}";
            protected override void Invert() { Add = !Add; }

            protected override bool Run(Graph graph)
            {
                if (Add)
                {
                    if (Value == null)
                        Value = graph.NewSeries();
                    if (Index >= 0 && Index < graph.Series.Count)
                        graph.InsertSeries(Index, Value);
                    else if (Index == graph.Series.Count)
                        graph.AddSeries(Value);
                }
                else
                {
                    Value = graph.Series[Index];
                    graph.RemoveSeries(Index);
                    Value.InvalidatePaths();
                }
                return true;
            }

            private string GetAction(bool undo) => $"function {(Add ^ undo ? "addition" : "removal")}";
        }

        private class GraphInsertSeriesCommand : GraphSeriesCommand
        {
            internal GraphInsertSeriesCommand(int index) : base(index, true) { }
        }

        private class GraphDeleteSeriesCommand : GraphSeriesCommand
        {
            internal GraphDeleteSeriesCommand(int index) : base(index, false) { }
        }
    }
}