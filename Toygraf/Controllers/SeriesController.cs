namespace ToyGraf.Controllers
{
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class SeriesController
    {
        #region Internal Interface

        internal SeriesController(LegendController parent)
        {
            Parent = parent;
            View = new SeriesView();
            InitFunctionNames();
        }

        internal SeriesView View
        {
            get => _view;
            set
            {
                _view = value;
                View.cbVisible.CheckedChanged += Parent.LiveUpdate;
                FunctionBox.DrawItem += FunctionBox_DrawItem;
                FunctionBox.TextChanged += FunctionBox_TextChanged;
                View.cbPenColour.SelectedValueChanged += Parent.LiveUpdate;
                View.cbFillColour.SelectedValueChanged += Parent.LiveUpdate;
                View.seTransparency.ValueChanged += Parent.LiveUpdate;
                View.btnDetails.Click += BtnDetails_Click;
                View.btnRemove.Click += BtnRemove_Click;
                ColourController.AddControls(View.cbPenColour, View.cbFillColour);
            }
        }

        internal bool TraceVisible
        {
            get => View.cbVisible.Checked;
            set => View.cbVisible.Checked = value;
        }

        internal string TraceLabel
        {
            get => View.Label.Text;
            set => View.Label.Text = value;
        }

        internal string Formula
        {
            get => FunctionBox.Text;
            set => FunctionBox.Text = value;
        }

        internal Color PenColour
        {
            get => ColourController.GetColour(View.cbPenColour);
            set => ColourController.SetColour(View.cbPenColour, value);
        }

        internal Color FillColour
        {
            get => ColourController.GetColour(View.cbFillColour);
            set => ColourController.SetColour(View.cbFillColour, value);
        }

        internal int FillTransparencyPercent
        {
            get => (int)View.seTransparency.Value;
            set => View.seTransparency.Value = value;
        }

        #endregion

        #region Private Properties

        private SeriesView _view;
        private AppController AppController { get => Parent.Parent; }
        private LegendController Parent;
        private ColourController ColourController = new ColourController();
        private CommandProcessor CommandController { get => AppController.CommandProcessor; }
        private KeyboardController MathController { get => AppController.KeyboardController; }
        private int Index { get => Parent.IndexOf(this); }
        private ComboBox FunctionBox { get => View.cbFunction; }
        private ComboBox.ObjectCollection Functions { get => FunctionBox.Items; }
        private Graph Graph { get => Parent.Parent.Graph; }

        #endregion

        #region Private Event Handlers

        private void BtnDetails_Click(object sender, System.EventArgs e)
        {
            int h = View.Height, h1 = MathController.View.Height,
                h2 = Screen.FromControl(View).Bounds.Height;
            var p = View.PointToScreen(new Point(0, h));
            if (p.Y + h1 > h2) p.Y -= h + h1;
            MathController.ShowDialog(AppController.View, p, Graph, Parent.IndexOf(this));
        }

        private void BtnRemove_Click(object sender, System.EventArgs e) => Parent.RemoveSeries(Index);

        private void FunctionBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var functionName = ((ComboBox)sender).Items[e.Index].ToString();
            e.DrawBackground();
            using (var brush = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(functionName, e.Font, brush, e.Bounds);
        }

        private void FunctionBox_TextChanged(object sender, System.EventArgs e)
        {
            var function = FunctionBox.Text;
            if (!string.IsNullOrWhiteSpace(function) && !Functions.Contains(function))
                Functions[0] = function;
            Parent.LiveUpdate(sender, e);
        }

        #endregion

        #region Private Methods

        private void InitFunctionNames()
        {
            Functions.Clear();
            Functions.Add(string.Empty);
            Functions.AddRange(Utility.FunctionNames.Select(f => $"{f}(x)").ToArray());
        }

        #endregion
    }
}
