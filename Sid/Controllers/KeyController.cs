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
            InitColours();
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
                    View.cbPenColour.DrawItem -= DrawColour;
                    View.cbPenColour.SelectedValueChanged -= Parent.LiveUpdate;
                    View.cbFillColour.DrawItem -= DrawColour;
                    View.cbFillColour.SelectedValueChanged -= Parent.LiveUpdate;
                    View.seTransparency.ValueChanged -= Parent.LiveUpdate;
                    View.btnRemove.Click -= BtnRemove_Click;
                }
                _view = value;
                if (View != null)
                {
                    View.cbVisible.CheckedChanged += Parent.LiveUpdate;
                    View.cbFunction.DrawItem += FunctionCombo_DrawItem;
                    View.cbFunction.TextChanged += Parent.LiveUpdate;
                    View.cbPenColour.DrawItem += DrawColour;
                    View.cbPenColour.SelectedValueChanged += Parent.LiveUpdate;
                    View.cbFillColour.DrawItem += DrawColour;
                    View.cbFillColour.SelectedValueChanged += Parent.LiveUpdate;
                    View.seTransparency.ValueChanged += Parent.LiveUpdate;
                    View.btnRemove.Click += BtnRemove_Click;
                }
            }
        }

        private LegendController Parent;

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

        private void BtnRemove_Click(object sender, System.EventArgs e)
        {
            Parent.Children.Remove(this);
        }

        #endregion

        #region Colours

        private void DrawColour(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            var r = e.Bounds;
            var colourName = ((ComboBox)sender).Items[e.Index].ToString();
            var selected = (e.State & DrawItemState.Selected) != 0;
            var colour = Color.FromName(colourName);
            using (var fill = new SolidBrush(colour))
                e.Graphics.FillRectangle(fill, r);
            var brush = IsBright(colour) ? Brushes.Black : Brushes.White;
            e.Graphics.DrawString(colourName, e.Font, brush, r.X + 1, r.Y + 1);
            if (selected)
                using (var pen = new Pen(brush))
                {
                    pen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, r.X, r.Y, r.Width - 1, r.Height - 1);
                }
        }

        private void FunctionCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            var r = e.Bounds;
            var functionName = ((ComboBox)sender).Items[e.Index].ToString();
            e.DrawBackground();
            using (var brush = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(functionName, e.Font, brush, r);
        }

        private Color GetColour(ComboBox comboBox) =>
            Color.FromName(comboBox.SelectedItem.ToString());

        private void InitColours()
        {
            View.cbFunction.Items.AddRange(Utility.FunctionNames.Select(f => $"{f}(x)").ToArray());
            var colourNames = Utility.NonSystemColourNames.ToArray();
            View.cbPenColour.Items.AddRange(colourNames);
            View.cbFillColour.Items.AddRange(colourNames);
        }

        /// <summary>
        /// Uses Luma to determine whether or not a Color is "bright".
        /// https://en.wikipedia.org/wiki/Luma_%28video%29
        /// </summary>
        /// <param name="colour">The sample colour.</param>
        /// <returns>True if the sample colour's Luma value is above 0.5, otherwise False.</returns>
        private bool IsBright(Color colour) =>
            0.2126 * colour.R + 0.7152 * colour.G + 0.0722 * colour.B > 127.5;

        private void SetColour(ComboBox comboBox, Color colour) =>
            comboBox.SelectedIndex = comboBox.Items.IndexOf(colour.Name);

        #endregion
    }
}
