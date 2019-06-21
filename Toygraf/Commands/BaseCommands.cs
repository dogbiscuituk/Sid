namespace ToyGraf.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ToyGraf.Expressions;
    using ToyGraf.Models;

    partial class CommandProcessor
    {
        #region Abstract Base Command

        private abstract class Command<TValue> : ICommand
        {
            protected Command(int index = 0) { Index = index; }

            public int Index { get; private set; }
            public abstract string RedoAction { get; }
            public abstract string UndoAction { get; }
            public TValue Value { get; set; }

            /// <summary>
            /// Invoke the Run method of the command, then immediately invert
            /// the command in readiness for its transfer between the Undo and
            /// Redo stacks. Since most commands are their own inverses (they
            /// just tell the Graph "Swap your property value with the one I'm
            /// carrying", the Invert() method is almost always empty. See the
            /// GraphInsertTraceCommand and GraphDeleteTraceCommand classes
            /// for two notable exceptions to this rule.
            /// </summary>
            /// <param name="graph"></param>
            public bool Do(Graph graph)
            {
                var result = Run(graph);
                Invert();
                return result;
            }

            public virtual void Invert() { }
            public abstract bool Run(Graph graph);

            protected string Detail { get; set; }
            protected abstract string Target { get; }
        }

        #endregion

        #region Abstract Property Commands

        private abstract class PropertyCommand<TItem, TValue> : Command<TValue>, IPropertyCommand
        {
            protected PropertyCommand(int index, string detail,
                TValue value, Func<TItem, TValue> get, Action<TItem, TValue> set)
                : base(index)
            {
                Detail = detail;
                Value = value;
                Get = get;
                Set = set;
            }

            public override string RedoAction => Action;
            public override string UndoAction => Action;

            public override bool Run(Graph graph)
            {
                TValue value = GetValue(graph);
                var result = !Equals(value, Value);
                if (result)
                {
                    SetValue(graph, Value);
                    Value = value;
                }
                return result;
            }

            public override string ToString() => $"{Target} {Detail} = {Value}";
            private string Action => $"{Detail} change";

            protected Func<TItem, TValue> Get;
            protected Action<TItem, TValue> Set;

            protected abstract TItem GetItem(Graph graph);
            protected TValue GetValue(Graph graph) => Get(GetItem(graph));
            protected void SetValue(Graph graph, TValue value) => Set(GetItem(graph), value);
        }

        private abstract class GraphPropertyCommand<TValue> : PropertyCommand<Graph, TValue>, IGraphPropertyCommand
        {
            protected GraphPropertyCommand(string detail,
                TValue value, Func<Graph, TValue> get, Action<Graph, TValue> set)
                : base(0, detail, value, get, set) { }

            public void RunOn(Graph graph) => Set(graph, Value);
            protected override string Target => "graph";
            protected override Graph GetItem(Graph graph) => graph;
        }

        private abstract class StylePropertyCommand<TValue> : PropertyCommand<Style, TValue>, IStylePropertyCommand
        {
            protected StylePropertyCommand(int index, string detail,
                TValue value, Func<Style, TValue> get, Action<Style, TValue> set)
                : base(index, detail, value, get, set) { }

            public void RunOn(Style style) => Set(style, Value);
            protected override string Target => $"style #{Index}";
            protected override Style GetItem(Graph graph) => graph.Styles[Index];
        }

        private abstract class TracePropertyCommand<TValue> : PropertyCommand<Trace, TValue>, ITracePropertyCommand
        {
            protected TracePropertyCommand(int index, string detail,
                TValue value, Func<Trace, TValue> get, Action<Trace, TValue> set)
                : base(index, detail, value, get, set) { }

            public void RunOn(Trace trace) => Set(trace, Value);
            protected override string Target => $"f{Index.ToSubscript()}";
            protected override Trace GetItem(Graph graph) => graph.Traces[Index];
        }

        #endregion

        #region Collection Commands

        /// <summary>
        /// Common ancestor for collection management (item insert and delete) commands.
        /// Those descendant classes differ only in the value of a private boolean flag,
        /// "Adding", which controls their appearance and behaviour. While most commands
        /// are their own inverses, since they just tell the Graph "Swap your property
        /// value with the one I'm carrying", the Invert() method is usually empty. But
        /// in the case of these commands, toggling the "Adding" flag converts one into
        /// the other, prior to the command processor moving them between the Undo and
        /// Redo stacks.
        /// </summary>
        private abstract class CollectionCommand<TItem> : Command<TItem>, ICollectionCommand
        {
            internal CollectionCommand(int index, bool add) : base(index) { Adding = add; }

            public bool Adding { get; set; }
            public override string RedoAction => GetAction(false);
            public override string UndoAction => GetAction(true);

            public override void Invert() { Adding = !Adding; }
            public override string ToString() => $"{(Adding ? "Add" : "Remove")} {Target}";

            public override bool Run(Graph graph)
            {
                var count = GetItemsCount(graph);
                if (Adding)
                {
                    if (Value == null)
                        Value = GetNewItem(graph);
                    if (Index >= 0 && Index < count)
                        InsertItem(graph);
                    else if (Index == count)
                        AddItem(graph);
                }
                else
                    RemoveItem(graph);
                return true;
            }

            protected abstract int GetItemsCount(Graph graph);
            protected abstract TItem GetNewItem(Graph graph);

            protected abstract void AddItem(Graph graph);
            protected abstract void InsertItem(Graph graph);
            protected abstract void RemoveItem(Graph graph);

            private string GetAction(bool undo) => $"{Detail} {(Adding ^ undo ? "addition" : "removal")}";
        }

        private class StylesCommand : CollectionCommand<Style>, IStylesCommand
        {
            internal StylesCommand(int index, bool add) : base(index, add)
            {
                Detail = "style";
            }

            protected override string Target => $"style #{Index}";

            protected override int GetItemsCount(Graph graph) => graph.Styles.Count;
            protected override Style GetNewItem(Graph graph) => graph.NewStyle();

            protected override void AddItem(Graph graph) => graph.AddStyle(Value);
            protected override void InsertItem(Graph graph) => graph.InsertStyle(Index, Value);
            protected override void RemoveItem(Graph graph) => graph.RemoveStyle(Index);
        }

        private class TracesCommand : CollectionCommand<Trace>, ITracesCommand
        {
            internal TracesCommand(int index, bool add) : base(index, add)
            {
                Detail = "trace";
            }

            protected override string Target =>
                string.IsNullOrWhiteSpace(Value?.Formula)
                ? $"f{Index.ToSubscript()}"
                : $"f{Index.ToSubscript()} = {Value.Formula}";

            protected override int GetItemsCount(Graph graph) => graph.Traces.Count;
            protected override Trace GetNewItem(Graph graph) => graph.NewTrace(Index);

            protected override void AddItem(Graph graph) => graph.AddTrace(Value);
            protected override void InsertItem(Graph graph) => graph.InsertTrace(Index, Value);
            protected override void RemoveItem(Graph graph) => graph.RemoveTrace(Index);
        }

        #endregion

        #region Composite Command

        private class CompositeCommand : Command<object>
        {
            protected CompositeCommand(params ICommand[] commands)
            {
                Detail = "multiple properties";
                Commands.Clear();
                Commands.AddRange(commands);
            }

            public override string RedoAction => Action;
            public override string UndoAction => Action;

            protected override string Target => null;

            public override void Invert()
            {
                Commands.ForEach(p => p.Invert());
                Commands.Reverse();
            }

            public override bool Run(Graph graph)
            {
                var result = false;
                foreach (var command in Commands)
                    result |= command.Run(graph);
                return result;
            }

            public override string ToString() =>
                Commands.Select(p => p.ToString()).Aggregate((s, t) => $"{s}; \n{t}");

            private const string Action = "multiple changes";
            private List<ICommand> Commands = new List<ICommand>();
        }

        #endregion
    }
}
