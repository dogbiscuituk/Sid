namespace Sid.Controllers
{
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using FormulaBuilder;
    using Sid.Views;

    public class TraceEditController
    {
        public TraceEditController(GraphDialogController parent)
        {
            Parent = parent;
            View = new TraceEdit();
            InitColours();
        }

        #region Properties

        public TraceEdit _view;
        public TraceEdit View
        {
            get => _view;
            set
            {
                _view = value;
                View.cbPenColour.DrawItem += ColourCombo_DrawItem;
                View.cbFunction.DrawItem += FunctionCombo_DrawItem;
                View.cbFillColour.DrawItem += ColourCombo_DrawItem;

                View.cbVisible.CheckedChanged += Parent.LiveUpdate;
                View.cbFunction.TextChanged += Parent.LiveUpdate;
                View.cbPenColour.SelectedValueChanged += Parent.LiveUpdate;
                View.cbFillColour.SelectedValueChanged += Parent.LiveUpdate;
                View.seTransparency.ValueChanged += Parent.LiveUpdate;
            }
        }

        private GraphDialogController Parent;

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

        private void ColourCombo_DrawItem(object sender, DrawItemEventArgs e) =>
            DrawColour((ComboBox)sender, e);

        private void DrawColour(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            var colourName = ((ComboBox)sender).Items[e.Index].ToString();
            Color colour;
            if ((e.State & DrawItemState.Selected) != 0)
            {
                colour = SystemColors.Highlight;
                e.DrawBackground();
            }
            else
            {
                colour = Color.FromName(colourName);
                using (var fill = new SolidBrush(colour))
                    e.Graphics.FillRectangle(fill, e.Bounds);
            }
            var brush = IsBright(colour) ? Brushes.Black : Brushes.White;
            using (var font = new Font("Arial", 9, FontStyle.Regular))
                e.Graphics.DrawString(colourName, font, brush, e.Bounds.X, e.Bounds.Top);
        }

        private void FunctionCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            var functionName = ((ComboBox)sender).Items[e.Index].ToString();
            e.DrawBackground();
            using (var brush = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(functionName, e.Font, brush, e.Bounds);
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
