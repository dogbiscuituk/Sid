namespace Sid.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Sid.Expressions;
    using Sid.Views;

    public class MathController
    {
        public MathController()
        {
            View = new Mathboard();
        }

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
                    View.PopupLowercase.Click -= PopupLowercase_Click;
                    View.PopupUppercase.Click -= PopupUppercase_Click;
                    View.PopupGreekLower.Click -= PopupGreekLower_Click;
                    View.PopupGreekUpper.Click -= PopupGreekUpper_Click;
                    View.PopupMathematical.Click -= PopupMathematical_Click;
                    View.PopupSubscript.Click -= PopupSubscript_Click;
                    View.PopupSuperLower.Click -= PopupSuperLower_Click;
                    View.PopupSuperUpper.Click -= PopupSuperUpper_Click;
                }
                _view = value;
                if (View != null)
                {
                    LoadKeys();
                    View.FormClosing += View_FormClosing;
                    View.KeyDown += View_KeyDown;
                    View.KeyPress += View_KeyPress;
                    View.KeyUp += View_KeyUp;
                    View.PopupLowercase.Click += PopupLowercase_Click;
                    View.PopupUppercase.Click += PopupUppercase_Click;
                    View.PopupGreekLower.Click += PopupGreekLower_Click;
                    View.PopupGreekUpper.Click += PopupGreekUpper_Click;
                    View.PopupMathematical.Click += PopupMathematical_Click;
                    View.PopupSubscript.Click += PopupSubscript_Click;
                    View.PopupSuperLower.Click += PopupSuperLower_Click;
                    View.PopupSuperUpper.Click += PopupSuperUpper_Click;
                }
            }
        }

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

        private void PopupLowercase_Click(object sender, System.EventArgs e) => InitKeys(KeyboardMode.LowerCase);
        private void PopupUppercase_Click(object sender, System.EventArgs e) => InitKeys(KeyboardMode.UpperCase);
        private void PopupGreekLower_Click(object sender, System.EventArgs e) => InitKeys(KeyboardMode.GreekLower);
        private void PopupGreekUpper_Click(object sender, System.EventArgs e) => InitKeys(KeyboardMode.GreekUpper);
        private void PopupMathematical_Click(object sender, System.EventArgs e) => InitKeys(KeyboardMode.Mathematical);
        private void PopupSubscript_Click(object sender, System.EventArgs e) => InitKeys(KeyboardMode.Subscript);
        private void PopupSuperLower_Click(object sender, System.EventArgs e) => InitKeys(KeyboardMode.SuperLower);
        private void PopupSuperUpper_Click(object sender, System.EventArgs e) => InitKeys(KeyboardMode.SuperUpper);

        private void InitKeys(KeyboardMode mode)
        {
            var map = GetKeyMap(mode);
            for (var index = 0; index < CustomKeys.Count; index++)
                CustomKeys[index].Text = map[index].ToString().AmpersandEscape();
        }

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

        private const string Lowercase = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";
        private const string Uppercase = @" ¬!""£$%^&*()_+ /*-QWERTYUIOP{}789+ASDFGHJKL:@~456|ZXCVBNM<>?123 0.";
        private const string GreekLower = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";
        private const string GreekUpper = @" ¬!""£$%^&*()_+ /*-QWΕΡΤΥΘΙΟΠ{}789+ΑΣΔΦΓΗΞΚΛ:@~456|ΖΧΨΩΒΝΜ<>?123 0.";
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
    }
}
