namespace Sid.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Sid.Expressions;
    using Sid.Views;

    public class MathController
    {
        public MathController(AppController parent)
        {
            Parent = parent;
            View = new Mathboard();
        }

        #region Properties

        private Mathboard _view;
        private Mathboard View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    UnloadKeys();
                    View.FormClosing -= View_FormClosing;
                    View.TextBox.TextChanged -= TextBox_TextChanged;
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
                    View.TextBox.TextChanged += TextBox_TextChanged;
                    View.btnLshift.Click += BtnShift_Click;
                    View.btnRshift.Click += BtnShift_Click;
                    View.btnShiftLock.Click += BtnShiftLock_Click;
                    View.btnGreek.Click += BtnGreek_Click;
                    View.btnMaths.Click += BtnMaths_Click;
                    View.btnSubscript.Click += BtnSubscript_Click;
                    View.btnSuperscript.Click += BtnSuperscript_Click;
                    InitKeyboardMode();
                }
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e) =>
            ActiveComboBox.Text = View.TextBox.Text;

        private AppController Parent;
        private ComboBox ActiveComboBox { get; set; }

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
                    InitKeyboardMode();
                }
            }
        }

        private readonly List<Button> CustomKeys = new List<Button>();

        #endregion

        #region Show/Hide

        public void ShowDialog(IWin32Window owner, KeyView sender)
        {
            ActiveComboBox = sender.cbFunction;
            View.ShowDialog(owner);
        }

        public void Hide()
        {
            View.Close();
        }

        #endregion

        #region Modes

        private void InitBackColour(Control control, KeyStates state) =>
            control.BackColor = Color.FromKnownColor(
                (State & state) == 0 ? KnownColor.ControlLight : KnownColor.Window);

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

        private void ToggleLanguage(KeyStates state)
        {
            State = State & (~KeyStates.Languages | state) ^ state;
        }

        private KeyboardMode GetKeyboardMode()
        {
            var shift = (State & KeyStates.Shifted) != 0;
            switch (State & KeyStates.Languages)
            {
                case KeyStates.Greek:
                    return shift ? KeyboardMode.GreekUpper : KeyboardMode.GreekLower;
                case KeyStates.Mathematical:
                    return KeyboardMode.Mathematical;
                case KeyStates.Subscript:
                    return KeyboardMode.Subscript;
                case KeyStates.Superscript:
                    return shift ? KeyboardMode.SuperUpper : KeyboardMode.SuperLower;
                default:
                    return shift ? KeyboardMode.LatinUpper : KeyboardMode.LatinLower;
            }
        }

        private string GetKeyMap(KeyboardMode mode) => KeyMaps[(int)mode];

        private void InitKeyboardMode()
        {
            var mode = GetKeyboardMode();
            View.Text = GetModeDescription(mode);
            var map = GetKeyMap(mode);
            for (var index = 0; index < CustomKeys.Count; index++)
                CustomKeys[index].Text = map[index].ToString().AmpersandEscape();
        }
        // Greek keyboard based on https://en.wikipedia.org/wiki/Keyboard_layout#/media/File:KB_Greek.svg
        private const string Lowercase = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";
        private const string Uppercase = @" ¬!""£$%^&*()_+ /*-QWERTYUIOP{}789+ASDFGHJKL:@~456|ZXCVBNM<>?123 0.";
        private const string GreekLower = @" `1234567890-= /*- ςερτυθιοπ[]789+ασδφγηξκλ;'#456\ζχψωβνμ,./123 0.";
        private const string GreekUpper = @" ¬!""£$%^&*()_+ /*-  ΕΡΤΥΘΙΟΠ{}789+ΑΣΔΦΓΗΞΚΛ:@~456|ΖΧΨΩΒΝΜ<>?123 0.";
        private const string Mathematical = @" ` √∛∜    ≮≯-≠ ÷×-qwertyuiop≰≱789+asdfghjkl;'#456\zxcvbnm≤≥/123 0.";
        private const string Subscript = @"ᵦᵧᵩᵪᵨ     ₍₎₋₌   ₋  ₑᵣₜ ᵤᵢₒₚ  ₇₈₉₊ₐₛ   ₕⱼₖₗ   ₄₅₆  ₓ ᵥ ₙₘ   ₁₂₃ ₀ ";
        private const string SuperLower = @"ᵝᵞᵠᵡᵅᵟᵋᶿᶥᶲ⁽⁾⁻⁼   ⁻ ʷᵉʳᵗʸᵘⁱᵒᵖ  ⁷⁸⁹⁺ᵃˢᵈᶠᵍʰʲᵏˡ   ⁴⁵⁶ ᶻˣᶜᵛᵇⁿᵐ   ¹²³ ⁰ ";
        private const string SuperUpper = @"          ⁽⁾⁻⁼   ⁻ ᵂᴱᴿᵀ ᵁᴵᴼᴾ  ⁷⁸⁹⁺ᴬ ᴰ ᴳᴴᴶᴷᴸ   ⁴⁵⁶    ⱽᴮᴺᴹ   ¹²³ ⁰ ";

        private static readonly string[] KeyMaps = new[]
        {
            Lowercase,
            Uppercase,
            GreekLower,
            GreekUpper,
            Mathematical,
            Subscript,
            SuperLower,
            SuperUpper
        };

        private string GetModeDescription(KeyboardMode mode) => ModeDescriptions[(int)mode];

        private static readonly string[] ModeDescriptions = new[]
        {
            "Latin Lowercase",
            "Latin Uppercase",
            "Greek Lowercase",
            "Greek Uppercase",
            "Mathematical",
            "Subscript",
            "Superscript Lowercase",
            "Superscript Uppercase"
        };


        #endregion

        private void ViewButton_Click(object sender, System.EventArgs e)
        {
            var text = ((Control)sender).Text;
            if (text != string.Empty)
                PassThrough(text[0]);
            State &= ~KeyStates.Shift;
            FocusTextBox();
        }

        private void FocusTextBox() => View.TextBox.Focus();

        private void PassThrough(char c)
        {
            View.TextBox.SelectedText = c.ToString();
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            e.Cancel = true;
            View.Hide();
        }

        /*
        private Keys LastKeyDownCode;

        private void View_KeyDown(object sender, KeyEventArgs e) =>
            LastKeyDownCode = e.KeyCode;

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == LastKeyDownCode)
                switch (e.KeyCode)
                {
                    case Keys.ShiftKey:
                        State ^= KeyStates.Shift;
                        break;
                    case Keys.CapsLock:
                        State ^= KeyStates.ShiftLock;
                        break;
                }
        }

        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            PassThrough(e.KeyChar);
        }
        */

        private void LoadKeys()
        {
            var keys = View.Controls.OfType<Button>().Where(p => p.Tag == null);
            CustomKeys.AddRange(keys);
            foreach (var key in keys)
                key.Click += ViewButton_Click;
        }

        private void UnloadKeys()
        {
            foreach (var key in CustomKeys)
                key.Click -= ViewButton_Click;
            CustomKeys.Clear();
        }

        #region Private Enumerations

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

        [Flags]
        public enum KeyboardMode
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

        #endregion
    }
}
