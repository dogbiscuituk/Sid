namespace ToyGraf.Models
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Interfaces;

    public class Style : IStyle
    {
        public Style() => RestoreDefaults();

        public Style(Graph graph) : this()
        {
            Updating = true;
            this.CopyFrom(graph);
            Updating = false;
        }

        public Style Clone()
        {
            var style = new Style();
            style.CopyFrom(this);
            return style;
        }

        protected BrushType _brushType;
        [DefaultValue(typeof(BrushType), "Solid")]
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
        [DefaultValue(typeof(Color), "Transparent")]
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
        [DefaultValue(typeof(Color), "Transparent")]
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

        protected FillMode _fillMode;
        [DefaultValue(typeof(FillMode), "Alternate")]
        public FillMode FillMode
        {
            get => _fillMode;
            set
            {
                if (FillMode != value)
                {
                    _fillMode = value;
                    OnPropertyChanged("FillMode");
                }
            }
        }

        protected int _fillTransparencyPercent;
        [DefaultValue(0)]
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
        [DefaultValue(typeof(LinearGradientMode), "Horizontal")]
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
        [DefaultValue(typeof(HatchStyle), "Horizontal")]
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
        [DefaultValue(typeof(Color), "DarkGray")]
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
        [DefaultValue(typeof(Color), "Black")]
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
        [DefaultValue(typeof(DashStyle), "Solid")]
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
        [DefaultValue(1f)]
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

        protected int _stepCount;
        [DefaultValue(1000)]
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
        [DefaultValue(null)]
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
        [DefaultValue(null)]
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
        [DefaultValue("")]
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
        [DefaultValue(typeof(WrapMode), "Tile")]
        public WrapMode WrapMode
        {
            get => _wrapMode;
            set
            {
                _wrapMode = value;
                OnPropertyChanged("WrapMode");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (!Updating)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RestoreDefaults()
        {
            _brushType = Defaults.StyleBrushType;
            _fillColour1 = Defaults.StyleFillColour1;
            _fillColour2 = Defaults.StyleFillColour2;
            _fillMode = Defaults.StyleFillMode;
            _fillTransparencyPercent = Defaults.StyleFillTransparencyPercent;
            _gradientMode = Defaults.StyleGradientMode;
            _hatchStyle = Defaults.StyleHatchStyle;
            _limitColour = Defaults.StyleLimitColour;
            _penColour = Defaults.StylePenColour;
            _penStyle = Defaults.StylePenStyle;
            _penWidth = Defaults.StylePenWidth;
            _stepCount = Defaults.StyleStepCount;
            _texture = Defaults.StyleTexture;
            _texturePath = Defaults.StyleTexturePath;
            _title = Defaults.StyleTitle;
            _wrapMode = Defaults.StyleWrapMode;
        }

        private bool Updating;
    }
}
