namespace Sid.Controllers
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
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
                    View.cbFunction.DrawItem -= FunctionCombo_DrawItem;
                    View.cbFunction.TextChanged -= Parent.LiveUpdate;
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
                    View.cbFunction.DrawItem += FunctionCombo_DrawItem;
                    View.cbFunction.TextChanged += Parent.LiveUpdate;
                    View.cbPenColour.SelectedValueChanged += Parent.LiveUpdate;
                    View.cbFillColour.SelectedValueChanged += Parent.LiveUpdate;
                    View.seTransparency.ValueChanged += Parent.LiveUpdate;
                    View.btnRemove.Click += BtnRemove_Click;
                    ColourController.AddControls(View.cbPenColour, View.cbFillColour);
                }
            }
        }

        private LegendController Parent;

        private ColourController ColourController = new ColourController();

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
            get => View.cbFunction.Text;
            set => View.cbFunction.Text = value;
        }

        public Color PenColour
        {
            get => GetColour(View.cbPenColour);
            set => SetColour(View.cbPenColour, value);
        }

        public Color FillColour
        {
            get => GetColour(View.cbFillColour);
            set => SetColour(View.cbFillColour, value);
        }

        public int FillTransparencyPercent
        {
            get => (int)View.seTransparency.Value;
            set => View.seTransparency.Value = value;
        }

        #endregion

        #region Colours

        private Color GetColour(ComboBox comboBox) =>
            Color.FromName(comboBox.SelectedItem.ToString());

        private void SetColour(ComboBox comboBox, Color colour) =>
            comboBox.SelectedIndex = comboBox.Items.IndexOf(colour.Name);

        #endregion

        #region Functions

        private void FunctionCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            var r = e.Bounds;
            var functionName = ((ComboBox)sender).Items[e.Index].ToString();
            e.DrawBackground();
            using (var brush = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(functionName, e.Font, brush, r);
        }

        private void InitFunctionNames()
        {
            View.cbFunction.Items.AddRange(Utility.FunctionNames.Select(f => $"{f}(x)").ToArray());
        }

        #endregion

        #region Key Management

        private void BtnRemove_Click(object sender, System.EventArgs e) =>
            Parent.RemoveKey(View);

        #endregion
    }
}
