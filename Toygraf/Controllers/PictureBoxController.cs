namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Expressions;

    public class PictureBoxController
    {
        public PictureBoxController(AppController parent)
        {
            Parent = parent;
            View = parent.View.PictureBox;
            AdjustPictureBox();
        }

        public Clock Clock = new Clock();
        private double Tick_ms = 100;
        private double[] Ticks = new double[64];
        private int TickCount, TickIndex;

        private readonly AppController Parent;
        private Point DragFrom, MouseDownAt;
        private bool Dragging;

        private PictureBox _view;
        public PictureBox View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    Clock.Stop();
                    Clock.Tick -= Clock_Tick;
                    View.MouseDown -= View_MouseDown;
                    View.MouseLeave -= View_MouseLeave;
                    View.MouseMove -= View_MouseMove;
                    View.MouseUp -= View_MouseUp;
                    View.MouseWheel -= View_MouseWheel;
                    View.Paint -= View_Paint;
                    View.Parent.Resize -= ParentView_Resize;
                    View.Resize -= View_Resize;
                }
                _view = value;
                if (View != null)
                {
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
        }

        private void ParentView_Resize(object sender, System.EventArgs e) => AdjustPictureBox();

        public void AdjustPictureBox() => View.Bounds = View.Parent.ClientRectangle;

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
                    Parent.ZoomReset();
                    break;
                case MouseButtons.Right:
                    break;
            }
        }

        private void View_MouseLeave(object sender, EventArgs e) =>
            Parent.UpdateMouseCoordinates(PointF.Empty);

        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
                View.Location = new Point(
                    View.Left - MouseDownAt.X + e.X,
                    View.Top - MouseDownAt.Y + e.Y);
            else
                Parent.UpdateMouseCoordinates(ScreenToGraph(e.Location));
        }

        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                PointF p = ScreenToGraph(DragFrom), q = ScreenToGraph(View.Location);
                Parent.ScrollBy(p.X - q.X, p.Y - q.Y);
                AdjustPictureBox();
                View.Cursor = Cursors.Default;
                Dragging = false;
            }
        }

        private void View_MouseWheel(object sender, MouseEventArgs e) =>
            Parent.Zoom((float)Math.Pow(e.Delta > 0 ? 10.0 / 11.0 : 11.0 / 10.0,
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
            Parent.Graph.Draw(e.Graphics, r, Clock.VirtualSecondsElapsed);
            if (stopwatch != null)
            {
                stopwatch.Stop();
                // That Graph.Draw() operation took ms = stopwatch.ElapsedMilliseconds.
                // Add 10% & round up to multiple of 10ms to get the time to next Draw.
                double
                    t = 10 * Math.Ceiling(stopwatch.ElapsedMilliseconds * 0.11),
                    fps = 2000 / (t + Tick_ms);
                Tick_ms = t;
            }
        }

        private void View_Resize(object sender, EventArgs e)
        {
            var w = View.Width;
            if (w != 0)
                Parent.Graph.Viewport.SetRatio(View.Height / w);
            InvalidateView();
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            Clock.Tick_ms = Tick_ms;
            UpdateRealTimeLabels();
        }

        public void ClockReset()
        {
            Clock.Reset();
            TickCount = 0;
            TickIndex = 0;
            Array.ForEach(Ticks, p => p = 0);
            Parent.View.TimeTrackBar.Value = 0;
            Parent.TimerReverse = false;
            UpdateRealTimeLabels();
        }

        public void InvalidateView() => View.Invalidate();
        private PointF ScreenToGraph(Point p) => Parent.Graph.ScreenToGraph(p, View.ClientRectangle);

        private void UpdateRealTimeLabels()
        {
            double
                realSecondsElapsed = Clock.RealSecondsElapsed,
                virtualSecondsElapsed = Clock.VirtualSecondsElapsed;
            Ticks[TickIndex = (TickIndex + 1) % Ticks.Length] = realSecondsElapsed;
            if (TickCount < Ticks.Length - 1) TickCount++;
            var fps = 0.0;
            if (TickCount > 1)
            {
                var ticks = Ticks.Take(TickCount);
                fps = TickCount / (ticks.Max() - ticks.Min());
            }
            Parent.UpdateLabels(virtualSecondsElapsed, fps);
            InvalidateView();
        }

        public void UpdateVirtualTimeFactor()
        {
            var value = Parent.View.TimeTrackBar.Value;
            int factor = (1 << Math.Abs(value)) * Parent.TimerSign;
            Clock.VirtualTimeFactor = value >= 0 ? factor : 1.0 / factor;
            var speed = value >= 0 ? $"time × {factor}" : $"time ÷ {factor}";
            Parent.View.ToolTip.SetToolTip(Parent.View.TimeTrackBar, speed);
            Parent.View.SpeedLabel.Text = speed;
        }
    }
}
