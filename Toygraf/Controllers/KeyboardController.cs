namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    public class KeyboardController
    {
        public KeyboardController(AppController parent)
        {
            Parent = parent;
            View = new KeyboardDialog();
            InitFunctionNames();
        }

        #region Properties

        private KeyboardDialog _view;
        public KeyboardDialog View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    UnloadKeys();
                    View.FormClosing -= View_FormClosing;
                    FunctionBox.KeyUp -= FunctionBox_KeyUp;
                    FunctionBox.MouseUp -= FunctionBox_MouseUp;
                    FunctionBox.TextChanged -= FunctionBox_TextChanged;
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
                    FunctionBox.KeyUp += FunctionBox_KeyUp;
                    FunctionBox.MouseUp += FunctionBox_MouseUp;
                    FunctionBox.TextChanged += FunctionBox_TextChanged;
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

        private readonly AppController Parent;
        private Control ActiveControl { get; set; }
        private readonly List<Button> CustomKeys = new List<Button>();
        private ComboBox FunctionBox { get => View.FunctionBox; }
        private ComboBox.ObjectCollection Functions { get => FunctionBox.Items; }
        private Graph Graph;
        private int Index;
        private int SelStart, SelLength;

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

        #endregion

        #region Show/Hide

        private LegendController LegendController { get => Parent.LegendController; }
        private Panel Legend { get => LegendController.View.LegendPanel; }
        private Control.ControlCollection SeriesViews { get => Legend.Controls; }

        public void ShowDialog(IWin32Window owner, Control sender, Point location, Graph graph)
        {
            ActiveControl = sender;
            Graph = graph;
            Index = SeriesViews.IndexOf(sender.Parent);
            FunctionBox.Text = ActiveControl.Text;
            View.Location = location;
            System.Diagnostics.Debug.WriteLine($"Loaded with Index={Index}");
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
            new Keyboard{Keys = @"½⅓⅔¼¾⅕⅖⅗⅘≮≯-≠°÷×-qwertyuiop≰≱789+asdfghjkl;'#456\zxcvbnm≤≥/123 0.", Name = "Mathematical Lowercase"},
            new Keyboard{Keys = @"⅙⅚⅐⅛⅜⅝⅞⅑⅒≮≯-≠°√∛∜QWERTYUIOP≰≱789+ASDFGHJKL;'#456\ZXCVBNM≤≥/123 0.", Name = "Mathematical Uppercase"},
            new Keyboard{Keys = @"ᵦᵧᵩᵪᵨ    ₍₎₋₌   ₋  ₑᵣₜ ᵤᵢₒₚ  ₇₈₉₊ₐₛ   ₕⱼₖₗ   ₄₅₆  ₓ ᵥ ₙₘ   ₁₂₃ ₀ ", Name = "Subscript"},
            new Keyboard{Keys = @"ᵝᵞᵠᵡᵅᵟᵋᶿᶥ⁽⁾⁻⁼   ⁻ᶲʷᵉʳᵗʸᵘⁱᵒᵖ  ⁷⁸⁹⁺ᵃˢᵈᶠᵍʰʲᵏˡ   ⁴⁵⁶ ᶻˣᶜᵛᵇⁿᵐ   ¹²³ ⁰ ", Name = "Superscript Lowercase"},
            new Keyboard{Keys = @"         ⁽⁾⁻⁼   ⁻ ᵂᴱᴿᵀ ᵁᴵᴼᴾ  ⁷⁸⁹⁺ᴬ ᴰ ᴳᴴᴶᴷᴸ   ⁴⁵⁶    ⱽᴮᴺᴹ   ¹²³ ⁰ ", Name = "Superscript Uppercase"}
        };

        private string GetKeyboardName(KeyboardType type) => Keyboards[(int)type].Name;

        #endregion

        #region States

        private void BtnShift_Click(object sender, EventArgs e) =>
            State = State & ~KeyStates.ShiftLock ^ KeyStates.Shift;

        private void BtnShiftLock_Click(object sender, EventArgs e) =>
            State = State & ~KeyStates.Shift ^ KeyStates.ShiftLock;

        private void BtnGreek_Click(object sender, EventArgs e) =>
            ToggleLanguage(KeyStates.Greek);

        private void BtnMaths_Click(object sender, EventArgs e) =>
            ToggleLanguage(KeyStates.Maths);

        private void BtnSubscript_Click(object sender, EventArgs e) =>
            ToggleLanguage(KeyStates.Subs);

        private void BtnSuperscript_Click(object sender, EventArgs e) =>
            ToggleLanguage(KeyStates.Super);

        private void InitBackColour(Control control, KeyStates state) =>
            control.BackColor = Color.FromKnownColor(
                (State & state) == 0 ? KnownColor.ControlLight : KnownColor.Window);

        private void ToggleLanguage(KeyStates state) =>
            State = State & (~KeyStates.Languages | state) ^ state;

        #endregion

        #region Keystrokes

        private void FunctionBox_MouseUp(object sender, MouseEventArgs e) => SaveSelection();
        private void FunctionBox_KeyUp(object sender, KeyEventArgs e) => SaveSelection();

        private void FocusFunctionBox()
        {
            FunctionBox.Focus();
            LoadSelection();
        }

        private void Key_Press(object sender, EventArgs e)
        {
            var text = ((Control)sender).Text;
            if (text != string.Empty)
            {
                FocusFunctionBox();
                FunctionBox.SelectedText = text;
                SaveSelection();
            }
            State &= ~(KeyStates.Shift | KeyStates.Languages);
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

        private void FunctionBox_TextChanged(object sender, EventArgs e)
        {
            var function = FunctionBox.Text;
            if (!string.IsNullOrWhiteSpace(function) && !Functions.Contains(function))
                Functions[0] = function;
            ActiveControl.Text = function;
            var proxies = Graph.GetProxies().ToArray();

            foreach (var proxy in proxies)
                System.Diagnostics.Debug.WriteLine(proxy.AsString());

            View.tbProxy.Text = Index >= 0 && Index < proxies.Length
                ? proxies[Index].AsString()
                : string.Empty;
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

        #endregion

        #region Private Types

        private struct Keyboard { public string Name, Keys; }

        public enum KeyboardType
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
