namespace Sid.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Sid.Expressions;
    using Sid.Views;

    public class MathController
    {
        public MathController() { View = new Mathboard(); }

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

                    View.KeyDown -= View_KeyDown;
                    View.KeyPress -= View_KeyPress;
                    View.KeyUp -= View_KeyUp;
                }
                _view = value;
                if (View != null)
                {
                    LoadKeys();
                    View.FormClosing += View_FormClosing;

                    View.btnLshift.Click += BtnShift_Click;
                    View.btnRshift.Click += BtnShift_Click;
                    View.btnShiftLock.Click += BtnShiftLock_Click;
                    View.btnGreek.Click += BtnGreek_Click;
                    View.btnMaths.Click += BtnMaths_Click;
                    View.btnSubscript.Click += BtnSubscript_Click;
                    View.btnSuperscript.Click += BtnSuperscript_Click;

                    View.KeyDown += View_KeyDown;
                    View.KeyPress += View_KeyPress;
                    View.KeyUp += View_KeyUp;
                }
            }
        }

        private KeyStates _state;
        private KeyStates State
        {
            get => _state;
            set
            {
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

        private void InitKeyboardMode()
        {
            var mode = GetKeyboardMode();
            View.Text = GetModeDescription(mode);
            var map = GetKeyMap(mode);
            for (var index = 0; index < CustomKeys.Count; index++)
                CustomKeys[index].Text = map[index].ToString().AmpersandEscape();
        }

        #endregion

        private void ViewButton_Click(object sender, System.EventArgs e)
        {
            var text = ((Control)sender).Text;
            if (text != string.Empty)
                PassThrough(text[0]);
        }

        private void PassThrough(char c) =>
            System.Diagnostics.Debug.Write(c);

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            e.Cancel = true;
            View.Hide();
        }

        private void View_KeyUp(object sender, KeyEventArgs e) { }
        //    System.Diagnostics.Debug.WriteLine($"Key Up. KeyCode = {e.KeyCode}, KeyData = {e.KeyData}.");

        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"Key Press. KeyChar = {e.KeyChar}.");
            PassThrough(e.KeyChar);
        }

        private void View_KeyDown(object sender, KeyEventArgs e) { }
        //    System.Diagnostics.Debug.WriteLine($"Key Down. KeyCode = {e.KeyCode}, KeyData = {e.KeyData}.");

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

        private readonly List<Button> CustomKeys = new List<Button>();

        public void Show(IWin32Window owner, Control sender)
        {
            if (!View.Visible)
                View.Show(owner);
            else
                View.BringToFront();
        }

        public void Hide()
        {
            View.Close();
        }

        private string GetKeyMap(KeyboardMode mode) => KeyMaps[(int)mode];

        // Greek keyboard based on https://en.wikipedia.org/wiki/Keyboard_layout#/media/File:KB_Greek.svg

        private const string Lowercase = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";
        private const string Uppercase = @" ¬!""£$%^&*()_+ /*-QWERTYUIOP{}789+ASDFGHJKL:@~456|ZXCVBNM<>?123 0.";
        private const string GreekLower = @" `1234567890-= /*- ςερτυθιοπ[]789+ασδφγηξκλ;'#456\ζχψωβνμ,./123 0.";
        private const string GreekUpper = @" ¬!""£$%^&*()_+ /*-  ΕΡΤΥΘΙΟΠ{}789+ΑΣΔΦΓΗΞΚΛ:@~456|ΖΧΨΩΒΝΜ<>?123 0.";
        private const string Mathematical = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";
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
    }
}
