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
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class KeyboardController
    {
        #region Internal Interface

        internal KeyboardController(SeriesPropertiesController seriesPropertiesController)
        {
            SeriesPropertiesController = seriesPropertiesController;
            View = seriesPropertiesController.View;
            GraphController.PropertyChanged += GraphController_PropertyChanged;
            InitFunctionNames();
        }

        internal SeriesPropertiesDialog View
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
                View.btnBackspace.Click += Key_Press;
                View.btnLshift.Click += BtnShift_Click;
                View.btnRshift.Click += BtnShift_Click;
                View.btnShiftLock.Click += BtnShiftLock_Click;
                View.btnGreek.Click += BtnGreek_Click;
                View.btnMaths.Click += BtnMaths_Click;
                View.btnSubscript.Click += BtnSubscript_Click;
                View.btnSuperscript.Click += BtnSuperscript_Click;
                InitKeys();
            }
        }

        internal void IndexValueChanged() => FocusFunctionBox();

        internal void LoadSeries()
        {
            Loading = true;
            if (Index >= 0)
            {
                View.cbVisible.Checked = SeriesView.cbVisible.Checked;
                FunctionBox.Text = ActiveControl.Text;
            }
            else
            {
                FunctionBox.Text = string.Empty;
            }
            Loading = false;
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Greek keyboard based on https://en.wikipedia.org/wiki/Keyboard_layout#/media/File:KB_Greek.svg
        /// </summary>
        private static readonly Keyboard[] Keyboards =
        {
            new Keyboard{Keys = @"`1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.", Name = "Latin Lowercase"},
            new Keyboard{Keys = @"¬!""£$%^&*()_+ /*-QWERTYUIOP{}789+ASDFGHJKL:@~456|ZXCVBNM<>?123 0.", Name = "Latin Uppercase"},
            new Keyboard{Keys = @"`1234567890-= /*- ςερτυθιοπ[]789+ασδφγηξκλ;'#456\ζχψωβνμ,./123 0.", Name = "Greek Lowercase"},
            new Keyboard{Keys = @"¬!""£$%^&*()_+ /*-  ΕΡΤΥΘΙΟΠ{}789+ΑΣΔΦΓΗΞΚΛ:@~456|ΖΧΨΩΒΝΜ<>?123 0.", Name = "Greek Uppercase"},
            new Keyboard{Keys = @"½⅓⅔¼¾⅕⅖⅗⅘≮≯-≠°÷×-qwertyuiop≰≱789+asdfghjkl;'#456\zxcvbnm≤≥/123 0.", Name = "Mathematical Lowercase"},
            new Keyboard{Keys = @"⅙⅚⅐⅛⅜⅝⅞⅑⅒≮≯-≠°√∛∜QWERTYUIOP≰≱789+ASDFGHJKL;'#456\ZXCVBNM≤≥/123 0.", Name = "Mathematical Uppercase"},
            new Keyboard{Keys = @"ᵦᵧᵩᵪᵨ    ₍₎₋₌ ⁄ ₋  ₑᵣₜ ᵤᵢₒₚ  ₇₈₉₊ₐₛ   ₕⱼₖₗ   ₄₅₆  ₓ ᵥ ₙₘ   ₁₂₃ ₀ ", Name = "Subscript"},
            new Keyboard{Keys = @"ᵝᵞᵠᵡᵅᵟᵋᶿᶥ⁽⁾⁻⁼ ⁄ ⁻ᶲʷᵉʳᵗʸᵘⁱᵒᵖ  ⁷⁸⁹⁺ᵃˢᵈᶠᵍʰʲᵏˡ   ⁴⁵⁶ ᶻˣᶜᵛᵇⁿᵐ   ¹²³ ⁰ ", Name = "Superscript Lowercase"},
            new Keyboard{Keys = @"         ⁽⁾⁻⁼   ⁻ ᵂᴱᴿᵀ ᵁᴵᴼᴾ  ⁷⁸⁹⁺ᴬ ᴰ ᴳᴴᴶᴷᴸ   ⁴⁵⁶    ⱽᴮᴺᴹ   ¹²³ ⁰ ", Name = "Superscript Uppercase"}
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
                    InitBackColour(View.btnLshift, KeyStates.Shift);
                    InitBackColour(View.btnRshift, KeyStates.Shift);
                    InitBackColour(View.btnShiftLock, KeyStates.ShiftLock);
                    InitBackColour(View.btnGreek, KeyStates.Greek);
                    InitBackColour(View.btnMaths, KeyStates.Maths);
                    InitBackColour(View.btnSubscript, KeyStates.Subs);
                    InitBackColour(View.btnSuperscript, KeyStates.Super);
                    InitKeys();
                }
            }
        }

        private SeriesPropertiesDialog _view;
        private readonly SeriesPropertiesController SeriesPropertiesController;
        private GraphController GraphController => SeriesPropertiesController.GraphController;
        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private List<SeriesController> SeriesControllers => GraphController.LegendController.Children;
        private SeriesView SeriesView => SeriesControllers[Index].View;
        private Control ActiveControl => SeriesView.cbFunction;
        private readonly List<Button> CustomKeys = new List<Button>();
        private ComboBox FunctionBox { get => View.FunctionBox; }
        private ComboBox.ObjectCollection Functions { get => FunctionBox.Items; }
        private Graph Graph => SeriesPropertiesController.Graph;
        private int Index => GetIndex();
        private int SelStart, SelLength;
        private bool CanCancel, Loading, Updating;

        #endregion

        #region Private Event Handlers

        private void BtnGreek_Click(object sender, EventArgs e) => ToggleLanguage(KeyStates.Greek);
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
            if (Index >= 0 && !Loading && !Updating && Validate())
                CommandProcessor.SetSeriesFormula(Index, formula);
        }

        private void FunctionBox_Validating(object sender, CancelEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var ok = new Parser().TryParse(comboBox.Text, out object result);
            View.ErrorProvider.SetError(comboBox, ok ? string.Empty : result.ToString());
            e.Cancel = CanCancel && !ok;
        }

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!Updating)
            {
                var match = Regex.Match(e.PropertyName, $@"Model.Graph.Series\[{Index}\]\.(\w+)");
                if (match.Success)
                {
                    Updating = true;
                    switch (match.Groups[1].Value)
                    {
                        case "Formula":
                            SaveSelection();
                            View.FunctionBox.Text = Graph.Series[Index].Formula;
                            LoadSelection();
                            UpdateProxyLabel();
                            break;
                    }
                Updating = false;
                }
            }
        }

        private void IndexValueChanged(object sender, EventArgs e) => IndexValueChanged();

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

        private void VisibleChanged(object sender, EventArgs e) => SeriesView.cbVisible.Checked = View.cbVisible.Checked;

        #endregion

        #region Private Methods

        private void FocusFunctionBox()
        {
            FunctionBox.Focus();
            LoadSelection();
        }

        private int GetIndex() => SeriesPropertiesController.Index;

        private string GetKeyboardName(KeyboardType type) => Keyboards[(int)type].Name;

        private KeyboardType GetKeyboardType()
        {
            var shift = (State & KeyStates.Shifted) != 0;
            switch (State & KeyStates.Languages)
            {
                case KeyStates.Greek:
                    return shift ? KeyboardType.GreekUpper : KeyboardType.GreekLower;
                case KeyStates.Maths:
                    return shift ? KeyboardType.MathsUpper : KeyboardType.MathsLower;
                case KeyStates.Subs:
                    return KeyboardType.Subscript;
                case KeyStates.Super:
                    return shift ? KeyboardType.SuperUpper : KeyboardType.SuperLower;
                default:
                    return shift ? KeyboardType.LatinUpper : KeyboardType.LatinLower;
            }
        }

        private string GetKeys(KeyboardType type) => Keyboards[(int)type].Keys;

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
            var type = GetKeyboardType();
            View.Text = GetKeyboardName(type);
            var map = GetKeys(type);
            for (var index = 0; index < CustomKeys.Count; index++)
            {
                var key = CustomKeys[index];
                var c = map[index];
                key.Text = c.ToString().AmpersandEscape();
                View.ToolTip.SetToolTip(key, $"{char.GetUnicodeCategory(c)} '{c}'");
            }
        }

        private void LoadKeys()
        {
            var keys = View.tpKeyboard.Controls.OfType<Button>().Where(p => (string)p.Tag == "KB");
            CustomKeys.AddRange(keys);
            foreach (var key in keys)
                key.Click += Key_Press;
        }

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

        private struct Keyboard { internal string Name, Keys; }

        private enum KeyboardType
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
            Languages = Greek | Maths | Subs | Super
        }

        #endregion
    }
}
