namespace Sid.Controllers
{
    using System;
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
                    View.ElementCheckboxes.ItemCheck -= ElementCheckboxes_ItemCheck;
                    View.btnApply.Click -= BtnApply_Click;
                }
                _view = value;
                if (View != null)
                {
                    ColourController.AddControls(
                        View.cbAxisColour, View.cbGridColour, View.cbPenColour,
                        View.cbLimitColour, View.cbPaperColour, View.cbFillColour);
                    View.ElementCheckboxes.ItemCheck += ElementCheckboxes_ItemCheck;
                    View.btnApply.Click += BtnApply_Click;
                }
            }
        }

        private void ElementCheckboxes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == e.CurrentValue)
                return;
            var index = e.Index;
            int x = index % 4, y = x + 4, z = y + 4;
            CheckState
                xState = View.ElementCheckboxes.GetItemCheckState(x),
                yState = View.ElementCheckboxes.GetItemCheckState(y),
                zState = View.ElementCheckboxes.GetItemCheckState(z);

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

        #region Read/Write Elements

        private Elements[] GetElementValues() => (Elements[])Enum.GetValues(typeof(Elements));

        private int ControlToEnum(int index) => index < 11 ? 3 * index % 11 : 11;

        private void ReadElements()
        {
            var values = GetElementValues();
            var graphElements = Graph.Elements;
            for (var index = 0; index < View.ElementCheckboxes.Items.Count; index++)
            {
                var value = values[ControlToEnum(index)];
                var graphValue = graphElements & value;
                var state = graphValue == value ? CheckState.Checked
                    : graphValue == 0 ? CheckState.Unchecked
                    : CheckState.Indeterminate;
                View.ElementCheckboxes.SetItemCheckState(index, state);
            }
        }

        private void WriteElements()
        {
            var values = GetElementValues();
            Elements graphElements = 0;
            for (var index = 0; index < View.ElementCheckboxes.Items.Count; index++)
                if (View.ElementCheckboxes.GetItemCheckState(index) == CheckState.Checked)
                    graphElements |= values[ControlToEnum(index)];
            Graph.Elements = graphElements;
        }

        #endregion

        #region Read/Write Model

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
            ReadElements();
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
            WriteElements();
        }

        #endregion
    }
}
