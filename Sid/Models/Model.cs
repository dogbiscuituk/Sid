namespace Sid.Models
{
    using System;

    public class Model
    {
        private Graph _graph = new Graph();
        public Graph Graph
        {
            get
            {
                return _graph;
            }
            set
            {
                _graph = value;
                OnGraphChanged();
            }
        }

        private bool _modified;
        public bool Modified
        {
            get => _modified;
            set
            {
                if (Modified != value)
                {
                    _modified = value;
                    OnModifiedChanged();
                }
            }
        }

        public void Clear()
        {
            Graph.Clear();
        }

        public event EventHandler GraphChanged;
        public event EventHandler ModifiedChanged;

        protected virtual void OnGraphChanged()
        {
            GraphChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnModifiedChanged()
        {
            ModifiedChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
