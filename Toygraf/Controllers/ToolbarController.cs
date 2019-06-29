namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Views;

    internal class ToolbarController
    {
        #region Internal Interface

        internal ToolbarController(GraphController graphController)
        {
            GraphController = graphController;
            GraphForm.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            GraphForm.ViewToolbar.Click += ViewToolbarHide_Click;
        }

        #endregion

        #region Private Properties

        private readonly GraphController GraphController;
        private GraphForm GraphForm => GraphController.GraphForm;
        private ToolStrip Toolbar => GraphForm.Toolbar;

        #endregion

        #region Private Event Handlers

        private void ViewMenu_DropDownOpening(object sender, EventArgs e) =>
            GraphForm.ViewToolbar.Checked = Toolbar.Visible;

        private void ViewToolbarHide_Click(object sender, EventArgs e) =>
            Toolbar.Visible = !Toolbar.Visible;

        #endregion
    }
}
