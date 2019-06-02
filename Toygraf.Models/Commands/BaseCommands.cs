namespace ToyGraf.Models.Commands
{
    using System;

    public abstract class GraphCommand<T> : IGraphCommand
    {
        /// <summary>
        /// Invoke the Run method of the command, then immediately invert
        /// the command in readiness for its transfer between the Undo and
        /// Redo stacks. Since most commands are their own inverses (they
        /// just tell the Graph "Swap your property value with the one I'm
        /// carrying", the Invert() method is almost always empty. See the
        /// GraphInsertSeriesCommand and GraphDeleteSeriesCommand classes
        /// for a notable exception to this rule.
        /// </summary>
        /// <param name="graph"></param>
        public bool Do(Graph graph)
        {
            var result = Run(graph);
            Invert();
            return result;
        }

        public virtual string Action => $"{Detail} change";
        public virtual string UndoAction => Action;
        public virtual string RedoAction => Action;
        public virtual T Value { get; set; }

        public override string ToString() => $"{Target} {Detail} = {Value}";

        protected abstract string Detail { get; }
        protected virtual string Target { get => "Graph"; }

        protected virtual void Invert() { }
        protected abstract bool Run(Graph graph);
    }

    public abstract class GraphPropertyCommand<T> : GraphCommand<T>, IGraphPropertyCommand
    {
        public GraphPropertyCommand(T value, Func<Graph, T> get, Action<Graph, T> set)
            : base()
        {
            Value = value;
            Get = get;
            Set = set;
        }

        protected Func<Graph, T> Get;
        protected Action<Graph, T> Set;

        protected override bool Run(Graph graph)
        {
            T value = Get(graph);
            var result = !Equals(value, Value);
            if (result)
            {
                Set(graph, Value);
                Value = value;
            }
            return result;
        }
    }

    public abstract class SeriesCommand<T> : GraphCommand<T>, ISeriesCommand
    {
        public SeriesCommand(int index) : base() { Index = index; }

        public int Index { get; set; }
    }

    public abstract class SeriesPropertyCommand<T> : SeriesCommand<T>, ISeriesPropertyCommand
    {
        public SeriesPropertyCommand(int index, T value, Func<Series, T> get, Action<Series, T> set)
            : base(index)
        {
            Value = value;
            Get = get;
            Set = set;
        }

        protected Func<Series, T> Get;
        protected Action<Series, T> Set;

        protected override bool Run(Graph graph)
        {
            T value = Get(graph.Series[Index]);
            var result = !value.Equals(Value);
            if (result)
            {
                Set(graph.Series[Index], Value);
                Value = value;
            }
            return result;
        }
        protected override string Target => $"f{Index}";
    }
}
