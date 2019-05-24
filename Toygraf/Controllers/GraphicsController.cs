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
            AppForm.TimeDecelerate.Click += TimeDecelerate_Click;
            AppForm.tbDecelerate.Click += TimeDecelerate_Click;
            AppForm.TimeReverse.Click += TimeReverse_Click;
            AppForm.tbReverse.Click += TimeReverse_Click;
            AppForm.TimeStop.Click += TimeStop_Click;
            AppForm.tbStop.Click += TimeStop_Click;
            AppForm.TimePause.Click += TimePause_Click;
            AppForm.tbPause.Click += TimePause_Click;
            AppForm.TimeForward.Click += TimeForward_Click;
            AppForm.tbForward.Click += TimeForward_Click;
            AppForm.TimeAccelerate.Click += TimeAccelerate_Click;
            AppForm.tbAccelerate.Click += TimeAccelerate_Click;
            AdjustPictureBox();
        }

        internal Clock Clock;
        internal bool ClockRunning => Clock.Running;

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
                Clock = new Clock { Sync = View };
                Clock.Tick += Clock_Tick;
            }
        }

        internal void AdjustPictureBox() => View.Bounds = View.Parent.ClientRectangle;
        internal void InvalidateView() => View.Invalidate();

        #endregion

        #region Private Properties

        private PictureBox _view;
        private readonly AppController Parent;
        private AppForm AppForm { get => Parent.View; }
        private CommandProcessor CommandController { get => Parent.CommandProcessor; }
        private Graph Graph => Parent.Graph;
        private Point DragFrom, MouseDownAt;
        private bool Dragging;

        #endregion

        #region Private Event Handlers

        private void Clock_Tick(object sender, EventArgs e) => UpdateTimeDisplay();
        private void ParentView_Resize(object sender, System.EventArgs e) => AdjustPictureBox();
        private void TimeDecelerate_Click(object sender, EventArgs e) => ClockDecelerate();
        private void TimeReverse_Click(object sender, EventArgs e) => ClockReverse();
        private void TimeStop_Click(object sender, EventArgs e) => ClockStop();
        private void TimePause_Click(object sender, EventArgs e) => ClockPause();
        private void TimeForward_Click(object sender, EventArgs e) => ClockForward();
        private void TimeAccelerate_Click(object sender, EventArgs e) => ClockAccelerate();

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

        private void ClockDecelerate()
        {
            Clock.Decelerate();
            UpdateTimeFactor();
        }

        private void ClockReverse()
        {
            Clock.VirtualTimeFactor = -Math.Abs(Clock.VirtualTimeFactor);
            Clock.Start();
            UpdateTimeFactor();
        }

        private void ClockStop()
        {
            Clock.Reset();
            UpdateTimeDisplay();
            UpdateTimeFactor();
        }

        private void ClockPause()
        {
            Clock.Stop();
            UpdateTimeFactor();
        }

        private void ClockForward()
        {
            Clock.VirtualTimeFactor = Math.Abs(Clock.VirtualTimeFactor);
            Clock.Start();
            UpdateTimeFactor();
        }

        private void ClockAccelerate()
        {
            Clock.Accelerate();
            UpdateTimeFactor();
        }

        private void UpdateTimeDisplay()
        {
            Clock.UpdateFPS();
            AppForm.Tlabel.Text = string.Format("t={0:f1}", Clock.VirtualSecondsElapsed);
            AppForm.FPSlabel.Text = string.Format("fps={0:f1}", Clock.FramesPerSecond);
            InvalidateView();
        }

        private void UpdateTimeControls()
        {

        }

        private void UpdateTimeFactor()
        {
            string speed;
            var factor = Clock.VirtualTimeFactor;
            if (factor == 0)
                speed = "time × 0";
            else
            {
                var divide = Math.Abs(factor) < 1;
                if (divide)
                    factor = 1 / factor;
                speed = divide ? $"time ÷ {factor}" : $"time × {factor}";
            }
            Parent.View.SpeedLabel.Text = speed;
        }

        #endregion
    }
}
