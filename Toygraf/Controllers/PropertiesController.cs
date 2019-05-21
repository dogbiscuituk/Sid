namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    public class PropertiesController
    {
        public PropertiesController(AppController parent)
        {
            Parent = parent;
            View = new PropertiesDialog();
            InitEnumControls();
        }

        #region Properties

        private AppController _appController;
        private AppController Parent
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

        private void AppController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            GraphRead();
        }

        private CommandController CommandController { get => Parent.CommandController; }
        private CheckedListBox ClbElements { get => View.ElementCheckboxes; }
        private Model Model { get => Parent.Model; }
        private Graph Graph { get => Model.Graph; }
        private CheckedListBox.ObjectCollection ElementItems { get => View.ElementCheckboxes.Items; }
        private ComboBox.ObjectCollection Optimizations { get => View.cbOptimization.Items; }
        private ComboBox.ObjectCollection PlotTypes { get => View.cbPlotType.Items; }
        private ComboBox.ObjectCollection Interpolations { get => View.cbInterpolation.Items; }
        private bool Loading, Updating;

        private PropertiesDialog _view;
        private PropertiesDialog View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    View.cbPlotType.SelectedValueChanged -= PlotTypeChanged;
                    View.cbInterpolation.SelectedValueChanged -= LiveUpdate;
                    View.cbDomainGraphWidth.CheckedChanged -= DomainGraphWidthChanged;
                    View.rbDegrees.CheckedChanged -= LiveUpdate;
                    View.rbRadians.CheckedChanged -= LiveUpdate;
                    View.seDomainMinCartesian.ValueChanged -= LiveUpdate;
                    View.seDomainMaxCartesian.ValueChanged -= LiveUpdate;
                    View.seDomainMinPolar.ValueChanged -= LiveUpdate;
                    View.seDomainMaxPolar.ValueChanged -= LiveUpdate;
                    ClbElements.ItemCheck -= ClbElements_ItemCheck;
                    ColourController.Clear();
                    View.cbOptimization.SelectedValueChanged -= PlotTypeChanged;
                    View.cbStepCount.SelectedValueChanged -= LiveUpdate;
                    View.btnClose.Click -= BtnClose_Click;
                    View.FormClosing -= View_FormClosing;
                }
                _view = value;
                if (View != null)
                {
                    View.cbPlotType.SelectedValueChanged += PlotTypeChanged;
                    View.cbInterpolation.SelectedValueChanged += LiveUpdate;
                    View.cbDomainGraphWidth.CheckedChanged += DomainGraphWidthChanged;
                    View.rbDegrees.CheckedChanged += LiveUpdate;
                    View.rbRadians.CheckedChanged += LiveUpdate;
                    View.seDomainMinCartesian.ValueChanged += LiveUpdate;
                    View.seDomainMaxCartesian.ValueChanged += LiveUpdate;
                    View.seDomainMinPolar.ValueChanged += LiveUpdate;
                    View.seDomainMaxPolar.ValueChanged += LiveUpdate;
                    ClbElements.ItemCheck += ClbElements_ItemCheck;
                    AddColourControls(View.cbAxisColour, View.cbReticleColour, View.cbPenColour,
                        View.cbLimitColour, View.cbPaperColour, View.cbFillColour);
                    View.cbOptimization.SelectedValueChanged += PlotTypeChanged;
                    View.cbStepCount.SelectedValueChanged += LiveUpdate;
                    View.btnClose.Click += BtnClose_Click;
                    View.FormClosing += View_FormClosing;
                }
            }
        }

        private ColourController ColourController = new ColourController();

        #endregion

        #region Show/Hide

        public void Show(IWin32Window owner)
        {
            if (!View.Visible)
            {
                GraphRead();
                View.Show(owner);
            }
            else
                View.BringToFront();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            View.Hide();
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            e.Cancel = true;
            View.Hide();
        }

        #endregion

        #region Colours

        private void AddColourControls(params ComboBox[] controls)
        {
            ColourController.AddControls(controls);
            foreach (var control in controls)
                control.SelectedValueChanged += Control_SelectedValueChanged;
        }

        private void Control_SelectedValueChanged(object sender, EventArgs e) => LiveUpdate(sender, e);

        private void Clear()
        {
            foreach (var control in ColourController.Controls)
                control.SelectedValueChanged -= Control_SelectedValueChanged;
            ColourController.Clear();
        }

        #endregion

        #region Elements

        private void ClbElements_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (Updating || e.NewValue == e.CurrentValue)
                return;
            Updating = true;
            int index = e.Index, x = index % 4, y = x + 4, both = y + 4;
            if (index == x)
                SetState(both, e.NewValue == ClbElements.GetItemCheckState(y) ?
                    e.NewValue : CheckState.Indeterminate);
            else if (index == y)
                SetState(both, e.NewValue == ClbElements.GetItemCheckState(x) ?
                    e.NewValue : CheckState.Indeterminate);
            else
            {
                SetState(x, e.NewValue);
                SetState(y, e.NewValue);
            }
            Updating = false;
            if (!Loading)
                View.BeginInvoke((MethodInvoker)(() => LiveUpdate(sender, e)));
        }

        private Elements[] GetElementValues() => (Elements[])Enum.GetValues(typeof(Elements));

        private int ControlToEnum(int index) => index < 11 ? 3 * index % 11 : 11;
        private CheckState GetState(int index) => ClbElements.GetItemCheckState(index);
        private void SetState(int index, CheckState state) => ClbElements.SetItemCheckState(index, state);

        private void ElementsRead()
        {
            var values = GetElementValues();
            var graphElements = Graph.Elements;
            for (var index = 0; index < ClbElements.Items.Count; index++)
            {
                var value = values[ControlToEnum(index)];
                var graphValue = graphElements & value;
                var state = graphValue == value ? CheckState.Checked
                    : graphValue == 0 ? CheckState.Unchecked
                    : CheckState.Indeterminate;
                SetState(index, state);
            }
        }

        private void ElementsWrite()
        {
            var values = GetElementValues();
            Elements elements = 0;
            for (var index = 0; index < ClbElements.Items.Count; index++)
                if (GetState(index) == CheckState.Checked)
                    elements |= values[ControlToEnum(index)];
            if (Graph.Elements != elements)
                CommandController.Run(new GraphElementsCommand(elements));
        }

        private void InitEnumControls()
        {
            Optimizations.Clear();
            Optimizations.AddRange(typeof(Optimization).GetDescriptions());
            PlotTypes.Clear();
            PlotTypes.AddRange(typeof(PlotType).GetDescriptions());
            Interpolations.Clear();
            Interpolations.AddRange(typeof(Interpolation).GetDescriptions());
        }

        #endregion

        #region Graph Read/Write

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
            ElementsRead();
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

        private void GraphWrite()
        {
            //
            // Plot Type & Approximation Type
            //
            var plotType = (PlotType)View.cbPlotType.SelectedIndex;
            if (Graph.PlotType != plotType)
                CommandController.Run(new GraphPlotTypeCommand(plotType));
            var interpolation = (Interpolation)View.cbInterpolation.SelectedIndex;
            if (Graph.Interpolation != interpolation)
                CommandController.Run(new GraphInterpolationCommand(interpolation));
            //
            // Domain
            //
            var domainGraphWidth = View.cbDomainGraphWidth.Checked;
            if (Graph.DomainGraphWidth != domainGraphWidth)
                CommandController.Run(new GraphDomainGraphWidthCommand(domainGraphWidth));
            var domainMinCartesian = (float)View.seDomainMinCartesian.Value;
            if (Graph.DomainMinCartesian != domainMinCartesian)
                CommandController.Run(new GraphDomainMinCartesianCommand(domainMinCartesian));
            var domainMaxCartesian = (float)View.seDomainMaxCartesian.Value;
            if (Graph.DomainMaxCartesian != domainMaxCartesian)
                CommandController.Run(new GraphDomainMaxCartesianCommand(domainMaxCartesian));
            var domainPolarDegrees = View.rbDegrees.Checked;
            if (Graph.DomainPolarDegrees != domainPolarDegrees)
                CommandController.Run(new GraphDomainPolarDegreesCommand(domainPolarDegrees));
            var domainMinPolar = (float)View.seDomainMinPolar.Value;
            if (Graph.DomainMinPolar != domainMinPolar)
                CommandController.Run(new GraphDomainMinPolarCommand(domainMinPolar));
            var domainMaxPolar = (float)View.seDomainMaxPolar.Value;
            if (Graph.DomainMaxPolar != domainMaxPolar)
                CommandController.Run(new GraphDomainMaxPolarCommand(domainMaxPolar));
            //
            // Elements
            //
            ElementsWrite();
            //
            // Reticle Colours
            //
            var axisColour = ColourController.GetColour(View.cbAxisColour);
            if (Graph.AxisColour.ToArgb() != axisColour.ToArgb())
                CommandController.Run(new GraphAxisColourCommand(axisColour));
            var reticleColour = ColourController.GetColour(View.cbReticleColour);
            if (Graph.ReticleColour.ToArgb() != reticleColour.ToArgb())
                CommandController.Run(new GraphReticleColourCommand(reticleColour));
            var penColour = ColourController.GetColour(View.cbPenColour);
            if (Graph.PenColour.ToArgb() != penColour.ToArgb())
                CommandController.Run(new GraphPenColourCommand(penColour));
            var limitColour = ColourController.GetColour(View.cbLimitColour);
            if (Graph.LimitColour.ToArgb() != limitColour.ToArgb())
                CommandController.Run(new GraphLimitColourCommand(limitColour));
            //
            // Fill Colours
            //
            var paperColour = ColourController.GetColour(View.cbPaperColour);
            if (Graph.PaperColour.ToArgb() != paperColour.ToArgb())
                CommandController.Run(new GraphPaperColourCommand(paperColour));
            var paperTransparencyPercent = (int)View.sePaperTransparency.Value;
            if (Graph.PaperTransparencyPercent != paperTransparencyPercent)
                CommandController.Run(new GraphPaperTransparencyPercentCommand(paperTransparencyPercent));
            var fillColour = ColourController.GetColour(View.cbFillColour);
            if (Graph.FillColour.ToArgb() != fillColour.ToArgb())
                CommandController.Run(new GraphFillColourCommand(fillColour));
            var fillTransparencyPercent = (int)View.seFillTransparency.Value;
            if (Graph.FillTransparencyPercent != fillTransparencyPercent)
                CommandController.Run(new GraphFillTransparencyPercentCommand(fillTransparencyPercent));
            //
            // Quality
            //
            var optimization = (Optimization)View.cbOptimization.SelectedIndex;
            if (Graph.Optimization != optimization)
                CommandController.Run(new GraphOptimizationCommand(optimization));
            var stepCount = int.Parse(View.cbStepCount.Text);
            if (Graph.StepCount != stepCount)
                CommandController.Run(new GraphStepCountCommand(stepCount));
        }

        public void LiveUpdate(object sender, EventArgs e)
        {
            if (!Loading)
                GraphWrite();
        }

        public void PlotTypeChanged(object sender, EventArgs e)
        {
            var polar = (PlotType)View.cbPlotType.SelectedIndex == PlotType.Polar;
            ElementItems[1] = polar ? "Radial reticle" : "Horizontal reticle";
            ElementItems[5] = polar ? "Circular reticle" : "Vertical reticle";
            View.cbDomainGraphWidth.Visible = View.seDomainMinCartesian.Visible =
                View.seDomainMaxCartesian.Visible = !polar;
            View.seDomainMinPolar.Visible = View.seDomainMaxPolar.Visible = 
                View.rbDegrees.Visible = View.rbRadians.Visible = polar;
            LiveUpdate(sender, e);
        }

        private void DomainGraphWidthChanged(object sender, EventArgs e)
        {
            View.seDomainMinCartesian.Enabled = View.seDomainMaxCartesian.Enabled =
                !View.cbDomainGraphWidth.Checked;
            LiveUpdate(sender, e);
        }

        #endregion
    }
}
