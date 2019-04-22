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

        private bool _isotropic;
        public bool Isotropic
        {
            get => _isotropic;
            set
            {
                if (Isotropic != value)
                {
                    _isotropic = value;
                    OnIsotropicChanged();
                }
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

        public void Clear() => Graph.Clear();

        public event EventHandler IsotropicChanged;
        public event EventHandler ModifiedChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public void Graph_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Graph.{e.PropertyName}");

        protected virtual void OnIsotropicChanged() =>
            IsotropicChanged?.Invoke(this, EventArgs.Empty);

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
