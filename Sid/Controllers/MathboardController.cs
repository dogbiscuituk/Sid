namespace Sid.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Sid.Views;

    public class MathboardController
    {
        public MathboardController()
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
                    CustomKeys.Clear();
                }
                _view = value;
                if (View != null)
                {
                    PopulateKeys();
                    View.KeyDown += View_KeyDown;
                    View.KeyPress += View_KeyPress;
                    View.KeyUp += View_KeyUp;
                    View.PopupLowercase.Click += PopupLowercase_Click;
                    View.PopupUppercase.Click += PopupUppercase_Click;
                    View.PopupGreekLower.Click += PopupGreekLower_Click;
                    View.PopupGreekUpper.Click += PopupGreekUpper_Click;
                    View.PopupSubscript.Click += PopupSubscript_Click;
                    View.PopupSuperLowercase.Click += PopupSuperLowercase_Click;
                    View.PopupSuperUppercase.Click += PopupSuperUppercase_Click;
                }
            }
        }

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Key Up. KeyCode = {e.KeyCode}, KeyData = {e.KeyData}.");
        }

        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Key Press. KeyChar = {e.KeyChar}.");
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Key Down. KeyCode = {e.KeyCode}, KeyData = {e.KeyData}.");
        }

        private void PopupLowercase_Click(object sender, System.EventArgs e) => LoadKeys(Lowercase);
        private void PopupUppercase_Click(object sender, System.EventArgs e) => LoadKeys(Uppercase);
        private void PopupGreekLower_Click(object sender, System.EventArgs e) => LoadKeys(GreekLower);
        private void PopupGreekUpper_Click(object sender, System.EventArgs e) => LoadKeys(GreekUpper);
        private void PopupSubscript_Click(object sender, System.EventArgs e) => LoadKeys(Subscript);
        private void PopupSuperLowercase_Click(object sender, System.EventArgs e) => LoadKeys(SuperLower);
        private void PopupSuperUppercase_Click(object sender, System.EventArgs e) => LoadKeys(SuperUpper);

        private void LoadKeys(string keys)
        {
            for (var index = 0; index < CustomKeys.Count; index++)
                CustomKeys[index].Text = keys[index].ToString();
        }

        private void PopulateKeys() =>
            CustomKeys.AddRange(View.Controls.OfType<Button>().Where(p => p.Tag == null));

        private readonly List<Button> CustomKeys = new List<Button>();

        public void Show(IWin32Window owner)
        {
            if (!View.Visible)
                View.Show(owner);
            else
                View.BringToFront();
        }

        private string GetKeys(KeyboardModes mode)
        {
            switch (mode)
            {
                case KeyboardModes.Normal:
                    return "";
                case KeyboardModes.Shift:
                    return "";
            }
            return string.Empty;
        }

        private const string Lowercase = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";
        private const string Uppercase = @" ¬!""£$%^&*()_+ /*-QWERTYUIOP{}789+ASDFGHJKL:@~456|ZXCVBNM<>?123 0.";
        private const string GreekLower = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";

        private const string GreekUpper = @" ¬!""£$%^&*()_+ /*-QWΕΡΤΥΘΙΟΠ{}789+ΑΣΔΦΓΗΞΚΛ:@~456|ΖΧΨΩΒΝΜ<>?123 0.";

        private const string Mathematical = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";
        private const string Subscript = @"ᵦᵧᵩᵪᵨ     ₍₎₋₌   ₋  ₑᵣₜ ᵤᵢₒₚ  ₇₈₉₊ₐₛ   ₕⱼₖₗ   ₄₅₆  ₓ ᵥ ₙₘ   ₁₂₃ ₀ ";
        private const string SuperLower = @"ᵝᵞᵠᵡᵅᵟᵋᶿᶥᶲ⁽⁾⁻⁼   ⁻ ʷᵉʳᵗʸᵘⁱᵒᵖ  ⁷⁸⁹⁺ᵃˢᵈᶠᵍʰʲᵏˡ   ⁴⁵⁶ ᶻˣᶜᵛᵇⁿᵐ   ¹²³ ⁰ ";
        private const string SuperUpper = @"          ⁽⁾⁻⁼   ⁻ ᵂᴱᴿᵀ ᵁᴵᴼᴾ  ⁷⁸⁹⁺ᴬ ᴰ ᴳᴴᴶᴷᴸ   ⁴⁵⁶    ⱽᴮᴺᴹ   ¹²³ ⁰ ";

        string xxx = "ABDEHIKLMNOPRSTXYZ";
        string xxy = "               ,";
    }
}
