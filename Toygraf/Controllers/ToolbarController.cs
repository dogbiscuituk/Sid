namespace ToyGraf.Controllers
{
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Views;

    internal class ToolbarController
    {
        #region Internal Interface

        internal ToolbarController(AppController parent)
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

        private AppController Parent;
        private AppForm View => Parent.View;
        private ToolStrip Toolbar => View.Toolbar;
        private TrackBar TimeTrackBar => View.TimeTrackBar;

        private const AnchorStyles
            TopRight = AnchorStyles.Top | AnchorStyles.Right,
            BottomLeft = AnchorStyles.Bottom | AnchorStyles.Left,
            BottomRight = AnchorStyles.Bottom | AnchorStyles.Right;

        #endregion

        #region Private Event Handlers

        private void DockToolbarBottom(object sender, System.EventArgs e) => DockToolbar(DockStyle.Bottom);
        private void DockToolbarLeft(object sender, System.EventArgs e) => DockToolbar(DockStyle.Left);
        private void DockToolbarRight(object sender, System.EventArgs e) => DockToolbar(DockStyle.Right);
        private void DockToolbarTop(object sender, System.EventArgs e) => DockToolbar(DockStyle.Top);

        private void ViewToolbar_DropDownOpening(object sender, System.EventArgs e)
        {
            View.ViewToolbarBottom.Checked =
                TimeTrackBar.Orientation == Orientation.Horizontal && TimeTrackBar.Anchor == BottomRight;
            View.ViewToolbarLeft.Checked = TimeTrackBar.Anchor == BottomLeft;
            View.ViewToolbarRight.Checked =
                TimeTrackBar.Orientation == Orientation.Vertical && TimeTrackBar.Anchor == BottomRight;
            View.ViewToolbarTop.Checked = TimeTrackBar.Anchor == TopRight;
            View.ViewToolbarHide.Checked = !Toolbar.Visible;
        }

        private void ViewToolbarHide_Click(object sender, System.EventArgs e)
        {
            Toolbar.Visible = TimeTrackBar.Visible = !Toolbar.Visible;
            if (Toolbar.Visible)
                DockToolbar(Toolbar.Dock);
        }

        #endregion

        #region Private Methods

        private void DockToolbar(DockStyle dock)
        {
            Toolbar.Dock = dock;
            switch (dock)
            {
                case DockStyle.Bottom:
                    InitTimeTrackBar(Orientation.Horizontal, BottomRight, TickStyle.TopLeft);
                    break;
                case DockStyle.Left:
                    InitTimeTrackBar(Orientation.Vertical, BottomLeft, TickStyle.BottomRight);
                    break;
                case DockStyle.Right:
                    InitTimeTrackBar(Orientation.Vertical, BottomRight, TickStyle.TopLeft);
                    break;
                case DockStyle.Top:
                    InitTimeTrackBar(Orientation.Horizontal, TopRight, TickStyle.BottomRight);
                    break;
            }
        }

        private void InitTimeTrackBar(Orientation orientation, AnchorStyles anchor, TickStyle tick)
        {
            var visible = TimeTrackBar.Visible;
            TimeTrackBar.Visible = false;
            TimeTrackBar.TickStyle = tick;
            TimeTrackBar.Anchor = AnchorStyles.None;
            TimeTrackBar.Orientation = orientation;
            var p = Toolbar.Location + Toolbar.Size - TimeTrackBar.Size;
            TimeTrackBar.Location = new Point(p.X - 4, p.Y);
            TimeTrackBar.Anchor = anchor;
            TimeTrackBar.Visible = visible;
        }

        #endregion
    }
}
