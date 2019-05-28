namespace ToyGraf.Controllers
{
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Views;

    internal class ToolbarController
    {
        #region Internal Interface

        internal ToolbarController(GraphController parent)
        {
            Parent = parent;
            View.ViewToolbar.DropDownOpening += ViewToolbar_DropDownOpening;
            View.ViewToolbarBottom.Click += DockToolbarBottom;
            View.ViewToolbarLeft.Click += DockToolbarLeft;
            View.ViewToolbarRight.Click += DockToolbarRight;
            View.ViewToolbarTop.Click += DockToolbarTop;
            View.ViewToolbarHide.Click += ViewToolbarHide_Click;
        }

        #endregion

        #region Private Properties

        private GraphController Parent;
        private GraphForm View => Parent.View;
        private ToolStrip Toolbar => View.Toolbar;

        private const AnchorStyles
            TopRight = AnchorStyles.Top | AnchorStyles.Right,
            BottomLeft = AnchorStyles.Bottom | AnchorStyles.Left,
            BottomRight = AnchorStyles.Bottom | AnchorStyles.Right;

        #endregion

        #region Private Event Handlers

        private void DockToolbarBottom(object sender, System.EventArgs e) => Toolbar.Dock = DockStyle.Bottom;
        private void DockToolbarLeft(object sender, System.EventArgs e) => Toolbar.Dock = DockStyle.Left;
        private void DockToolbarRight(object sender, System.EventArgs e) => Toolbar.Dock = DockStyle.Right;
        private void DockToolbarTop(object sender, System.EventArgs e) => Toolbar.Dock = DockStyle.Top;

        private void ViewToolbar_DropDownOpening(object sender, System.EventArgs e)
        {
            View.ViewToolbarBottom.Checked = Toolbar.Dock == DockStyle.Bottom;
            View.ViewToolbarLeft.Checked = Toolbar.Dock == DockStyle.Left;
            View.ViewToolbarRight.Checked = Toolbar.Dock == DockStyle.Right;
            View.ViewToolbarTop.Checked = Toolbar.Dock == DockStyle.Top;
            View.ViewToolbarHide.Checked = !Toolbar.Visible;
        }

        private void ViewToolbarHide_Click(object sender, System.EventArgs e)
        {
            Toolbar.Visible = !Toolbar.Visible;
        }

        #endregion
    }
}
