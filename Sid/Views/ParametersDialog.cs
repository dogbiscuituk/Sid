namespace Sid
{
    using System.Drawing;
    using System.Windows.Forms;

    public partial class ParametersDialog : Form
    {
        public ParametersDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            using (var dialog = new ColorDialog())
                dialog.ShowDialog();
        }

        private static Color[] Palette = new[] {
            Color.White,
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

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            using (var brush = new SolidBrush(Palette[e.Index + 1]))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}
