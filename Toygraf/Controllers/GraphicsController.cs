namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    public class GraphicsController
    {
        #region Public Interface

        public GraphicsController(AppController parent)
        {
            Parent = parent;
            View = parent.View.PictureBox;
            AppForm.TimerMenu.DropDownOpening += TimerMenu_DropDownOpening;
            AppForm.TimerRunPause.Click += TimerRunPause_Click;
            AppForm.TimerReverse.Click += TimerReverse_Click;
            AppForm.TimerReset.Click += TimerReset_Click;
            AppForm.tbTimer.ButtonClick += TimerRunPause_Click;
            AppForm.tbTimer.DropDownOpening += TbTimer_DropDownOpening;
            AppForm.TimeTrackBar.ValueChanged += TimeTrackBar_ValueChanged;
            AdjustPictureBox();
        }

        public Clock Clock;
        public bool ClockRunning => Clock.Running;

        public PictureBox View
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
                Clock = new Clock { Sync = View };
                Clock.Tick += Clock_Tick;
            }
        }

        public void AdjustPictureBox() => View.Bounds = View.Parent.ClientRectangle;
        public void InvalidateView() => View.Invalidate();

        #endregion

        #region Private Properties

        private PictureBox _view;
        private readonly AppController Parent;
        private AppForm AppForm { get => Parent.View; }
        private CommandProcessor CommandController { get => Parent.CommandProcessor; }
        private Graph Graph => Parent.Graph;
        private Point DragFrom, MouseDownAt;
        private bool Dragging;

        private bool TimerReverse
        {
            get => AppForm.TimerReverse.Checked;
            set
            {
                AppForm.TimerReverse.Checked = value;
                UpdateVirtualTimeFactor(value);
            }
        }

        #endregion

        #region Private Event Handlers

        private void TimerMenu_DropDownOpening(object sender, EventArgs e) => AppForm.TimerRunPause.Checked = ClockRunning;
        private void TimerRunPause_Click(object sender, EventArgs e) => ToggleClock();
        private void TimerReverse_Click(object sender, EventArgs e) => TimerReverse = !TimerReverse;
        private void TimerReset_Click(object sender, EventArgs e) => ClockReset();
        private void TimeTrackBar_ValueChanged(object sender, EventArgs e) => UpdateVirtualTimeFactor(TimerReverse);
        private void TbTimer_DropDownOpening(object sender, EventArgs e) => AppForm.TimerMenu.CloneTo(AppForm.tbTimer);
        private void ParentView_Resize(object sender, System.EventArgs e) => AdjustPictureBox();
        private void Clock_Tick(object sender, EventArgs e) => UpdateRealTimeLabels();

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
            System.Diagnostics.Stopwatch stopwatch = null;
            if (Clock.Running)
            {
                stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
            }
            Graph.Draw(e.Graphics, r, Clock.VirtualSecondsElapsed);
            if (stopwatch != null)
            {
                stopwatch.Stop();
                // That Graph.Draw() operation took ms = stopwatch.ElapsedMilliseconds.
                // Add 10% & round up to multiple of 10ms to get the time to next Draw.
                double
                    t = 10 * Math.Ceiling(stopwatch.ElapsedMilliseconds * 0.11),
                    fps = 2000 / (t + Clock.Interval);
                Clock.NextInterval = t;
            }
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
        private void ToggleClock() => Clock.Running = !Clock.Running;

        private void ClockReset()
        {
            Clock.Reset();
            Parent.View.TimeTrackBar.Value = 0;
            TimerReverse = false;
            UpdateRealTimeLabels();
        }

        private void UpdateRealTimeLabels()
        {
            Clock.UpdateFPS();
            AppForm.Tlabel.Text = string.Format("t={0:f1}", Clock.VirtualSecondsElapsed);
            AppForm.FPSlabel.Text = string.Format("fps={0:f1}", Clock.FramesPerSecond);
            InvalidateView();
        }

        private void UpdateVirtualTimeFactor(bool timeReverse)
        {
            var value = Parent.View.TimeTrackBar.Value;
            int factor = (1 << Math.Abs(value)) * (timeReverse ? -1 : +1);
            Clock.VirtualTimeFactor = value >= 0 ? factor : 1.0 / factor;
            var speed = value >= 0 ? $"time × {factor}" : $"time ÷ {factor}";
            Parent.View.ToolTip.SetToolTip(Parent.View.TimeTrackBar, speed);
            Parent.View.SpeedLabel.Text = speed;
        }

        #endregion
    }
}
