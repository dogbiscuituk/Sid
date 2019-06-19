namespace ToyGraf.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ToyGraf.Models;
    using static ToyGraf.Commands.CommandProcessor;

    internal class TraceSelection
    {
        internal TraceSelection(CommandProcessor commandProcessor)
        {
            CommandProcessor = commandProcessor;
        }

        internal void Add(IEnumerable<Trace> traces)
        {
            var change = false;
            foreach (var trace in traces)
                if (!_Traces.Contains(trace))
                {
                    _Traces.Add(trace);
                    change = true;
                }
            if (change)
                OnChange();
        }

        internal void Clear()
        {
            if (_Traces.Any())
            {
                _Traces.Clear();
                OnChange();
            }
        }

        internal void Invert(IEnumerable<Trace> traces)
        {
            foreach (var trace in traces)
                if (_Traces.Contains(trace))
                    _Traces.Remove(trace);
                else
                    _Traces.Add(trace);
            OnChange();
        }

        internal void Remove(IEnumerable<Trace> traces)
        {
            var change = false;
            foreach (var trace in traces)
                if (_Traces.Contains(trace))
                {
                    _Traces.Remove(trace);
                    change = true;
                }
            if (change)
                OnChange();
        }

        internal void Set(IEnumerable<Trace> traces)
        {
            _Traces.Clear();
            Add(traces);
        }

        private readonly CommandProcessor CommandProcessor;
        private Graph Graph => CommandProcessor.Graph;
        private List<Trace> _Traces = new List<Trace>();

        internal bool IsEmpty => !_Traces.Any();
        internal TraceProxy[] Traces
        {
            get => _Traces.Select(trace => new TraceProxy(CommandProcessor, Graph.Traces.IndexOf(trace))).ToArray();
        }

        protected virtual void OnChange() => Changed?.Invoke(this, EventArgs.Empty);

        internal event EventHandler Changed;
    }
}
