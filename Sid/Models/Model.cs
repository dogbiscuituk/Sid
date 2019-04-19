namespace Sid.Models
{
    using System;
    using System.ComponentModel;

    public class Model : INotifyPropertyChanged
    {
        private Graph _graph = new Graph();
        public Graph Graph
        {
            get => _graph;
            set
            {
                _graph = value;
                Graph.PropertyChanged += Graph_PropertyChanged;
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
                    OnModifiedChanged();
                }
            }
        }

        public void Clear() => Graph.Clear();

        public event EventHandler IsotropicChanged;
        public event EventHandler ModifiedChanged;
        public event PropertyChangedEventHandler PropertyChanged;


        private void Graph_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
            Modified = true;
        }

        protected virtual void OnIsotropicChanged() =>
            IsotropicChanged?.Invoke(this, EventArgs.Empty);

        protected virtual void OnModifiedChanged() =>
            ModifiedChanged?.Invoke(this, EventArgs.Empty);

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
