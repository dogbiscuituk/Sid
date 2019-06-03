namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Commands;
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
        }

        private GraphProxy GraphProxy => GraphController.GraphProxy;

        private void PropertyTable_FormClosed(object s, FormClosedEventArgs e) { }

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
