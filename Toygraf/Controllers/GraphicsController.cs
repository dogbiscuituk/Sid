namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class GraphicsController
    {
        #region Internal Interface

        internal GraphicsController(GraphController graphController, bool doubleBuffered)
        {
            GraphController = graphController;
            DoubleBuffered = doubleBuffered;
            PictureBox = GraphForm.PictureBox;
            ClockController = new ClockController(this);
            GraphForm.tbTool.ButtonClick += TbTool_ButtonClick;
            GraphForm.tbToolArrow.Click += TbToolArrow_Click;
            GraphForm.tbToolCross.Click += TbToolCross_Click;
            GraphForm.tbToolHand.Click += TbToolHand_Click;
            AdjustPictureBox();
        }

        internal readonly GraphController GraphController;
        internal GraphForm GraphForm { get => GraphController.View; }
        internal ClockController ClockController;

        internal PictureBox PictureBox
        {
            get => _PictureBox;
            set
            {
                _PictureBox = value;
                PictureBox.MouseDown += View_MouseDown;
                PictureBox.MouseLeave += View_MouseLeave;
                PictureBox.MouseMove += View_MouseMove;
                PictureBox.MouseUp += View_MouseUp;
                PictureBox.MouseWheel += View_MouseWheel;
                PictureBox.Paint += View_Paint;
                PictureBox.Parent.Resize += ParentView_Resize;
                PictureBox.Resize += View_Resize;
            }
        }

        internal void AdjustPictureBox() => PictureBox.Bounds = PictureBox.Parent.ClientRectangle;
        internal void InvalidateView() => PictureBox.Invalidate();

        #endregion

        #region Private Properties

        private PictureBox _PictureBox;
        private CommandProcessor CommandController { get => GraphController.CommandProcessor; }
        private Graph Graph => GraphController.Graph;
        private Point DragFrom, MouseDownAt;
        private Tool SelectedTool;
        private bool DoubleBuffered, Dragging;

        #endregion

        #region Private Event Handlers

        private void ParentView_Resize(object sender, System.EventArgs e) => AdjustPictureBox();

        private void TbTool_ButtonClick(object sender, EventArgs e) => SelectNextTool();
        private void TbToolArrow_Click(object sender, EventArgs e) => SelectTool(Tool.Arrow);
        private void TbToolCross_Click(object sender, EventArgs e) => SelectTool(Tool.Cross);
        private void TbToolHand_Click(object sender, EventArgs e) => SelectTool(Tool.Hand);

        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Dragging = true;
                    PictureBox.Cursor = Cursors.Hand;
                    MouseDownAt = e.Location;
                    DragFrom = PictureBox.Location;
                    break;
                case MouseButtons.Middle: // Click wheel
                    CommandController.ZoomReset();
                    break;
                case MouseButtons.Right:
                    break;
            }
        }

        private void View_MouseLeave(object sender, EventArgs e) => GraphController.UpdateMouseCoordinates(PointF.Empty);

        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
                PictureBox.Location = new Point(
                    PictureBox.Left - MouseDownAt.X + e.X,
                    PictureBox.Top - MouseDownAt.Y + e.Y);
            else
                GraphController.UpdateMouseCoordinates(ClientToGraph(e.Location));
        }

        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                PointF p = ClientToGraph(DragFrom), q = ClientToGraph(PictureBox.Location);
                CommandController.ScrollBy(p.X - q.X, p.Y - q.Y);
                AdjustPictureBox();
                PictureBox.Cursor = Cursors.Default;
                Dragging = false;
            }
        }

        private void View_MouseWheel(object sender, MouseEventArgs e) =>
            CommandController.Zoom((float)Math.Pow(e.Delta > 0 ? 10.0 / 11.0 : 11.0 / 10.0,
                Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta)));

        private void View_Paint(object sender, PaintEventArgs e)
        {
            var r = PictureBox.ClientRectangle;
            ClockController.BeforeDraw();
            var t = ClockController.VirtualSecondsElapsed;
            if (DoubleBuffered)
                using (var bitmap = new Bitmap(r.Width, r.Height, e.Graphics))
                {
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.SetClip(e.Graphics);
                        Graph.Draw(g, r, t);
                    }
                    e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
                }
            else
                Graph.Draw(e.Graphics, r, t);
            ClockController.AfterDraw();
        }

        private void View_Resize(object sender, EventArgs e)
        {
            var w = PictureBox.Width;
            if (w != 0)
                Graph.Viewport.SetRatio(PictureBox.Height / w);
            InvalidateView();
        }

        #endregion

        #region Private Methods

        private PointF ClientToGraph(Point p) => Graph.ClientToGraph(p, PictureBox.ClientRectangle);
        private PointF ScreenToGraph(Point p) => ClientToGraph(PictureBox.PointToClient(p));

        private Point GraphToClient(PointF p) => Graph.GraphToClient(p, PictureBox.ClientRectangle);
        private Point GraphToScreen(PointF p) => PictureBox.PointToScreen(GraphToClient(p));

        private void SelectNextTool() => SelectTool((Tool)((int)(SelectedTool + 1) % 3));

        private void SelectTool(Tool tool)
        {
            SelectedTool = tool;
            PictureBox.Cursor = ToolToCursor(tool);
            GraphForm.tbTool.Image = ToolToImage(tool);
        }

        private static Cursor ToolToCursor(Tool tool)
        {
            switch (tool)
            {
                case Tool.Arrow:
                    return Cursors.Arrow;
                case Tool.Cross:
                    return Cursors.Cross;
                case Tool.Hand:
                    return Cursors.Hand;
                default:
                    return Cursors.Default;
            }
        }

        private static Image ToolToImage(Tool tool)
        {
            switch (tool)
            {
                case Tool.Arrow:
                    return Properties.Resources.PointerHS;
                case Tool.Cross:
                    return Properties.Resources.Cross;
                case Tool.Hand:
                    return Properties.Resources.clock_16xLG;
                default:
                    return Properties.Resources.PointerHS;
            }
        }

        #endregion
    }
}
