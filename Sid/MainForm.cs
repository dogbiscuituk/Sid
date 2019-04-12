namespace Sid
{
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Windows.Forms;
    using FormulaBuilder;
    using FormulaGrapher;

    public partial class MainForm : Form
    {
        public MainForm() {
            InitializeComponent();
            Differentiator.TestAll(); }

        private void MainForm_Paint(object sender, PaintEventArgs e) {
            DrawGraph(e.Graphics); }

        private void MainForm_Resize(object sender, System.EventArgs e) {
            Invalidate(); }

        private void MainForm_MouseMove(object sender, MouseEventArgs e) {
            var p = Graph.ScreenToGraph(e.Location, ClientRectangle);
            ToolTip.SetToolTip(this, $"({p.X}, {p.Y})"); }

        private void MenuSinX_Click(object sender, System.EventArgs e) {
            DrawGraphs(sender, x.Sin()); }

        private void MenuSinhXover2_Click(object sender, System.EventArgs e) {
            DrawGraphs(sender, x.Over(2).Sinh()); }

        private void Menu1overX_Click(object sender, System.EventArgs e) {
            DrawGraphs(sender, x.Power(-1)); }

        private void MenuLnXsquaredMinus1_Click(object sender, System.EventArgs e) {
            DrawGraphs(sender, x.Squared().Minus(1).Log()); }

        private Graph Graph = new Graph(new RectangleF(-9, -7, 18, 14), 16000);
        private Expression x = Differentiator.x;

        private void DrawGraph(Graphics g) {
            Graph.Draw(g, ClientRectangle); }

        private void DrawGraphs(object Sender, Expression y) {
            Text = ((ToolStripMenuItem)Sender).Text;
            Graph.Clear();
            Graph.AddSeries(y, Color.Black, brush: Color.Yellow);
            y = y.Differentiate();
            Graph.AddSeries(y, Color.Red);
            y = y.Differentiate();
            Graph.AddSeries(y, Color.Green);
            y = y.Differentiate();
            Graph.AddSeries(y, Color.Blue);
            Invalidate(); }
    }
}
