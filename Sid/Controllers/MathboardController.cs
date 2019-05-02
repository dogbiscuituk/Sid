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
                    PopulateCustomKeys();
                    LoadCustomKeys(SuperLower);
                }
            }
        }

        private void LoadCustomKeys(string keys)
        {
            for (var index = 0; index < CustomKeys.Count; index++)
                CustomKeys[index].Text = keys[index].ToString();
        }

        private void PopulateCustomKeys() =>
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
        private const string Subscript = @"          ₍₎₋₌   ₋  ₑᵣₜ ᵤᵢₒₚ  ₇₈₉₊ₐₛ   ₕⱼₖₗ   ₄₅₆  ₓ ᵥ ₙₘ   ₁₂₃ ₀ ";
        private const string SuperLower = @"          ⁽⁾⁻⁼ /*⁻qʷᵉʳᵗʸᵘⁱᵒᵖ[]⁷⁸⁹⁺ᵃˢᵈᶠᵍʰʲᵏˡ;'#⁴⁵⁶\ᶻˣᶜᵛᵇⁿᵐ,./¹²³ ⁰.";

        private string sub = "ᵦᵧᵨᵩᵪ";
        private string Superscripts = "ᵝᵞρᵠᵡ ᴬᴮᴰᴱᴳᴴᴵᴶᴷᴸᴹᴺᴼᴾᴿᵀᵁⱽᵂᵅᵟᵋᶿᶥᶲ";

        private const string GreekLower = @" `1234567890-= /*-qwertyuiop[]789+asdfghjkl;'#456\zxcvbnm,./123 0.";
        private const string GreekUpper = @" ¬!""£$%^&*()_+ /*-QWERTYUIOP{}789+ASDFGHJKL:@~456|ZXCVBNM<>?123 0.";
    }
}
