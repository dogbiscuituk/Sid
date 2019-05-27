namespace ToyGraf.Controllers
{
    using System;
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

        internal Color GetColour(ComboBox comboBox) =>
            Color.FromName(comboBox.SelectedItem.ToString());

        internal void SetColour(ComboBox comboBox, Color colour)
        {
            var argb = colour.ToArgb();
            var name = comboBox.Items.Cast<string>()
                .FirstOrDefault(s => Color.FromName(s).ToArgb() == argb);
            comboBox.SelectedIndex =
                string.IsNullOrWhiteSpace(name) ? -1 : comboBox.Items.IndexOf(name);
        }

        #endregion

        #region Private Implementation

        private static readonly string[] NonSystemColourNames =
            Utility.GetNonSystemColourNames(Properties.Settings.Default.KnownColorSortOrder).ToArray();

        private void Control_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            var g = e.Graphics;
            g.SetOptimization(Optimization.HighQuality);
            var r = e.Bounds;
            var colourName = ((ComboBox)sender).Items[e.Index].ToString();
            var selected = (e.State & DrawItemState.Selected) != 0;
            Color
                ground = Color.FromName(colourName),
                figure = ground.IsBright() ? Color.Black : Color.White;
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
