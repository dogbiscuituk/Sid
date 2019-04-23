namespace Sid.Views
{
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using FormulaBuilder;

    public partial class TraceEditor : UserControl
    {
        public TraceEditor()
        {
            InitializeComponent();
            InitColours();
        }

        public bool TraceVisible
        {
            get => cbVisible.Checked;
            set => cbVisible.Checked = value;
        }

        public string Formula
        {
            get => cbFunction.Text;
            set => cbFunction.Text = value;
        }

        public Color PenColour
        {
            get => GetColour(cbPenColour);
            set => SetColour(cbPenColour, value);
        }

        public Color FillColour
        {
            get => GetColour(cbFillColour);
            set => SetColour(cbFillColour, value);
        }

        public int FillOpacity
        {
            get
            {
                var x = cbFillOpacity.SelectedItem;
                return 100;
            }
        }

        private void ColourCombo_DrawItem(object sender, DrawItemEventArgs e) =>
            DrawColour((ComboBox)sender, e);

        private void FunctionCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            var functionName = ((ComboBox)sender).Items[e.Index].ToString();
            e.DrawBackground();
            using (var brush = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(functionName, e.Font, brush, e.Bounds);
        }

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

        private Color GetColour(ComboBox comboBox) =>
            Color.FromName(comboBox.SelectedItem.ToString());

        private void InitColours()
        {
            cbFunction.Items.AddRange(Functions.FunctionNames.Select(f => $"{f}(x)").ToArray());
            var colourNames = Utility.NonSystemColourNames.ToArray();
            cbPenColour.Items.AddRange(colourNames);
            cbFillColour.Items.AddRange(colourNames);
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
    }
}
