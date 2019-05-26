namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class GraphPropertiesController
    {
        #region Internal Interface

        internal GraphPropertiesController(AppController parent)
        {
            Parent = parent;
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

        internal AppController Parent
        {
            get => _appController;
            set
            {
                if (Parent != null)
                    Parent.PropertyChanged -= AppController_PropertyChanged;
                _appController = value;
                if (Parent != null)
                    Parent.PropertyChanged += AppController_PropertyChanged;
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
        private AppController _appController;
        private ColourController ColourController = new ColourController();
        private CommandProcessor CommandController { get => Parent.CommandProcessor; }
        private ElementsController ElementsController;
        private CheckedListBox.ObjectCollection ElementItems { get => View.ElementCheckboxes.Items; }
        private Model Model { get => Parent.Model; }

        private bool Loading;

        #endregion

        #region Private Event Handlers

        private void AppController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => GraphRead();
        private void BtnClose_Click(object sender, EventArgs e) => View.Hide();

        private void AxisColourChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var axisColour = ColourController.GetColour(View.cbAxisColour);
            if (Graph.AxisColour.ToArgb() != axisColour.ToArgb())
                CommandController.Run(new GraphAxisColourCommand(axisColour));
        }

        private void DomainGraphWidthChanged(object sender, EventArgs e)
        {
            var domainGraphWidth = View.cbDomainGraphWidth.Checked;
            View.seDomainMinCartesian.Enabled = View.seDomainMaxCartesian.Enabled = !domainGraphWidth;
            if (Loading) return;
            if (Graph.DomainGraphWidth != domainGraphWidth)
                CommandController.Run(new GraphDomainGraphWidthCommand(domainGraphWidth));
        }

        private void DomainMaxCartesianChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var domainMaxCartesian = (float)View.seDomainMaxCartesian.Value;
            if (Graph.DomainMaxCartesian != domainMaxCartesian)
                CommandController.Run(new GraphDomainMaxCartesianCommand(domainMaxCartesian));
        }

        private void DomainMaxPolarChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var domainMaxPolar = (float)View.seDomainMaxPolar.Value;
            if (Graph.DomainMaxPolar != domainMaxPolar)
                CommandController.Run(new GraphDomainMaxPolarCommand(domainMaxPolar));
        }

        private void DomainMinCartesianChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var domainMinCartesian = (float)View.seDomainMinCartesian.Value;
            if (Graph.DomainMinCartesian != domainMinCartesian)
                CommandController.Run(new GraphDomainMinCartesianCommand(domainMinCartesian));
        }

        private void DomainMinPolarChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var domainMinPolar = (float)View.seDomainMinPolar.Value;
            if (Graph.DomainMinPolar != domainMinPolar)
                CommandController.Run(new GraphDomainMinPolarCommand(domainMinPolar));
        }

        private void DomainPolarDegreesChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var domainPolarDegrees = View.rbDegrees.Checked;
            if (Graph.DomainPolarDegrees != domainPolarDegrees)
                CommandController.Run(new GraphDomainPolarDegreesCommand(domainPolarDegrees));
        }

        private void FillColourChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var fillColour = ColourController.GetColour(View.cbFillColour);
            if (Graph.FillColour.ToArgb() != fillColour.ToArgb())
                CommandController.Run(new GraphFillColourCommand(fillColour));
        }

        private void FillTransparencyChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var fillTransparencyPercent = (int)View.seFillTransparency.Value;
            if (Graph.FillTransparencyPercent != fillTransparencyPercent)
                CommandController.Run(new GraphFillTransparencyPercentCommand(fillTransparencyPercent));
        }

        private void InterpolationChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var interpolation = (Interpolation)View.cbInterpolation.SelectedIndex;
            if (Graph.Interpolation != interpolation)
                CommandController.Run(new GraphInterpolationCommand(interpolation));
        }

        private void LimitColourChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var limitColour = ColourController.GetColour(View.cbLimitColour);
            if (Graph.LimitColour.ToArgb() != limitColour.ToArgb())
                CommandController.Run(new GraphLimitColourCommand(limitColour));
        }

        private void OptimizationChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var optimization = (Optimization)View.cbOptimization.SelectedIndex;
            if (Graph.Optimization != optimization)
                CommandController.Run(new GraphOptimizationCommand(optimization));
        }

        private void PaperColourChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var paperColour = ColourController.GetColour(View.cbPaperColour);
            if (Graph.PaperColour.ToArgb() != paperColour.ToArgb())
                CommandController.Run(new GraphPaperColourCommand(paperColour));
        }

        private void PaperTransparencyChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var paperTransparencyPercent = (int)View.sePaperTransparency.Value;
            if (Graph.PaperTransparencyPercent != paperTransparencyPercent)
                CommandController.Run(new GraphPaperTransparencyPercentCommand(paperTransparencyPercent));
        }

        private void PenColourChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var penColour = ColourController.GetColour(View.cbPenColour);
            if (Graph.PenColour.ToArgb() != penColour.ToArgb())
                CommandController.Run(new GraphPenColourCommand(penColour));
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
            if (Loading) return;
            var plotType = (PlotType)View.cbPlotType.SelectedIndex;
            if (Graph.PlotType != plotType)
                CommandController.Run(new GraphPlotTypeCommand(plotType));
        }

        private void ReticleColourChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var reticleColour = ColourController.GetColour(View.cbReticleColour);
            if (Graph.ReticleColour.ToArgb() != reticleColour.ToArgb())
                CommandController.Run(new GraphReticleColourCommand(reticleColour));
        }

        private void StepCountChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var stepCount = int.Parse(View.cbStepCount.Text);
            if (Graph.StepCount != stepCount)
                CommandController.Run(new GraphStepCountCommand(stepCount));
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
            ColourController.SetColour(View.cbFillColour, Graph.FillColour);
            View.seFillTransparency.Value = Graph.FillTransparencyPercent;
            // Quality
            View.cbOptimization.SelectedIndex = (int)Graph.Optimization;
            View.cbStepCount.Text = Graph.StepCount.ToString();
            // Done.
            Loading = false;
        }

        private void InitEnumControls()
        {
            View.cbOptimization.Items.PopulateWith(typeof(Optimization));
            View.cbPlotType.Items.PopulateWith(typeof(PlotType));
            View.cbInterpolation.Items.PopulateWith(typeof(Interpolation));
        }

        #endregion
    }
}
