namespace ToyGraf.Models
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

        #region Properties

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
                    OnModifiedChanged();
                }
            }
        }

        protected void OnModifiedChanged() =>
            ModifiedChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler ModifiedChanged;

        #endregion

        #region Series management

        public event EventHandler Cleared;

        public void Clear()
        {
            Graph.Clear();
            OnClear();
        }

        protected void OnClear() =>
            Cleared?.Invoke(this, EventArgs.Empty);

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            Modified = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Graph_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Graph.{e.PropertyName}");

        #endregion
    }
}
