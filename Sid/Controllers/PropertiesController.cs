namespace Sid.Controllers
{
    using System.Windows.Forms;
    using Sid.Models;
    using Sid.Views;

    public class PropertiesController
    {
        public PropertiesController()
        {
            View = new PropertiesView();
        }

        private PropertiesView _view;
        private PropertiesView View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    ColourController.Clear();
                }
                _view = value;
                if (View != null)
                {
                    ColourController.AddControls(
                        View.cbAxisColour, View.cbGridColour, View.cbPenColour,
                        View.cbLimitColour, View.cbPaperColour, View.cbFillColour);
                }
            }
        }

        private ColourController ColourController = new ColourController();

        public DialogResult ShowDialog(IWin32Window owner)
        {
            return View.ShowDialog(owner);
        }
    }
}
