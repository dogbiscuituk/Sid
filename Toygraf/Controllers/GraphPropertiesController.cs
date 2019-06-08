namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class GraphPropertiesController
    {
        #region Internal Interface

        internal GraphPropertiesController(GraphController graphController)
        {
            GraphController = graphController;
            ColourController = new ColourController();
            View = new GraphPropertiesDialog();
            ElementsController = new ElementsController(this);
            InitEnumControls();
        }

        internal GraphPropertiesDialog View
        {
            get => _view;
            set
            {
                _view = value;
                ColourController.AddControls(View.cbAxisColour, View.cbReticleColour, View.cbPenColour,
                    View.cbLimitColour, View.cbPaperColour, View.cbFillColour);
                View.cbPlotType.SelectedValueChanged += PlotTypeChanged;
                View.cbInterpolation.SelectedValueChanged += InterpolationChanged;
                View.cbDomainGraphWidth.CheckedChanged += DomainGraphWidthChanged;
                View.rbDegrees.CheckedChanged += DomainPolarDegreesChanged;
                View.seDomainMinCartesian.ValueChanged += DomainMinCartesianChanged;
                View.seDomainMaxCartesian.ValueChanged += DomainMaxCartesianChanged;
                View.seDomainMinPolar.ValueChanged += DomainMinPolarChanged;
                View.seDomainMaxPolar.ValueChanged += DomainMaxPolarChanged;
                View.cbAxisColour.SelectedValueChanged += AxisColourChanged;
                View.cbFillColour.SelectedValueChanged += FillColourChanged;
                View.seFillTransparency.ValueChanged += FillTransparencyChanged;
                View.cbLimitColour.SelectedValueChanged += LimitColourChanged;
                View.cbPaperColour.SelectedValueChanged += PaperColourChanged;
                View.sePaperTransparency.ValueChanged += PaperTransparencyChanged;
                View.cbPenColour.SelectedValueChanged += PenColourChanged;
                View.cbReticleColour.SelectedValueChanged += ReticleColourChanged;
                View.cbOptimization.SelectedValueChanged += OptimizationChanged;
                View.cbStepCount.SelectedValueChanged += StepCountChanged;
                View.btnClose.Click += BtnClose_Click;
                View.FormClosing += View_FormClosing;
            }
        }

        internal GraphController GraphController
        {
            get => _GraphController;
            set
            {
                if (GraphController != null)
                    GraphController.PropertyChanged -= GraphController_PropertyChanged;
                _GraphController = value;
                if (GraphController != null)
                    GraphController.PropertyChanged += GraphController_PropertyChanged;
            }
        }

        internal Graph Graph { get => Model.Graph; }

        internal void Show(IWin32Window owner)
        {
            if (!View.Visible)
            {
                GraphRead();
                View.Show(owner);
            }
            else
                View.BringToFront();
        }

        #endregion

        #region Private Properties

        private GraphPropertiesDialog _view;
        private GraphController _GraphController;
        private ColourController ColourController;
        private CommandProcessor CommandProcessor { get => GraphController.CommandProcessor; }
        private ElementsController ElementsController;
        private CheckedListBox.ObjectCollection ElementItems { get => View.ElementCheckboxes.Items; }
        private Model Model { get => GraphController.Model; }
        private bool Loading;

        #endregion

        #region Private Event Handlers

        private void GraphController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => GraphRead();
        private void BtnClose_Click(object sender, EventArgs e) => View.Hide();

        private void AxisColourChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.AxisColour = ColourController.GetColour(View.cbAxisColour);
        }

        private void DomainGraphWidthChanged(object sender, EventArgs e)
        {
            var domainGraphWidth = View.cbDomainGraphWidth.Checked;
            View.seDomainMinCartesian.Enabled = View.seDomainMaxCartesian.Enabled = !domainGraphWidth;
            if (!Loading)
                CommandProcessor.DomainGraphWidth = domainGraphWidth;
        }

        private void DomainMaxCartesianChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.DomainMaxCartesian = (float)View.seDomainMaxCartesian.Value;
        }

        private void DomainMaxPolarChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.DomainMaxPolar = (float)View.seDomainMaxPolar.Value;
        }

        private void DomainMinCartesianChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.DomainMinCartesian = (float)View.seDomainMinCartesian.Value;
        }

        private void DomainMinPolarChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.DomainMinPolar = (float)View.seDomainMinPolar.Value;
        }

        private void DomainPolarDegreesChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.DomainPolarDegrees = View.rbDegrees.Checked;
        }

        private void FillColourChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.FillColour1 = ColourController.GetColour(View.cbFillColour);
        }

        private void FillTransparencyChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.FillTransparencyPercent = (int)View.seFillTransparency.Value;
        }

        private void InterpolationChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.Interpolation = (Interpolation)View.cbInterpolation.SelectedIndex;
        }

        private void LimitColourChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.LimitColour = ColourController.GetColour(View.cbLimitColour);
        }

        private void OptimizationChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.Optimization = (Optimization)View.cbOptimization.SelectedIndex;
        }

        private void PaperColourChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.PaperColour = ColourController.GetColour(View.cbPaperColour);
        }

        private void PaperTransparencyChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.PaperTransparencyPercent = (int)View.sePaperTransparency.Value;
        }

        private void PenColourChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.PenColour = ColourController.GetColour(View.cbPenColour);
        }

        private void PlotTypeChanged(object sender, EventArgs e)
        {
            var polar = (PlotType)View.cbPlotType.SelectedIndex == PlotType.Polar;
            ElementItems[1] = polar ? "Radial reticle" : "Horizontal reticle";
            ElementItems[5] = polar ? "Circular reticle" : "Vertical reticle";
            View.cbDomainGraphWidth.Visible = View.seDomainMinCartesian.Visible =
                View.seDomainMaxCartesian.Visible = !polar;
            View.seDomainMinPolar.Visible = View.seDomainMaxPolar.Visible =
                View.rbDegrees.Visible = View.rbRadians.Visible = polar;
            if (!Loading)
                CommandProcessor.PlotType = (PlotType)View.cbPlotType.SelectedIndex;
        }

        private void ReticleColourChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.ReticleColour = ColourController.GetColour(View.cbReticleColour);
        }

        private void StepCountChanged(object sender, EventArgs e)
        {
            if (!Loading)
                CommandProcessor.StepCount = int.Parse(View.cbStepCount.Text);
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            e.Cancel = true;
            View.Hide();
        }

        #endregion

        #region Private Methods

        private void GraphRead()
        {
            // Disable events while reading
            Loading = true;
            // Plot Type & Approximation Type
            View.cbPlotType.SelectedIndex = (int)Graph.PlotType;
            View.cbInterpolation.SelectedIndex = (int)Graph.Interpolation;
            // Domain
            View.cbDomainGraphWidth.Checked = Graph.DomainGraphWidth;
            View.seDomainMinCartesian.Value = (decimal)Graph.DomainMinCartesian;
            View.seDomainMaxCartesian.Value = (decimal)Graph.DomainMaxCartesian;
            View.rbDegrees.Checked = Graph.DomainPolarDegrees;
            View.rbRadians.Checked = !Graph.DomainPolarDegrees;
            View.seDomainMinPolar.Value = (decimal)Graph.DomainMinPolar;
            View.seDomainMaxPolar.Value = (decimal)Graph.DomainMaxPolar;
            // Elements
            ElementsController.ElementsRead();
            // Reticle Colours
            ColourController.SetColour(View.cbAxisColour, Graph.AxisColour);
            ColourController.SetColour(View.cbReticleColour, Graph.ReticleColour);
            ColourController.SetColour(View.cbPenColour, Graph.PenColour);
            ColourController.SetColour(View.cbLimitColour, Graph.LimitColour);
            // Fill Colours
            ColourController.SetColour(View.cbPaperColour, Graph.PaperColour);
            View.sePaperTransparency.Value = Graph.PaperTransparencyPercent;
            ColourController.SetColour(View.cbFillColour, Graph.FillColour1);
            View.seFillTransparency.Value = Graph.FillTransparencyPercent;
            // Quality
            View.cbOptimization.SelectedIndex = (int)Graph.Optimization;
            View.cbStepCount.Text = Graph.StepCount.ToString();
            // Done.
            Loading = false;
        }

        private void InitEnumControls()
        {
            View.cbOptimization.Items.PopulateWithDescriptions(typeof(Optimization));
            View.cbPlotType.Items.PopulateWithDescriptions(typeof(PlotType));
            View.cbInterpolation.Items.PopulateWithDescriptions(typeof(Interpolation));
        }

        #endregion
    }
}
