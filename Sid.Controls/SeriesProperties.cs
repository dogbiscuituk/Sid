using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sid.Views
{
    public partial class SeriesProperties : UserControl
    {
        public SeriesProperties()
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
            set => cbPenColour.SelectedIndex = Array.IndexOf(Palette, valuel);
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
            using (var brush = new SolidBrush(Palette[e.Index + 1]))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }
    }
}
