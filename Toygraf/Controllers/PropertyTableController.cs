namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Views;

    internal class PropertyTableController
    {
        internal PropertyTableController(GraphController graphController)
        {
            GraphController = graphController;
            PropertyTable = graphController.View.PropertyTable;
            Form.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            Form.ViewPropertyTable.Click += TogglePropertyTable;
        }

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
        private readonly PropertyGrid PropertyTable;
    }
}
