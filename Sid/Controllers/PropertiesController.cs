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

        #region Properties

        private CheckedListBox ClbElements { get => View.ElementCheckboxes; }
        private Model Model { get; set; }
        private Graph Graph { get => Model.Graph; }
        private bool Loading;

        private PropertiesDialog _view;
        private PropertiesDialog View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    ColourController.Clear();
                    ClbElements.ItemCheck -= ClbElements_ItemCheck;
                    View.btnApply.Click -= BtnApply_Click;
                }
                _view = value;
                if (View != null)
                {
                    ColourController.AddControls(
                        View.cbAxisColour, View.cbGridColour, View.cbPenColour,
                        View.cbLimitColour, View.cbPaperColour, View.cbFillColour);
                    ClbElements.ItemCheck += ClbElements_ItemCheck;
                    View.btnApply.Click += BtnApply_Click;
                }
            }
        }

        private ColourController ColourController = new ColourController();

        #endregion

        #region Execute

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

        #endregion

        #region Elements

        private void ClbElements_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (Loading || e.NewValue == e.CurrentValue)
                return;
            Loading = true;
            int i = e.Index, x = i % 4, y = x + 4, z = y + 4;
            CheckState
                xState = ClbElements.GetItemCheckState(x),
                yState = ClbElements.GetItemCheckState(y),
                zState = ClbElements.GetItemCheckState(z);
            if (i == x)
                SetState(z, e.NewValue == yState ? e.NewValue : CheckState.Indeterminate);
            else if (i == y)
                SetState(z, xState == e.NewValue ? e.NewValue : CheckState.Indeterminate);
            else
            {
                SetState(x, e.NewValue);
                SetState(y, e.NewValue);
            }
            Loading = false;
        }

        private Elements[] GetElementValues() => (Elements[])Enum.GetValues(typeof(Elements));

        private int ControlToEnum(int index) => index < 11 ? 3 * index % 11 : 11;
        private CheckState GetState(int index) => ClbElements.GetItemCheckState(index);
        private void SetState(int index, CheckState state) => ClbElements.SetItemCheckState(index, state);

        private void ReadElements()
        {
            var values = GetElementValues();
            var graphElements = Graph.Elements;
            for (var index = 0; index < ClbElements.Items.Count; index++)
            {
                var value = values[ControlToEnum(index)];
                var graphValue = graphElements & value;
                var state = graphValue == value ? CheckState.Checked
                    : graphValue == 0 ? CheckState.Unchecked
                    : CheckState.Indeterminate;
                SetState(index, state);
            }
        }

        private void WriteElements()
        {
            var values = GetElementValues();
            Elements graphElements = 0;
            for (var index = 0; index < ClbElements.Items.Count; index++)
                if (GetState(index) == CheckState.Checked)
                    graphElements |= values[ControlToEnum(index)];
            Graph.Elements = graphElements;
        }

        #endregion

        #region Model

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
