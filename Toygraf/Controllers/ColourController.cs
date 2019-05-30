namespace ToyGraf.Controllers
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;

    internal class ColourController
    {
        #region Internal Interface

        internal ColourController() { }

        internal readonly List<ComboBox> Controls = new List<ComboBox>();

        internal void AddControls(params ComboBox[] controls)
        {
            Controls.AddRange(controls);
            foreach (var control in controls)
            {
                control.Items.AddRange(NonSystemColourNames);
                control.DrawItem += Control_DrawItem;
            }
        }

        internal void Clear()
        {
            foreach (var control in Controls)
                control.DrawItem -= Control_DrawItem;
            Controls.Clear();
        }

        internal Color GetColour(ComboBox comboBox)
        {
            var item = comboBox.SelectedItem;
            if (item != null)
                return Color.FromName(item.ToString());
            else if (comboBox.Tag is Color)
                return (Color)comboBox.Tag;
            else
                return Color.Transparent;
        }

        internal void SetColour(ComboBox comboBox, Color colour)
        {
            var argb = colour.ToArgb();
            comboBox.Tag = colour;
            var name = comboBox.Items.Cast<string>()
                .FirstOrDefault(s => Color.FromName(s).ToArgb() == argb);
            comboBox.SelectedIndex =
                string.IsNullOrWhiteSpace(name) ? -1 : comboBox.Items.IndexOf(name);
            comboBox.Invalidate();
        }

        #endregion

        #region Private Implementation

        private static readonly string[] NonSystemColourNames =
            Utility.GetNonSystemColourNames(Properties.Settings.Default.KnownColorSortOrder).ToArray();

        private void Control_DrawItem(object sender, DrawItemEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            string colourName;
            Color colour;
            if (e.Index >= 0)
            {
                colourName = comboBox.Items[e.Index].ToString();
                colour = Color.FromName(colourName);
            }
            else if (comboBox.Tag is Color)
            {
                colour = (Color)comboBox.Tag;
                colourName = $"{colour.ToArgb() & 0xffffff:X}";
            }
            else
                return;
            Color
                ground = colour,
                figure = ground.Contrast();
            var selected = (e.State & DrawItemState.Selected) != 0;
            var g = e.Graphics;
            g.SetOptimization(Optimization.HighQuality);
            var r = e.Bounds;
            using (Brush figureBrush = new SolidBrush(figure), groundBrush = new SolidBrush(ground))
            {
                e.Graphics.FillRectangle(groundBrush, r);
                e.Graphics.DrawString(colourName, e.Font, figureBrush, r.X + 1, r.Y);
                if (selected)
                    using (Pen pen = new Pen(figureBrush))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        e.Graphics.DrawRectangle(pen, r.X, r.Y, r.Width - 1, r.Height - 1);
                    }
            }
        }

        #endregion
    }
}
