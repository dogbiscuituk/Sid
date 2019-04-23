namespace Sid.Models
{
    using System;
    using System.ComponentModel;

    public class Model : INotifyPropertyChanged
    {
        public Model()
        {
            Graph = new Graph();
            Modified = false;
        }

        private Graph _graph;
        public Graph Graph
        {
            get => _graph;
            set
            {
                if (Graph != null) Graph.PropertyChanged -= Graph_PropertyChanged;
                _graph = value;
                if (Graph != null) Graph.PropertyChanged += Graph_PropertyChanged;
                OnPropertyChanged("Graph");
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
                    System.Diagnostics.Debug.WriteLine($"Modified = {Modified}");
                    OnModifiedChanged();
                }
            }
        }

        public void Clear()
        {
            Graph.Clear();
            OnClear();
        }

        public event EventHandler Cleared, ModifiedChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public void Graph_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Graph.{e.PropertyName}");

        protected virtual void OnClear() =>
            Cleared?.Invoke(this, EventArgs.Empty);

        protected virtual void OnModifiedChanged() =>
            ModifiedChanged?.Invoke(this, EventArgs.Empty);

        protected virtual void OnPropertyChanged(string propertyName)
        {
            System.Diagnostics.Debug.WriteLine($"Model.OnPropertyChanged(\"{propertyName}\")");
            Modified = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
