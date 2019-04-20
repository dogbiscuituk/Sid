using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sid.Views
{
    public partial class TraceEditor : UserControl
    {
        public TraceEditor()
        {
            InitializeComponent();
        }

        public string Formula
        {
            get => cbFunction.Text;
            set => cbFunction.Text = value;
        }

        public Color PenColour
        {
            get => Palette[cbPenColour.SelectedIndex];
            set => cbPenColour.SelectedIndex = Array.IndexOf(Palette, value);
        }

        public Color FillColour
        {
            get
            {
                return Color.FromArgb(100, Palette[cbFillColour.SelectedIndex]);
            }
            set
            {
                cbFillColour.SelectedIndex = Array.IndexOf(Palette, value);
            }
        }

        public int FillOpacity
        {
            get
            {
                var x = cbFillOpacity.SelectedItem;
                return 100;
            }
        }

        private static Color[] Palette = new[] {
            Color.Black,
            Color.Brown,
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.GreenYellow,
            Color.LimeGreen,
            Color.Cyan,
            Color.DodgerBlue,
            Color.MediumOrchid,
            Color.Gray,
            Color.White
        };

        private void ColourCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            var index = e.Index;
            var colour = index >= 0 ? Palette[index] : Color.White;
            using (var brush = new SolidBrush(colour))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }
    }
}
