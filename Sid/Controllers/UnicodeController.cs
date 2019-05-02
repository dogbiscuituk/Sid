namespace Sid.Controllers
{
    using Sid.Views;
    using System.Windows.Forms;

    public class UnicodeController
    {
        public UnicodeController()
        {
            View = new MathematicalKeyboard();
        }

        private MathematicalKeyboard _view;
        private MathematicalKeyboard View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                }
                _view = value;
                if (View != null)
                {
                }
            }
        }

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
    }
}
