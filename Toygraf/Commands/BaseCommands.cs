namespace ToyGraf.Commands
{
    using System;
    using ToyGraf.Models;

    partial class GraphProxy
    {
        private abstract class GraphCommand<T> : IGraphCommand
        {
            public virtual string RedoAction => Action;
            public virtual string UndoAction => Action;

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
            public void Do(Graph graph)
            {
                Run(graph);
                Invert();
            }

            public virtual void Invert() { }
            public abstract void Run(Graph graph);
            public override string ToString() => $"{Target} {Detail} = {Value}";

            protected virtual string Action => $"{Detail} change";
            protected abstract string Detail { get; }
            protected virtual string Target { get => "Graph"; }
            protected T Value { get; set; }
        }

        private abstract class GraphPropertyCommand<T> : GraphCommand<T>
        {
            protected GraphPropertyCommand(T value, Func<Graph, T> get, Action<Graph, T> set) : base()
            {
                Value = value;
                Get = get;
                Set = set;
            }

            public override void Run(Graph graph)
            {
                T value = Get(graph);
                if (!Equals(value, Value))
                {
                    Set(graph, Value);
                    Value = value;
                }
            }

            protected Func<Graph, T> Get;
            protected Action<Graph, T> Set;
        }

        private abstract class TraceCommand<T> : GraphCommand<T>, ITraceCommand
        {
            protected TraceCommand(int index) : base() { Index = index; }

            public int Index { get; set; }
        }

        private abstract class TracePropertyCommand<T> : TraceCommand<T>, ITracePropertyCommand
        {
            protected TracePropertyCommand(int index, T value, Func<Trace, T> get, Action<Trace, T> set)
                : base(index)
            {
                Value = value;
                Get = get;
                Set = set;
            }

            public override void Run(Graph graph)
            {
                T value = Get(graph.Traces[Index]);
                if (!Equals(value, Value))
                {
                    Set(graph.Traces[Index], Value);
                    Value = value;
                }
            }

            protected Func<Trace, T> Get;
            protected Action<Trace, T> Set;
            protected override string Target => $"f{Index}";
        }
    }
}