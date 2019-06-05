namespace ToyGraf.Commands
{
    using ToyGraf.Models;

    partial class GraphProxy
    {
        /// <summary>
        /// Common ancestor for GraphInsertStyleCommand and GraphDeleteStyleCommand.
        /// These two descendant classes differ only in the value of a private bool flag,
        /// "Add", which controls their appearance and behaviour. Whereas most commands
        /// are their own inverses, since they just tell the Graph "Swap your property
        /// value with the one I'm carrying", the Invert() method is usually empty. In
        /// the case of these two commands, toggling the Add flag converts one into the
        /// other, prior to the CommandProcessor (now GraphProxy) transfering them
        /// between the Undo and Redo stacks.
        /// </summary>
        private class GraphStyleCommand : StyleCommand<Style>, IGraphStyleCommand
        {
            internal GraphStyleCommand(int index, bool add) : base(index) { Add = add; }

            public bool Add { get; set; }
            public Style Style { get => Value; set => Value = value; }
            public override string UndoAction => GetAction(true);
            public override string RedoAction => GetAction(false);

            public override void Invert() { Add = !Add; }
            public override string ToString() => $"{(Add ? "Add" : "Remove")} style #{Index} = {Detail}";

            public override void Run(Graph graph)
            {
                if (Add)
                {
                    if (Value == null)
                        Value = graph.NewStyle();
                    if (Index >= 0 && Index < graph.Styles.Count)
                        graph.InsertStyle(Index, Value);
                    else if (Index == graph.Styles.Count)
                        graph.AddStyle(Value);
                }
                else
                {
                    Value = graph.Styles[Index];
                    graph.RemoveStyle(Index);
                }
            }

            protected override string Detail => string.Empty;

            private string GetAction(bool undo) => $"function {(Add ^ undo ? "addition" : "removal")}";
        }

        private class GraphInsertStyleCommand : GraphStyleCommand
        {
            internal GraphInsertStyleCommand(int index) : base(index, true) { }
        }

        private class GraphDeleteStyleCommand : GraphStyleCommand
        {
            internal GraphDeleteStyleCommand(int index) : base(index, false) { }
        }
    }
}