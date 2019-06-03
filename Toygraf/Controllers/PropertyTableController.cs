namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Controls;
    using ToyGraf.Views;

    internal class PropertyTableController
    {
        internal PropertyTableController(GraphController graphController)
        {
            GraphController = graphController;
            PropertyTable = graphController.View.PropertyTable;
            Form.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            Form.ViewPropertyTable.Click += TogglePropertyTable;
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

        private void CloseButton_Click(object sender, EventArgs e) => PropertyTableVisible = false;

        internal bool PropertyTableVisible
        {
            get => !Form.SplitContainer.Panel2Collapsed;
            set => Form.SplitContainer.Panel2Collapsed = !value;
        }

        private void ViewMenu_DropDownOpening(object sender, System.EventArgs e) => Form.ViewPropertyTable.Checked = PropertyTableVisible;
        private void TogglePropertyTable(object sender, EventArgs e) => PropertyTableVisible = !PropertyTableVisible;

        private GraphProxy GraphProxy => GraphController.GraphProxy;
        private readonly GraphController GraphController;
        private GraphForm Form => GraphController.View;
        private readonly TgPropertyGrid PropertyTable;
    }
}
