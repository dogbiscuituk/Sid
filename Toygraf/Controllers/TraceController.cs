namespace ToyGraf.Controllers
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class TraceController
    {
        #region Internal Interface

        internal TraceController(LegendController legendController, Trace trace)
        {
            LegendController = legendController;
            Trace = trace;
            ColourController = new ColourController();
            View = new TraceView();
            InitFunctionNames();
        }

        internal TraceView View
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
                GraphController.PropertyChanged += GraphController_PropertyChanged;
            }
        }

        internal Trace Trace { get; set; }

        internal bool TraceVisible
        {
            get => View.cbVisible.Checked;
            set
            {
                View.cbVisible.Checked = value;
                if (!Updating)
                    CommandProcessor[Index].Visible = value;
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

        internal void BeforeRemove() => GraphController.PropertyChanged -= GraphController_PropertyChanged;

        #endregion

        #region Private Properties

        private TraceView _view;
        private GraphController GraphController { get => LegendController.GraphController; }
        private LegendController LegendController;
        private ColourController ColourController;
        private CommandProcessor CommandProcessor { get => GraphController.CommandProcessor; }
        private TracePropertiesController TracePropertiesController { get => GraphController.TracePropertiesController; }
        private KeyboardController KeyboardController { get => TracePropertiesController.KeyboardController; }
        private int Index { get => LegendController.IndexOf(this); }
        private ComboBox FunctionBox { get => View.cbFunction; }
        private ComboBox.ObjectCollection Functions { get => FunctionBox.Items; }
        private Graph Graph { get => LegendController.GraphController.Graph; }
        private bool Updating => UpdatingThis || LegendController.Loading;
        private int FormulaSelStart, FormulaSelLength;
        private bool UpdatingThis;

        #endregion

        #region Private Event Handlers

        private void BtnDetails_Click(object sender, System.EventArgs e)
        {
            var index = LegendController.IndexOf(this);
            if (!TracePropertiesController.View.Visible)
            {
                int h = View.Height, h1 = KeyboardController.View.Height,
                    h2 = Screen.FromControl(View).Bounds.Height;
                var p = View.PointToScreen(new Point(0, h));
                if (p.Y + h1 > h2) p.Y -= h + h1;
                TracePropertiesController.Show(GraphController.View, p, index);
            }
            else
                TracePropertiesController.Trace = Graph.Traces[index];
        }

        private void BtnRemove_Click(object sender, System.EventArgs e) => LegendController.RemoveTrace(Index);

        private void CbFillColour_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].FillColour1 = FillColour;
        }

        private void CbPenColour_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].PenColour = PenColour;
        }

        private void CbVisible_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].Visible = TraceVisible;
        }

        private void FunctionBox_TextChanged(object sender, System.EventArgs e)
        {
            View.ToolTip.SetToolTip(FunctionBox, FunctionBox.Text);
            if (!string.IsNullOrWhiteSpace(Formula) && !Functions.Contains(Formula))
                Functions[0] = Formula;
            if (!Updating && LegendController.Validate())
                CommandProcessor[Index].Formula = Formula;
        }

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!UpdatingThis)
            {
                var match = Regex.Match(e.PropertyName, $@"Model.Graph.Traces\[{Index}\]\.(\w+)");
                if (match.Success)
                {
                    UpdatingThis = true;
                    switch (match.Groups[1].Value)
                    {
                        case "Visible":
                            TraceVisible = Trace.Visible;
                            break;
                        case "Formula":
                            SaveFormulaSelection();
                            Formula = Trace.Formula;
                            LoadFormulaSelection();
                            break;
                        case "PenColour":
                            PenColour = Trace.PenColour;
                            break;
                        case "FillColour1":
                            FillColour = Trace.FillColour1;
                            break;
                        case "FillTransparencyPercent":
                            FillTransparencyPercent = Trace.FillTransparencyPercent;
                            break;
                    }
                    UpdatingThis = false;
                }
            }
        }

        private void SeTransparency_ValueChanged(object sender, System.EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].FillTransparencyPercent = FillTransparencyPercent;
        }

        #endregion

        #region Private Methods

        private void InitFunctionNames()
        {
            Functions.Clear();
            Functions.Add(string.Empty);
            Functions.AddRange(Utility.FunctionNames.Select(f => $"{f}(x)").ToArray());
        }

        private void LoadFormulaSelection()
        {
            FunctionBox.SelectionStart = FormulaSelStart;
            FunctionBox.SelectionLength = FormulaSelLength;
        }

        private void SaveFormulaSelection()
        {
            FormulaSelStart = FunctionBox.SelectionStart;
            FormulaSelLength = FunctionBox.SelectionLength;
        }

        #endregion
    }
}
