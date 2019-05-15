namespace ToyGraf.Controllers
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Expressions;

    public class ColourController
    {
        public ColourController() { }

        #region Properties

        public readonly List<ComboBox> Controls = new List<ComboBox>();

        private static readonly string[] colourNames = Utility.NonSystemColourNames.ToArray();

        #endregion

        #region Control Management

        public void AddControls(params ComboBox[] controls)
        {
            Controls.AddRange(controls);
            foreach (var control in controls)
            {
                control.Items.AddRange(colourNames);
                control.DrawItem += Control_DrawItem;
            }
        }

        public void Clear()
        {
            foreach (var control in Controls)
                control.DrawItem -= Control_DrawItem;
            Controls.Clear();
        }

        #endregion

        #region Colour Management

        public Color GetColour(ComboBox comboBox) =>
            Color.FromName(comboBox.SelectedItem.ToString());

        public void SetColour(ComboBox comboBox, Color colour) =>
            comboBox.SelectedIndex = comboBox.Items.IndexOf(colour.Name);

        private void Control_DrawItem(object sender, DrawItemEventArgs e)
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
            e.Graphics.DrawString(colourName, e.Font, brush, r.X + 1, r.Y - 1);
            if (selected)
                using (var pen = new Pen(brush))
                {
                    pen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, r.X, r.Y, r.Width - 1, r.Height - 1);
                }
        }

        /// <summary>
        /// Uses Luma to determine whether or not a Color is "bright".
        /// https://en.wikipedia.org/wiki/Luma_%28video%29
        /// </summary>
        /// <param name="colour">The sample colour.</param>
        /// <returns>True if the sample colour's Luma value is above 0.5, otherwise False.</returns>
        private static bool IsBright(Color colour) =>
            (0.2126 * colour.R + 0.7152 * colour.G + 0.0722 * colour.B) / 255 > 0.5;

        #endregion
    }
}
