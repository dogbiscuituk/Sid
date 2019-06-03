namespace ToyGraf.Commands
{
    using ToyGraf.Models;

    partial class GraphProxy
    {
        /// <summary>
        /// Common ancestor for GraphInsertTraceCommand and GraphDeleteTraceCommand.
        /// These two descendant classes differ only in the value of a private bool flag,
        /// "Add", which controls their appearance and behaviour. Whereas most commands
        /// are their own inverses, since they just tell the Graph "Swap your property
        /// value with the one I'm carrying", the Invert() method is usually empty. In
        /// the case of these two commands, toggling the Add flag converts one into the
        /// other, prior to the CommandProcessor (now GraphProxy) transfering them
        /// between the Undo and Redo stacks.
        /// </summary>
        private class GraphTraceCommand : TraceCommand<Trace>, IGraphTraceCommand
        {
            internal GraphTraceCommand(int index, bool add) : base(index) { Add = add; }

            public bool Add { get; set; }
            public Trace Trace { get => Value; set => Value = value; }
            public override string UndoAction => GetAction(true);
            public override string RedoAction => GetAction(false);

            public override void Invert() { Add = !Add; }
            public override string ToString() => $"{(Add ? "Add" : "Remove")} function f{Index} = {Detail}";

            public override void Run(Graph graph)
            {
                if (Add)
                {
                    if (Value == null)
                        Value = graph.NewTrace();
                    if (Index >= 0 && Index < graph.Traces.Count)
                        graph.InsertTrace(Index, Value);
                    else if (Index == graph.Traces.Count)
                        graph.AddTrace(Value);
                }
                else
                {
                    Value = graph.Traces[Index];
                    graph.RemoveTrace(Index);
                    Value.InvalidatePaths();
                }
            }

            protected override string Detail => $"{base.Value?.Formula}";

            private string GetAction(bool undo) => $"function {(Add ^ undo ? "addition" : "removal")}";
        }

        private class GraphInsertTraceCommand : GraphTraceCommand
        {
            internal GraphInsertTraceCommand(int index) : base(index, true) { }
        }

        private class GraphDeleteTraceCommand : GraphTraceCommand
        {
            internal GraphDeleteTraceCommand(int index) : base(index, false) { }
        }
    }
}