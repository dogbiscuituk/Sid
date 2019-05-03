namespace Sid.Controllers
{
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Sid.Expressions;
    using Sid.Views;

    public class KeyController
    {
        public KeyController(LegendController parent)
        {
            Parent = parent;
            View = new KeyView();
            InitFunctionNames();
        }

        #region Properties

        public KeyView _view;
        public KeyView View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    View.cbVisible.CheckedChanged -= Parent.LiveUpdate;
                    FunctionBox.DrawItem -= FunctionBox_DrawItem;
                    FunctionBox.TextChanged -= FunctionBox_TextChanged;
                    View.cbPenColour.SelectedValueChanged -= Parent.LiveUpdate;
                    View.cbFillColour.SelectedValueChanged -= Parent.LiveUpdate;
                    View.seTransparency.ValueChanged -= Parent.LiveUpdate;
                    View.btnRemove.Click -= BtnRemove_Click;
                    ColourController.Clear();
                }
                _view = value;
                if (View != null)
                {
                    View.cbVisible.CheckedChanged += Parent.LiveUpdate;
                    FunctionBox.DrawItem += FunctionBox_DrawItem;
                    FunctionBox.TextChanged += FunctionBox_TextChanged;
                    View.cbPenColour.SelectedValueChanged += Parent.LiveUpdate;
                    View.cbFillColour.SelectedValueChanged += Parent.LiveUpdate;
                    View.seTransparency.ValueChanged += Parent.LiveUpdate;
                    View.btnRemove.Click += BtnRemove_Click;
                    ColourController.AddControls(View.cbPenColour, View.cbFillColour);
                }
            }
        }

        private AppController AppController { get => Parent.Parent; }
        private LegendController Parent;
        private ColourController ColourController = new ColourController();
        private ComboBox FunctionBox { get => View.cbFunction; }
        private ComboBox.ObjectCollection Functions { get => FunctionBox.Items; }

        public bool TraceVisible
        {
            get => View.cbVisible.Checked;
            set => View.cbVisible.Checked = value;
        }

        public string TraceLabel
        {
            get => View.cbVisible.Text;
            set => View.cbVisible.Text = value;
        }

        public string Formula
        {
            get => FunctionBox.Text;
            set => FunctionBox.Text = value;
        }

        public Color PenColour
        {
            get => ColourController.GetColour(View.cbPenColour);
            set => ColourController.SetColour(View.cbPenColour, value);
        }

        public Color FillColour
        {
            get => ColourController.GetColour(View.cbFillColour);
            set => ColourController.SetColour(View.cbFillColour, value);
        }

        public int FillTransparencyPercent
        {
            get => (int)View.seTransparency.Value;
            set => View.seTransparency.Value = value;
        }

        #endregion

        #region Functions

        private void FunctionBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var r = e.Bounds;
            var functionName = ((ComboBox)sender).Items[e.Index].ToString();
            e.DrawBackground();
            using (var brush = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(functionName, e.Font, brush, r);
        }

        private void FunctionBox_TextChanged(object sender, System.EventArgs e)
        {
            var function = FunctionBox.Text;
            if (!string.IsNullOrWhiteSpace(function) && !Functions.Contains(function))
                Functions[0] = function;
            Parent.LiveUpdate(sender, e);
        }

        private void InitFunctionNames()
        {
            Functions.Clear();
            Functions.Add(string.Empty);
            Functions.AddRange(Utility.FunctionNames.Select(f => $"{f}(x)").ToArray());
        }

        #endregion

        #region Key Management

        private void BtnRemove_Click(object sender, System.EventArgs e) =>
            Parent.RemoveKey(View);

        #endregion
    }
}
