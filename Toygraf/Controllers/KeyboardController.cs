namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Controls;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class KeyboardController
    {
        #region Internal Interface

        internal KeyboardController(TracePropertiesController tracePropertiesController)
        {
            TracePropertiesController = tracePropertiesController;
            View = tracePropertiesController.TracePropertiesDialog;
            GraphController.PropertyChanged += GraphController_PropertyChanged;
            InitFunctionNames();
        }

        internal TracePropertiesDialog View
        {
            get => _view;
            set
            {
                _view = value;
                LoadKeys();
                View.cbVisible.CheckedChanged += VisibleChanged;
                FunctionBox.KeyUp += FunctionBox_KeyUp;
                FunctionBox.MouseUp += FunctionBox_MouseUp;
                FunctionBox.TextChanged += FunctionBox_TextChanged;
                FunctionBox.Validating += FunctionBox_Validating;
                Keyboard.btnInverse.Click += BtnInverse_Click;
                Keyboard.btnHyper.Click += BtnHyper_Click;
                Keyboard.btnBackspace.Click += Key_Press;
                Keyboard.btnLshift.Click += BtnShift_Click;
                Keyboard.btnRshift.Click += BtnShift_Click;
                Keyboard.btnShiftLock.Click += BtnShiftLock_Click;
                Keyboard.btnGreek.Click += BtnGreek_Click;
                Keyboard.btnMaths.Click += BtnMaths_Click;
                Keyboard.btnSubscript.Click += BtnSubscript_Click;
                Keyboard.btnSuperscript.Click += BtnSuperscript_Click;
                InitKeys();
            }
        }

        internal void IndexValueChanged() => FocusFunctionBox();

        internal void LoadTrace()
        {
            Updating = true;
            if (Index >= 0)
            {
                View.cbVisible.Checked = TraceView.cbVisible.Checked;
                FunctionBox.Text = ActiveControl.Text;
            }
            else
            {
                FunctionBox.Text = string.Empty;
            }
            Updating = false;
            UpdateProxyLabel();
        }

        #endregion

        #region Private Properties

        private TgKeyboard Keyboard => View.Keyboard;

        private static readonly Layout[] FkLayouts =
        {
            new Layout{Keys="sin|cos|tan|log|abs|erf|ceiling|round|hstep|csc|sec|cot|log₁₀|sqrt|erfc|floor|sign|", Name = "Normal"},
            new Layout{Keys="asin|acos|atan|exp|abs|erf|ceiling|round|hstep|acsc|asec|acot|alog|sqr|erfc|floor|sign|", Name = "Inverse"},
            new Layout{Keys="sinh|cosh|tanh|log|abs|erf|ceiling|round|hstep|csch|sech|coth|log₁₀|sqrt|erfc|floor|sign|", Name = "Hyperbolic"},
            new Layout{Keys="asinh|acosh|atanh|exp|abs|erf|ceiling|round|hstep|acsch|asech|acoth|alog|sqr|erfc|floor|sign|", Name = "Inverse Hyperbolic"}
        };

        /// <summary>
        /// Greek keyboard layout based on https://en.wikipedia.org/wiki/Keyboard_layout#/media/File:KB_Greek.svg
        /// </summary>
        private static readonly Layout[] KbLayouts =
        {
            new Layout{Keys = @"`1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.", Name = "Latin Lowercase"},
            new Layout{Keys = @"¬!""£$%^&*()_+ /*-QWERTYUIOP{}789+ASDFGHJKL:@~456|ZXCVBNM<>?123 0.", Name = "Latin Uppercase"},
            new Layout{Keys = @"`1234567890-= /*- ςερτυθιοπ[]789+ασδφγηξκλ;'#456\ζχψωβνμ,./123 0.", Name = "Greek Lowercase"},
            new Layout{Keys = @"¬!""£$%^&*()_+ /*-  ΕΡΤΥΘΙΟΠ{}789+ΑΣΔΦΓΗΞΚΛ:@~456|ΖΧΨΩΒΝΜ<>?123 0.", Name = "Greek Uppercase"},
            new Layout{Keys = @"½⅓⅔¼¾⅕⅖⅗⅘≮≯-≠°÷×-qwertyuiop≰≱789+asdfghjkl;'#456\zxcvbnm≤≥/123 0.", Name = "Mathematical Lowercase"},
            new Layout{Keys = @"⅙⅚⅐⅛⅜⅝⅞⅑⅒≮≯-≠∫√∛∜QWERTYUIOP≰≱789+ASDFGHJKL;'#456\ZXCVBNM≤≥/123 0.", Name = "Mathematical Uppercase"},
            new Layout{Keys = @"ᵦᵧᵩᵪᵨ    ₍₎₋₌ ⁄ ₋  ₑᵣₜ ᵤᵢₒₚ  ₇₈₉₊ₐₛ   ₕⱼₖₗ   ₄₅₆  ₓ ᵥ ₙₘ   ₁₂₃ ₀ ", Name = "Subscript"},
            new Layout{Keys = @"ᵝᵞᵠᵡᵅᵟᵋᶿᶥ⁽⁾⁻⁼ ⁄ ⁻ᶲʷᵉʳᵗʸᵘⁱᵒᵖ  ⁷⁸⁹⁺ᵃˢᵈᶠᵍʰʲᵏˡ   ⁴⁵⁶ ᶻˣᶜᵛᵇⁿᵐ   ¹²³ ⁰ ", Name = "Superscript Lowercase"},
            new Layout{Keys = @"         ⁽⁾⁻⁼   ⁻ ᵂᴱᴿᵀ ᵁᴵᴼᴾ  ⁷⁸⁹⁺ᴬ ᴰ ᴳᴴᴶᴷᴸ   ⁴⁵⁶    ⱽᴮᴺᴹ   ¹²³ ⁰ ", Name = "Superscript Uppercase"}
        };

        private KeyStates _state;
        private KeyStates State
        {
            get => _state;
            set
            {
                FocusFunctionBox();
                if (State != value)
                {
                    _state = value;
                    InitBackColour(Keyboard.btnInverse, KeyStates.Inverse);
                    InitBackColour(Keyboard.btnHyper, KeyStates.Hyperbolic);
                    InitBackColour(Keyboard.btnLshift, KeyStates.Shift);
                    InitBackColour(Keyboard.btnRshift, KeyStates.Shift);
                    InitBackColour(Keyboard.btnShiftLock, KeyStates.ShiftLock);
                    InitBackColour(Keyboard.btnGreek, KeyStates.Greek);
                    InitBackColour(Keyboard.btnMaths, KeyStates.Maths);
                    InitBackColour(Keyboard.btnSubscript, KeyStates.Subs);
                    InitBackColour(Keyboard.btnSuperscript, KeyStates.Super);
                    InitKeys();
                }
            }
        }

        private TracePropertiesDialog _view;
        private readonly TracePropertiesController TracePropertiesController;
        private GraphController GraphController => TracePropertiesController.GraphController;
        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private List<TraceController> TraceControllers => GraphController.LegendController.TraceControllers;
        private TraceView TraceView => TraceControllers[Index].View;
        private Control ActiveControl => TraceView.cbFunction;
        private readonly List<Button> CustomKeys = new List<Button>();
        private readonly List<Button> FunctionKeys = new List<Button>();
        private ComboBox FunctionBox { get => View.FunctionBox; }
        private ComboBox.ObjectCollection Functions { get => FunctionBox.Items; }
        private Graph Graph => TracePropertiesController.Graph;
        private int Index => GetIndex();
        private int SelStart, SelLength;
        private bool CanCancel, Updating;

        #endregion

        #region Private Event Handlers

        private void BtnGreek_Click(object sender, EventArgs e) => ToggleLanguage(KeyStates.Greek);
        private void BtnHyper_Click(object sender, EventArgs e) => ToggleLanguage(KeyStates.Hyperbolic);
        private void BtnInverse_Click(object sender, EventArgs e) => ToggleLanguage(KeyStates.Inverse);
        private void BtnMaths_Click(object sender, EventArgs e) => ToggleLanguage(KeyStates.Maths);
        private void BtnShift_Click(object sender, EventArgs e) => State = State & ~KeyStates.ShiftLock ^ KeyStates.Shift;
        private void BtnShiftLock_Click(object sender, EventArgs e) => State = State & ~KeyStates.Shift ^ KeyStates.ShiftLock;
        private void BtnSubscript_Click(object sender, EventArgs e) => ToggleLanguage(KeyStates.Subs);
        private void BtnSuperscript_Click(object sender, EventArgs e) => ToggleLanguage(KeyStates.Super);
        private void FunctionBox_KeyUp(object sender, KeyEventArgs e) => SaveSelection();
        private void FunctionBox_MouseUp(object sender, MouseEventArgs e) => SaveSelection();

        private void FunctionBox_TextChanged(object sender, EventArgs e)
        {
            View.ToolTip.SetToolTip(FunctionBox, FunctionBox.Text);
            var formula = FunctionBox.Text;
            if (!string.IsNullOrWhiteSpace(formula) && !Functions.Contains(formula))
                Functions[0] = formula;
            if (Index >= 0 && !Updating && Validate())
                CommandProcessor[Index].Formula = formula;
        }

        private void FunctionBox_Validating(object sender, CancelEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var ok = new Parser().TryParse(comboBox.Text, out _, out string error);
            View.ErrorProvider.SetError(comboBox, ok ? string.Empty : error);
            e.Cancel = CanCancel && !ok;
        }

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!Updating)
            {
                var match = Regex.Match(e.PropertyName, $@"Model.Graph.Traces\[{Index}\]\.(\w+)");
                if (match.Success)
                {
                    Updating = true;
                    switch (match.Groups[1].Value)
                    {
                        case "Formula":
                            SaveSelection();
                            FunctionBox.Text = Graph.Traces[Index].Formula;
                            LoadSelection();
                            UpdateProxyLabel();
                            break;
                    }
                    Updating = false;
                }
            }
        }

        private void IndexValueChanged(object sender, EventArgs e) => IndexValueChanged();

        private void FunctionKey_Press(object sender, EventArgs e)
        {
            var text = ((Control)sender).Text;
            FocusFunctionBox();
            FunctionBox.SelectedText = $"{text}()";
            //SelStart += text.Length;
            //SelLength = 0;
            SaveSelection();
        }

        private void Key_Press(object sender, EventArgs e)
        {
            var text = ((Control)sender).Text;
            FocusFunctionBox();
            switch (text)
            {
                case "Back":
                    if (FunctionBox.SelectionLength > 0)
                        FunctionBox.SelectedText = string.Empty;
                    else
                    {
                        var index = FunctionBox.SelectionStart - 1;
                        if (index >= 0)
                        {
                            FunctionBox.SelectionStart = index;
                            FunctionBox.SelectionLength = 1;
                            FunctionBox.SelectedText = string.Empty;
                            SaveSelection();
                        }
                    }
                    break;
                case string t when !string.IsNullOrEmpty(t):
                    FunctionBox.SelectedText = t;
                    SaveSelection();
                    State &= ~(KeyStates.Shift | KeyStates.Languages);
                    break;
            }
        }

        private void UpdateProxyLabel()
        {
            var proxies = Graph.GetProxies().ToArray();
            View.tbProxy.Text = Index >= 0 && Index < proxies.Length
                ? proxies[Index]?.AsString()
                : string.Empty;
        }

        private void VisibleChanged(object sender, EventArgs e) => TraceView.cbVisible.Checked = View.cbVisible.Checked;

        #endregion

        #region Private Methods

        private void FocusFunctionBox()
        {
            FunctionBox.Focus();
            LoadSelection();
        }

        private string GetFkKeys(FkLayoutType type) => FkLayouts[(int)type].Keys;

        private FkLayoutType GetFkLayoutType()
        {
            switch (State & KeyStates.Functions)
            {
                case KeyStates.Inverse:
                    return FkLayoutType.Inverse;
                case KeyStates.Hyperbolic:
                    return FkLayoutType.Hyperbolic;
                case KeyStates.Functions:
                    return FkLayoutType.InverseHyperbolic;
                default:
                    return FkLayoutType.Normal;
            }
        }

        private int GetIndex() => TracePropertiesController.Index;
        private string GetKbLayoutName(KbLayoutType type) => KbLayouts[(int)type].Name;

        private KbLayoutType GetKbLayoutType()
        {
            var shift = (State & KeyStates.Shifted) != 0;
            switch (State & KeyStates.Languages)
            {
                case KeyStates.Greek:
                    return shift ? KbLayoutType.GreekUpper : KbLayoutType.GreekLower;
                case KeyStates.Maths:
                    return shift ? KbLayoutType.MathsUpper : KbLayoutType.MathsLower;
                case KeyStates.Subs:
                    return KbLayoutType.Subscript;
                case KeyStates.Super:
                    return shift ? KbLayoutType.SuperUpper : KbLayoutType.SuperLower;
                default:
                    return shift ? KbLayoutType.LatinUpper : KbLayoutType.LatinLower;
            }
        }

        private string GetKbKeys(KbLayoutType type) => KbLayouts[(int)type].Keys;

        private void InitBackColour(Control control, KeyStates state) =>
            control.BackColor = Color.FromKnownColor(
                (State & state) == 0 ? KnownColor.ControlLight : KnownColor.Window);

        private void InitFunctionNames()
        {
            Functions.Clear();
            Functions.Add(string.Empty);
            Functions.AddRange(Utility.FunctionNames.Select(f => $"{f}(x)").ToArray());
        }

        private void InitKeys()
        {
            var type = GetKbLayoutType();
            View.Text = GetKbLayoutName(type);
            var map = GetKbKeys(type);
            for (var index = 0; index < CustomKeys.Count; index++)
            {
                var key = CustomKeys[index];
                var cap = map[index];
                key.Text = cap.ToString().AmpersandEscape();
                View.ToolTip.SetToolTip(key, $"{char.GetUnicodeCategory(cap)} '{cap}'");
            }
            var fkLayoutType = GetFkLayoutType();
            var fkMap = GetFkKeys(fkLayoutType).Split('|');
            for (var index = 0; index < FunctionKeys.Count; index++)
            {
                var key = FunctionKeys[index];
                var cap = fkMap[index];
                key.Text = cap.ToString().AmpersandEscape();
            }
        }

        private void LoadKeys()
        {
            var keys = GetKeys("FK");
            FunctionKeys.AddRange(keys);
            foreach (var key in keys)
                key.Click += FunctionKey_Press;
            keys = GetKeys("KB");
            CustomKeys.AddRange(keys);
            foreach (var key in keys)
                key.Click += Key_Press;
        }

        private IEnumerable<Button> GetKeys(string tag) =>
            Keyboard.Controls.OfType<Button>().Where(p => (string)p.Tag == tag).OrderBy(p => p.TabIndex);

        private void LoadSelection()
        {
            FunctionBox.SelectionStart = SelStart;
            FunctionBox.SelectionLength = SelLength;
        }

        private void SaveSelection()
        {
            SelStart = FunctionBox.SelectionStart;
            SelLength = FunctionBox.SelectionLength;
        }

        private void ToggleLanguage(KeyStates state) =>
            State = State & (~KeyStates.Languages | state) ^ state;

        private void UnloadKeys()
        {
            foreach (var key in FunctionKeys)
                key.Click -= FunctionKey_Press;
            FunctionKeys.Clear();
            foreach (var key in CustomKeys)
                key.Click -= Key_Press;
            CustomKeys.Clear();
        }

        private bool Validate()
        {
            Graph.ValidateProxies();
            CanCancel = true;
            var ok = View.ValidateChildren();
            CanCancel = false;
            return ok;
        }

        #endregion

        #region Private Types

        private struct Layout { internal string Name, Keys; }

        private enum FkLayoutType
        {
            Normal,
            Inverse,
            Hyperbolic,
            InverseHyperbolic
        }

        private enum KbLayoutType
        {
            LatinLower, LatinUpper,
            GreekLower, GreekUpper,
            MathsLower, MathsUpper,
            Subscript,
            SuperLower, SuperUpper
        }

        [Flags]
        private enum KeyStates
        {
            Normal = 0x00,
            Shift = 0x01,
            ShiftLock = 0x02,
            Shifted = Shift | ShiftLock,
            Greek = 0x04,
            Maths = 0x08,
            Subs = 0x10,
            Super = 0x20,
            Languages = Greek | Maths | Subs | Super,
            Inverse = 0x40,
            Hyperbolic = 0x80,
            Functions = Inverse | Hyperbolic
        }

        #endregion
    }
}
