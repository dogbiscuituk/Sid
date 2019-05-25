namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class GraphicsController
    {
        #region Internal Interface

        internal GraphicsController(AppController parent)
        {
            Parent = parent;
            View = parent.View.PictureBox;
            ClockController = new ClockController(this);
            AdjustPictureBox();
        }

        internal readonly AppController Parent;
        internal AppForm AppForm { get => Parent.View; }
        internal ClockController ClockController;

        internal PictureBox View
        {
            get => _view;
            set
            {
                _view = value;
                View.MouseDown += View_MouseDown;
                View.MouseLeave += View_MouseLeave;
                View.MouseMove += View_MouseMove;
                View.MouseUp += View_MouseUp;
                View.MouseWheel += View_MouseWheel;
                View.Paint += View_Paint;
                View.Parent.Resize += ParentView_Resize;
                View.Resize += View_Resize;
            }
        }

        internal void AdjustPictureBox() => View.Bounds = View.Parent.ClientRectangle;
        internal void InvalidateView() => View.Invalidate();

        #endregion

        #region Private Properties

        private PictureBox _view;
        private CommandProcessor CommandController { get => Parent.CommandProcessor; }
        private Graph Graph => Parent.Graph;
        private Point DragFrom, MouseDownAt;
        private bool Dragging;

        #endregion

        #region Private Event Handlers

        private void ParentView_Resize(object sender, System.EventArgs e) => AdjustPictureBox();

        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Dragging = true;
                    View.Cursor = Cursors.Hand;
                    MouseDownAt = e.Location;
                    DragFrom = View.Location;
                    break;
                case MouseButtons.Middle: // Click wheel
                    CommandController.ZoomReset();
                    break;
                case MouseButtons.Right:
                    break;
            }
        }

        private void View_MouseLeave(object sender, EventArgs e) => Parent.UpdateMouseCoordinates(PointF.Empty);

        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
                View.Location = new Point(
                    View.Left - MouseDownAt.X + e.X,
                    View.Top - MouseDownAt.Y + e.Y);
            else
                Parent.UpdateMouseCoordinates(ClientToGraph(e.Location));
        }

        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                PointF p = ClientToGraph(DragFrom), q = ClientToGraph(View.Location);
                CommandController.ScrollBy(p.X - q.X, p.Y - q.Y);
                AdjustPictureBox();
                View.Cursor = Cursors.Default;
                Dragging = false;
            }
        }

        private void View_MouseWheel(object sender, MouseEventArgs e) =>
            CommandController.Zoom((float)Math.Pow(e.Delta > 0 ? 10.0 / 11.0 : 11.0 / 10.0,
                Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta)));

        private void View_Paint(object sender, PaintEventArgs e)
        {
            var r = View.ClientRectangle;
            ClockController.BeforeDraw();
            Graph.Draw(e.Graphics, r, ClockController.VirtualSecondsElapsed);
            ClockController.AfterDraw();
        }

        private void View_Resize(object sender, EventArgs e)
        {
            var w = View.Width;
            if (w != 0)
                Graph.Viewport.SetRatio(View.Height / w);
            InvalidateView();
        }

        #endregion

        #region Private Methods

        private PointF ClientToGraph(Point p) => Graph.ClientToGraph(p, View.ClientRectangle);
        private Point GraphToClient(PointF p) => Graph.GraphToClient(p, View.ClientRectangle);
        private Point GraphToScreen(PointF p) => View.PointToScreen(GraphToClient(p));
        private PointF ScreenToGraph(Point p) => ClientToGraph(View.PointToClient(p));

        #endregion
    }
}
