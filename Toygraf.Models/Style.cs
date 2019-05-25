namespace ToyGraf.Models
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class Style
    {
        protected Color _axisColour;
        public Color AxisColour
        {
            get => _axisColour;
            set
            {
                if (AxisColour != value)
                {
                    _axisColour = value;
                    OnPropertyChanged("AxisColour");
                }
            }
        }

        protected Color _fillColour;
        public Color FillColour
        {
            get => _fillColour;
            set
            {
                if (FillColour != value)
                {
                    _fillColour = value;
                    OnPropertyChanged("FillColour");
                }
            }
        }

        protected int _fillTransparencyPercent;
        public int FillTransparencyPercent
        {
            get => _fillTransparencyPercent;
            set
            {
                if (FillTransparencyPercent != value)
                {
                    _fillTransparencyPercent = value;
                    OnPropertyChanged("FillTransparencyPercent");
                }
            }
        }

        protected Color _limitColour;
        public Color LimitColour
        {
            get => _limitColour;
            set
            {
                if (LimitColour != value)
                {
                    _limitColour = value;
                    OnPropertyChanged("LimitColour");
                }
            }
        }

        protected Color _penColour;
        public Color PenColour
        {
            get => _penColour;
            set
            {
                if (PenColour != value)
                {
                    _penColour = value;
                    OnPropertyChanged("PenColour");
                }
            }
        }

        protected DashStyle _penStyle;
        public DashStyle PenStyle
        {
            get => _penStyle;
            set
            {
                if (PenStyle != value)
                {
                    _penStyle = value;
                    OnPropertyChanged("PenStyle");
                }
            }
        }

        protected float _penWidth;
        public float PenWidth
        {
            get => _penWidth;
            set
            {
                if (PenWidth != value)
                {
                    _penWidth = value;
                    OnPropertyChanged("PenWidth");
                }
            }
        }

        protected Color _reticleColour;
        public Color ReticleColour
        {
            get => _reticleColour;
            set
            {
                if (ReticleColour != value)
                {
                    _reticleColour = value;
                    OnPropertyChanged("ReticleColour");
                }
            }
        }

        protected int _stepCount;
        public int StepCount
        {
            get => _stepCount;
            set
            {
                if (StepCount != value)
                {
                    _stepCount = value;
                    OnPropertyChanged("StepCount");
                }
            }
        }

        protected abstract void StepCountChanged();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
