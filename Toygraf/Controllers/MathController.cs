namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Views;

    public class MathController
    {
        public MathController(AppController parent)
        {
            Parent = parent;
            View = new Mathboard();
            InitFunctionNames();
        }

        #region Properties

        private Mathboard _view;
        public Mathboard View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    UnloadKeys();
                    View.FormClosing -= View_FormClosing;
                    View.FunctionBox.TextChanged -= TextBox_TextChanged;
                    View.btnLshift.Click -= BtnShift_Click;
                    View.btnRshift.Click -= BtnShift_Click;
                    View.btnShiftLock.Click -= BtnShiftLock_Click;
                    View.btnGreek.Click -= BtnGreek_Click;
                    View.btnMaths.Click -= BtnMaths_Click;
                    View.btnSubscript.Click -= BtnSubscript_Click;
                    View.btnSuperscript.Click -= BtnSuperscript_Click;
                }
                _view = value;
                if (View != null)
                {
                    LoadKeys();
                    View.FormClosing += View_FormClosing;
                    View.FunctionBox.TextChanged += TextBox_TextChanged;
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
        }

        private AppController Parent;
        private Control ActiveControl { get; set; }
        private readonly List<Button> CustomKeys = new List<Button>();
        private ComboBox FunctionBox { get => View.FunctionBox; }
        private ComboBox.ObjectCollection Functions { get => FunctionBox.Items; }

        private KeyStates _state;
        private KeyStates State
        {
            get => _state;
            set
            {
                FocusTextBox();
                if (State != value)
                {
                    _state = value;
                    InitBackColour(View.btnLshift, KeyStates.Shift);
                    InitBackColour(View.btnRshift, KeyStates.Shift);
                    InitBackColour(View.btnShiftLock, KeyStates.ShiftLock);
                    InitBackColour(View.btnGreek, KeyStates.Greek);
                    InitBackColour(View.btnMaths, KeyStates.Mathematical);
                    InitBackColour(View.btnSubscript, KeyStates.Subscript);
                    InitBackColour(View.btnSuperscript, KeyStates.Superscript);
                    InitKeys();
                }
            }
        }

        #endregion

        #region Show/Hide

        public void ShowDialog(IWin32Window owner, Control sender, Point location)
        {
            ActiveControl = sender;
            View.FunctionBox.Text = ActiveControl.Text;
            View.Location = location;
            View.ShowDialog(owner);
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                View.Hide();
            }
        }

        #endregion

        #region Keyboards

        private KeyboardType GetKeyboardType()
        {
            var shift = (State & KeyStates.Shifted) != 0;
            switch (State & KeyStates.Languages)
            {
                case KeyStates.Greek:
                    return shift ? KeyboardType.GreekUpper : KeyboardType.GreekLower;
                case KeyStates.Mathematical:
                    return KeyboardType.Mathematical;
                case KeyStates.Subscript:
                    return KeyboardType.Subscript;
                case KeyStates.Superscript:
                    return shift ? KeyboardType.SuperUpper : KeyboardType.SuperLower;
                default:
                    return shift ? KeyboardType.LatinUpper : KeyboardType.LatinLower;
            }
        }

        private string GetKeys(KeyboardType type) => Keyboards[(int)type].Keys;

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

        /// <summary>
        /// Greek keyboard based on https://en.wikipedia.org/wiki/Keyboard_layout#/media/File:KB_Greek.svg
        /// </summary>
        private readonly Keyboard[] Keyboards =
        {
            new Keyboard{Keys = @"`1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.", Name = "Latin Lowercase"},
            new Keyboard{Keys = @"¬!""£$%^&*()_+ /*-QWERTYUIOP{}789+ASDFGHJKL:@~456|ZXCVBNM<>?123 0.", Name = "Latin Uppercase"},
            new Keyboard{Keys = @"`1234567890-= /*- ςερτυθιοπ[]789+ασδφγηξκλ;'#456\ζχψωβνμ,./123 0.", Name = "Greek Lowercase"},
            new Keyboard{Keys = @"¬!""£$%^&*()_+ /*-  ΕΡΤΥΘΙΟΠ{}789+ΑΣΔΦΓΗΞΚΛ:@~456|ΖΧΨΩΒΝΜ<>?123 0.", Name = "Greek Uppercase"},
            new Keyboard{Keys = @"` √∛∜    ≮≯-≠°÷×-qwertyuiop≰≱789+asdfghjkl;'#456\zxcvbnm≤≥/123 0.", Name = "Mathematical"},
            new Keyboard{Keys = @"ᵦᵧᵩᵪᵨ    ₍₎₋₌   ₋  ₑᵣₜ ᵤᵢₒₚ  ₇₈₉₊ₐₛ   ₕⱼₖₗ   ₄₅₆  ₓ ᵥ ₙₘ   ₁₂₃ ₀ ", Name = "Subscript"},
            new Keyboard{Keys = @"ᵝᵞᵠᵡᵅᵟᵋᶿᶥ⁽⁾⁻⁼   ⁻ᶲʷᵉʳᵗʸᵘⁱᵒᵖ  ⁷⁸⁹⁺ᵃˢᵈᶠᵍʰʲᵏˡ   ⁴⁵⁶ ᶻˣᶜᵛᵇⁿᵐ   ¹²³ ⁰ ", Name = "Superscript Lowercase"},
            new Keyboard{Keys = @"         ⁽⁾⁻⁼   ⁻ ᵂᴱᴿᵀ ᵁᴵᴼᴾ  ⁷⁸⁹⁺ᴬ ᴰ ᴳᴴᴶᴷᴸ   ⁴⁵⁶    ⱽᴮᴺᴹ   ¹²³ ⁰ ", Name = "Superscript Uppercase"}
        };

        private string GetKeyboardName(KeyboardType type) => Keyboards[(int)type].Name;

        #endregion

        #region States

        private void BtnShift_Click(object sender, System.EventArgs e) =>
            State = State & ~KeyStates.ShiftLock ^ KeyStates.Shift;

        private void BtnShiftLock_Click(object sender, System.EventArgs e) =>
            State = State & ~KeyStates.Shift ^ KeyStates.ShiftLock;

        private void BtnGreek_Click(object sender, System.EventArgs e) =>
            ToggleLanguage(KeyStates.Greek);

        private void BtnMaths_Click(object sender, EventArgs e) =>
            ToggleLanguage(KeyStates.Mathematical);

        private void BtnSubscript_Click(object sender, EventArgs e) =>
            ToggleLanguage(KeyStates.Subscript);

        private void BtnSuperscript_Click(object sender, EventArgs e) =>
            ToggleLanguage(KeyStates.Superscript);

        private void InitBackColour(Control control, KeyStates state) =>
            control.BackColor = Color.FromKnownColor(
                (State & state) == 0 ? KnownColor.ControlLight : KnownColor.Window);

        private void ToggleLanguage(KeyStates state) =>
            State = State & (~KeyStates.Languages | state) ^ state;

        #endregion

        #region Keystrokes

        private void FocusTextBox() => View.FunctionBox.Focus();

        private void Key_Press(object sender, System.EventArgs e)
        {
            var text = ((Control)sender).Text;
            if (text != string.Empty)
                View.FunctionBox.SelectedText = text;
            State &= ~(KeyStates.Shift | KeyStates.Languages);
            FocusTextBox();
        }

        private void LoadKeys()
        {
            var keys = View.Controls.OfType<Button>().Where(p => p.Tag == null);
            CustomKeys.AddRange(keys);
            foreach (var key in keys)
                key.Click += Key_Press;
        }

        private void UnloadKeys()
        {
            foreach (var key in CustomKeys)
                key.Click -= Key_Press;
            CustomKeys.Clear();
        }

        #endregion

        #region Functions

        private void InitFunctionNames()
        {
            Functions.Clear();
            Functions.Add(string.Empty);
            Functions.AddRange(Utility.FunctionNames.Select(f => $"{f}(x)").ToArray());
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var function = FunctionBox.Text;
            if (!string.IsNullOrWhiteSpace(function) && !Functions.Contains(function))
                Functions[0] = function;
            ActiveControl.Text = function;
        }

        #endregion

        #region Private Types

        private struct Keyboard { public string Name, Keys; }

        public enum KeyboardType
        {
            LatinLower,
            LatinUpper,
            GreekLower,
            GreekUpper,
            Mathematical,
            Subscript,
            SuperLower,
            SuperUpper
        }

        [Flags]
        private enum KeyStates
        {
            Normal = 0x00,
            Shift = 0x01,
            ShiftLock = 0x02,
            Shifted = Shift | ShiftLock,
            Greek = 0x04,
            Mathematical = 0x08,
            Subscript = 0x10,
            Superscript = 0x20,
            Languages = Greek | Mathematical | Subscript | Superscript
        }

        #endregion
    }
}
