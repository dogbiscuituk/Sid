namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Controls;
    using ToyGraf.Views;

    internal class PropertyTableController
    {
        #region Internal Interface

        internal PropertyTableController(GraphController graphController)
        {
            GraphController = graphController;
            PropertyTable = graphController.View.PropertyTable;
            Form.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            Form.ViewPropertyTable.Click += TogglePropertyTable;
            AddCloseButton();
            GraphController.PropertyChanged += GraphController_PropertyChanged;
        }

        internal bool PropertyTableVisible
        {
            get => !Form.SplitContainer.Panel2Collapsed;
            set => Form.SplitContainer.Panel2Collapsed = !value;
        }

        #endregion

        #region Private Properties

        private GraphProxy GraphProxy => GraphController.GraphProxy;
        private readonly GraphController GraphController;
        private GraphForm Form => GraphController.View;
        private readonly TgPropertyGrid PropertyTable;

        #endregion

        #region Private Event Handlers

        private void CloseButton_Click(object sender, EventArgs e) => PropertyTableVisible = false;
        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged(e.PropertyName);
        private void TogglePropertyTable(object sender, EventArgs e) => PropertyTableVisible = !PropertyTableVisible;
        private void ViewMenu_DropDownOpening(object sender, EventArgs e) => Form.ViewPropertyTable.Checked = PropertyTableVisible;

        #endregion

        #region Private Methods

        private void AddCloseButton()
        {
            var toolStrip = PropertyTable.GetToolStrip();
            toolStrip.Items.RemoveAt(4); // Property Pages
            toolStrip.Items.RemoveAt(3); // Separator
            var closeButton = new ToolStripButton("Close", Properties.Resources.Close)
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                ImageTransparentColor = Color.White,
                ToolTipText = "Hide the Property Table"
            };
            closeButton.Click += CloseButton_Click;
            toolStrip.Items.Add(closeButton);
        }

        private void PropertyChanged(string propertyName)
        {
            // Only Graph properties need cause a PropertyTable.Refresh();
            // Trace properties will be refreshed on the collection fetch.
            if (!propertyName.StartsWith("Model.Graph.Traces["))
                PropertyTable.Refresh();
        }

        #endregion
    }
}
