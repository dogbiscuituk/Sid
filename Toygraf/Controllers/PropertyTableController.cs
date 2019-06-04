namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Commands;
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
            var toolStrip = FindToolStrip(PropertyTable);
            HidePropertyPagesButton(toolStrip);
            AddCloseButton(toolStrip);
            GraphController.PropertyChanged += GraphController_PropertyChanged;
        }

        internal bool PropertyTableVisible
        {
            get => !Form.SplitContainer.Panel2Collapsed;
            set => Form.SplitContainer.Panel2Collapsed = !value;
        }

        internal static void HidePropertyPagesButton(PropertyGrid propertyGrid) =>
            HidePropertyPagesButton(FindToolStrip(propertyGrid));

        #endregion

        #region Private Properties

        private GraphProxy GraphProxy => GraphController.GraphProxy;
        private readonly GraphController GraphController;
        private GraphForm Form => GraphController.View;
        private readonly PropertyGrid PropertyTable;

        #endregion

        #region Private Event Handlers

        private void CloseButton_Click(object sender, EventArgs e) => PropertyTableVisible = false;
        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged(e.PropertyName);
        private void TogglePropertyTable(object sender, EventArgs e) => PropertyTableVisible = !PropertyTableVisible;
        private void ViewMenu_DropDownOpening(object sender, EventArgs e) => Form.ViewPropertyTable.Checked = PropertyTableVisible;

        #endregion

        #region Private Methods

        private void AddCloseButton(ToolStrip toolStrip)
        {
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

        private static void HidePropertyPagesButton(ToolStrip toolStrip)
        {
            toolStrip.Items[4].Visible = false; // Property Pages
            toolStrip.Items[3].Visible = false; // Separator
        }

        private static ToolStrip FindToolStrip(PropertyGrid propertyGrid) =>
            propertyGrid.Controls.OfType<ToolStrip>().FirstOrDefault();

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
