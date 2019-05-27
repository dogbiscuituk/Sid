namespace ToyGraf.Controllers
{
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Views;

    internal class SeriesController
    {
        #region Internal Interface

        internal SeriesController(LegendController parent, Series series)
        {
            Parent = parent;
            Series = series;
            ColourController = new ColourController();
            View = new SeriesView();
            InitFunctionNames();
        }

        internal SeriesView View
        {
            get => _view;
            set
            {
                _view = value;
                View.cbVisible.CheckedChanged += CbVisible_CheckedChanged;
                FunctionBox.TextChanged += FunctionBox_TextChanged;
                View.cbPenColour.SelectedValueChanged += CbPenColour_SelectedValueChanged;
                View.cbFillColour.SelectedValueChanged += CbFillColour_SelectedValueChanged;
                View.seTransparency.ValueChanged += SeTransparency_ValueChanged;
                View.btnDetails.Click += BtnDetails_Click;
                View.btnRemove.Click += BtnRemove_Click;
                ColourController.AddControls(View.cbPenColour, View.cbFillColour);
            }
        }

        internal Series Series { get; set; }

        internal bool TraceVisible
        {
            get => View.cbVisible.Checked;
            set
            {
                View.cbVisible.Checked = value;
                if (Series.Visible != value)
                    CommandProcessor.Run(new SeriesVisibleCommand(Index, value));
            }
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
        private ColourController ColourController;
        private CommandProcessor CommandProcessor { get => AppController.CommandProcessor; }
        private SeriesPropertiesController SeriesPropertiesController { get => AppController.SeriesPropertiesController; }
        private KeyboardController KeyboardController { get => SeriesPropertiesController.KeyboardController; }
        private int Index { get => Parent.IndexOf(this); }
        private ComboBox FunctionBox { get => View.cbFunction; }
        private ComboBox.ObjectCollection Functions { get => FunctionBox.Items; }
        private Graph Graph { get => Parent.Parent.Graph; }

        #endregion

        #region Private Event Handlers

        private void BtnDetails_Click(object sender, System.EventArgs e)
        {
            int h = View.Height, h1 = KeyboardController.View.Height,
                h2 = Screen.FromControl(View).Bounds.Height;
            var p = View.PointToScreen(new Point(0, h));
            if (p.Y + h1 > h2) p.Y -= h + h1;
            SeriesPropertiesController.ShowDialog(AppController.View, p, Graph, Parent.IndexOf(this));
        }

        private void BtnRemove_Click(object sender, System.EventArgs e) => Parent.RemoveSeries(Index);

        private void CbFillColour_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (!Parent.Loading)
                CommandProcessor.Run(new SeriesFillColour1Command(Index, FillColour));
        }

        private void CbPenColour_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (!Parent.Loading)
                CommandProcessor.Run(new SeriesPenColourCommand(Index, PenColour));
        }

        private void CbVisible_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!Parent.Loading)
                CommandProcessor.Run(new SeriesVisibleCommand(Index, TraceVisible));
        }

        private void FunctionBox_TextChanged(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Formula) && !Functions.Contains(Formula))
                Functions[0] = Formula;
            if (!Parent.Loading && Parent.Validate())
                CommandProcessor.Run(new SeriesFormulaCommand(Index, Formula));
        }

        private void SeTransparency_ValueChanged(object sender, System.EventArgs e)
        {
            if (!Parent.Loading)
                CommandProcessor.Run(new SeriesFillTransparencyPercentCommand(Index, FillTransparencyPercent));
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
