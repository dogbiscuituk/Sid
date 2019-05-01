namespace Sid.Models
{
    using Sid.Expressions;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class Model : INotifyPropertyChanged
    {
        public Model()
        {
            Graph = new Graph();
            Modified = false;
            Clock = new Clock();
            Clock.Tick += Clock_Tick;
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

        protected virtual void OnModifiedChanged() =>
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

        protected virtual void OnClear() =>
            Cleared?.Invoke(this, EventArgs.Empty);

        #endregion

        #region Clock

        private Clock Clock;

        public void Draw(Graphics g, Rectangle r) => Graph.Draw(g, r, Clock.SecondsElapsed);

        private void Clock_Tick(object sender, EventArgs e) => ClockTick?.Invoke(this, EventArgs.Empty);

        public event EventHandler ClockTick;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void Graph_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Graph.{e.PropertyName}");

        protected virtual void OnPropertyChanged(string propertyName)
        {
            Modified = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
