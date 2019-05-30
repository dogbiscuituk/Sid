namespace ToyGraf.Models
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using ToyGraf.Models.Enumerations;

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

        protected BrushType _brushType;
        public BrushType BrushType
        {
            get => _brushType;
            set
            {
                if (BrushType != value)
                {
                    _brushType = value;
                    OnPropertyChanged("BrushType");
                }
            }
        }

        protected Color _fillColour1;
        public Color FillColour1
        {
            get => _fillColour1;
            set
            {
                if (FillColour1 != value)
                {
                    _fillColour1 = value;
                    OnPropertyChanged("FillColour1");
                }
            }
        }

        protected Color _fillColour2;
        public Color FillColour2
        {
            get => _fillColour2;
            set
            {
                if (FillColour2 != value)
                {
                    _fillColour2 = value;
                    OnPropertyChanged("FillColour2");
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

        protected LinearGradientMode _gradientMode;
        public LinearGradientMode GradientMode
        {
            get => _gradientMode;
            set
            {
                if (GradientMode != value)
                {
                    _gradientMode = value;
                    OnPropertyChanged("GradientMode");
                }
            }
        }

        protected HatchStyle _hatchStyle;
        public HatchStyle HatchStyle
        {
            get => _hatchStyle;
            set
            {
                if (HatchStyle != value )
                {
                    _hatchStyle = value;
                    OnPropertyChanged("HatchStyle");
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

        protected string _texture;
        public string Texture
        {
            get => _texture;
            set
            {
                _texture = value;
                OnPropertyChanged("Texture");
            }
        }

        protected string _texturePath;
        public string TexturePath
        {
            get => _texturePath;
            set
            {
                _texturePath = value;
                OnPropertyChanged("TexturePath");
            }
        }

        protected string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (Title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        protected WrapMode _wrapMode;
        public WrapMode WrapMode
        {
            get => _wrapMode;
            set
            {
                _wrapMode = value;
                OnPropertyChanged("WrapMode");
            }
        }

        protected abstract void StepCountChanged();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
