namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class PropertyGridController
    {
        internal PropertyGridController(GraphController graphController)
        {
            GraphController = graphController;
            PropertyTable = graphController.View.PropertyTable;
            PropertyTable.MaximumSize = new Size(Properties.Settings.Default.PropertyTable_MaximumWidth, 0);
            GraphForm.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            GraphForm.ViewPropertyTable.Click += ViewPropertyTable_Click;
            PropertyTable.PropertyValueChanged += PropertyTable_PropertyValueChanged;
        }

        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;

        private void PropertyTable_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) =>
            PropertyValueChanged(e.ChangedItem.PropertyDescriptor.Name, e.OldValue);

        private bool PropertyValueChanged(string propertyName, object oldValue)
        {
            switch (propertyName)
            {
                case "AxisColour":
                    return CommandProcessor.SetGraphAxisColour((Color)oldValue, false);
                case "Centre":
                    return CommandProcessor.SetGraphCentre((PointF)oldValue, false);
                case "DomainGraphWidth":
                    return CommandProcessor.SetGraphDomainGraphWidth((bool)oldValue, false);
                case "DomainGraphMaxCartesian":
                    return CommandProcessor.SetGraphDomainMaxCartesian((float)oldValue, false);
                case "DomainGraphMaxPolar":
                    return CommandProcessor.SetGraphDomainMaxPolar((float)oldValue, false);
                case "DomainGraphMinCartesian":
                    return CommandProcessor.SetGraphDomainMinCartesian((float)oldValue, false);
                case "DomainGraphMinPolar":
                    return CommandProcessor.SetGraphDomainMinPolar((float)oldValue, false);
                case "DomainPolarDegrees":
                    return CommandProcessor.SetGraphDomainPolarDegrees((bool)oldValue, false);
                case "Elements":
                    return CommandProcessor.SetGraphElements((Elements)oldValue, false);
                case "FillColour1":
                    return CommandProcessor.SetGraphFillColour1((Color)oldValue, false);
                case "FillColour2":
                    return CommandProcessor.SetGraphFillColour2((Color)oldValue, false);
                case "FillTransparencyPercent":
                    return CommandProcessor.SetGraphFillTransparencyPercent((int)oldValue, false);
                case "Interpolation":
                    return CommandProcessor.SetGraphInterpolation((Interpolation)oldValue, false);
                case "LimitColour":
                    return CommandProcessor.SetGraphLimitColour((Color)oldValue, false);
                case "Optimization":
                    return CommandProcessor.SetGraphOptimization((Optimization)oldValue, false);
                case "PaperColour":
                    return CommandProcessor.SetGraphPaperColour((Color)oldValue, false);
                case "PenColour":
                    return CommandProcessor.SetGraphPenColour((Color)oldValue, false);
                case "PlotType":
                    return CommandProcessor.SetGraphPlotType((PlotType)oldValue, false);
                case "ReticleColour":
                    return CommandProcessor.SetGraphReticleColour((Color)oldValue, false);
                case "StepCount":
                    return CommandProcessor.SetGraphStepCount((int)oldValue, false);
                case "TickStyles":
                    return CommandProcessor.SetGraphTickStyles((TickStyles)oldValue, false);
                case "Title":
                    return CommandProcessor.SetGraphTitle((string)oldValue, false);
                case "Width":
                    return CommandProcessor.SetGraphWidth((float)oldValue, false);
                default:
                    return false;
            }
        }

        private void ViewPropertyTable_Click(object sender, EventArgs e)
        {
            var visible = !PropertyTable.Visible;
            PropertyTable.Visible = GraphForm.Splitter.Visible = visible;
        }

        private void ViewMenu_DropDownOpening(object sender, EventArgs e) =>
            GraphForm.ViewPropertyTable.Checked = PropertyTable.Visible;

        private readonly GraphController GraphController;
        private GraphForm GraphForm => GraphController.View;
        private readonly PropertyGrid PropertyTable;
    }
}
