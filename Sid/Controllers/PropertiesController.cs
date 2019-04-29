namespace Sid.Controllers
{
    using System.Windows.Forms;
    using Sid.Models;
    using Sid.Views;

    public class PropertiesController
    {
        public PropertiesController(Model model)
        {
            Model = model;
            View = new PropertiesDialog();
        }

        private Model Model { get; set; }
        private Graph Graph { get => Model.Graph; }

        private PropertiesDialog _view;
        private PropertiesDialog View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    ColourController.Clear();
                    View.btnApply.Click -= BtnApply_Click;
                }
                _view = value;
                if (View != null)
                {
                    ColourController.AddControls(
                        View.cbAxisColour, View.cbGridColour, View.cbPenColour,
                        View.cbLimitColour, View.cbPaperColour, View.cbFillColour);
                    View.btnApply.Click += BtnApply_Click;
                }
            }
        }

        private ColourController ColourController = new ColourController();

        public bool Execute(IWin32Window owner)
        {
            ReadModel();
            var ok = View.ShowDialog(owner) == DialogResult.OK;
            if (ok)
                WriteModel();
            return ok;
        }

        private void BtnApply_Click(object sender, System.EventArgs e)
        {
            WriteModel();
        }

        private void ReadModel()
        {
            ColourController.SetColour(View.cbAxisColour, Graph.AxisColour);
            ColourController.SetColour(View.cbGridColour, Graph.GridColour);
            ColourController.SetColour(View.cbPenColour, Graph.PenColour);
            ColourController.SetColour(View.cbLimitColour, Graph.LimitColour);
            ColourController.SetColour(View.cbPaperColour, Graph.PaperColour);
            ColourController.SetColour(View.cbFillColour, Graph.FillColour);
            View.sePaperTransparency.Value = Graph.PaperTransparencyPercent;
            View.seFillTransparency.Value = Graph.FillTransparencyPercent;
        }

        private void WriteModel()
        {
            Graph.AxisColour = ColourController.GetColour(View.cbAxisColour);
            Graph.GridColour = ColourController.GetColour(View.cbGridColour);
            Graph.PenColour = ColourController.GetColour(View.cbPenColour);
            Graph.LimitColour = ColourController.GetColour(View.cbLimitColour);
            Graph.PaperColour = ColourController.GetColour(View.cbPaperColour);
            Graph.FillColour = ColourController.GetColour(View.cbFillColour);
            Graph.PaperTransparencyPercent = (int)View.sePaperTransparency.Value;
            Graph.FillTransparencyPercent = (int)View.seFillTransparency.Value;
        }
    }
}
