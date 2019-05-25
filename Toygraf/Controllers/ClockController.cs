namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Views;

    internal class ClockController
    {
        #region Internal Interface

        internal ClockController(GraphicsController parent)
        {
            Parent = parent;
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
            Clock = new Clock { Sync = Parent.View };
            Clock.Tick += Clock_Tick;
        }

        internal Clock Clock;
        internal double VirtualSecondsElapsed => Clock.VirtualSecondsElapsed;
        internal bool ClockRunning => Clock.Running;

        internal void AfterDraw()
        {
            if (Stopwatch != null)
            {
                Stopwatch.Stop();
                // That Graph.Draw() operation took ms = stopwatch.ElapsedMilliseconds.
                // Add 10% & round up to multiple of 10ms to get the time to next Draw.
                double
                    t = 10 * Math.Ceiling(Stopwatch.ElapsedMilliseconds * 0.11),
                    fps = 2000 / (t + Clock.Interval);
                Clock.NextInterval = t;
                Stopwatch = null;
            }
        }

        internal void BeforeDraw()
        {
            if (Clock.Running)
            {
                Stopwatch = new System.Diagnostics.Stopwatch();
                Stopwatch.Start();
            }
        }

        #endregion

        #region Private Properties

        private GraphicsController Parent;
        private AppForm AppForm { get => Parent.AppForm; }
        private System.Diagnostics.Stopwatch Stopwatch;
        private bool EpilepsyWarningAcknowledged;

        #endregion

        #region Private Event Handlers

        private void Clock_Tick(object sender, EventArgs e) => UpdateTimeDisplay();
        private void TimeDecelerate_Click(object sender, EventArgs e) => ClockDecelerate();
        private void TimeReverse_Click(object sender, EventArgs e) => ClockReverse();
        private void TimeStop_Click(object sender, EventArgs e) => ClockStop();
        private void TimePause_Click(object sender, EventArgs e) => ClockPause();
        private void TimeForward_Click(object sender, EventArgs e) => ClockForward();
        private void TimeAccelerate_Click(object sender, EventArgs e) => ClockAccelerate();

        #endregion

        #region Private Methods

        private void ClockDecelerate()
        {
            Clock.Decelerate();
            UpdateTimeFactor();
        }

        private void ClockReverse()
        {
            Clock.VirtualTimeFactor = -Math.Abs(Clock.VirtualTimeFactor);
            ClockStart();
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
            ClockStart();
            UpdateTimeFactor();
        }

        private void ClockAccelerate()
        {
            Clock.Accelerate();
            UpdateTimeFactor();
        }

        private void ClockStart()
        {
            if (!EpilepsyWarningAcknowledged)
                EpilepsyWarningAcknowledged = MessageBox.Show(
                    AppForm,
                    "The Time function can be used to create fast flashing images which may cause discomfort, " +
                    "and have the potential to trigger seizures in people with photosensitive epilepsy. " +
                    "User discretion is advised.\n\nDo you wish to proceed?",
                    "Warning - Photosensitive Epilepsy",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes;
            if (EpilepsyWarningAcknowledged)
                Clock.Start();
        }

        private void UpdateTimeDisplay()
        {
            Clock.UpdateFPS();
            AppForm.Tlabel.Text = string.Format("t={0:f1}", VirtualSecondsElapsed);
            AppForm.FPSlabel.Text = string.Format("fps={0:f1}", Clock.FramesPerSecond);
            Parent.InvalidateView();
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
            AppForm.SpeedLabel.Text = speed;
        }

        #endregion
    }
}
