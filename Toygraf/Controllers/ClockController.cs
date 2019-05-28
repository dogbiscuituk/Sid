namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Models;
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
            UpdateTimeControls();
        }

        internal Clock Clock;
        internal double VirtualSecondsElapsed => Clock.VirtualSecondsElapsed;
        internal bool ClockRunning => Clock.Running;

        internal double VirtualTimeFactor
        {
            get => Clock.VirtualTimeFactor;
            set => Clock.VirtualTimeFactor = value;
        }

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

        internal void UpdateTimeControls()
        {
            if (ClockRunning && !UsesTime)
                ClockStop();
            AppForm.TimeAccelerate.Enabled = AppForm.tbAccelerate.Enabled = CanAccelerate;
            AppForm.TimeDecelerate.Enabled = AppForm.tbDecelerate.Enabled = CanDecelerate;
            AppForm.TimeForward.Enabled = AppForm.tbForward.Enabled = CanStart;
            AppForm.TimePause.Enabled = AppForm.tbPause.Enabled = CanPause;
            AppForm.TimeReverse.Enabled = AppForm.tbReverse.Enabled = CanReverse;
            AppForm.TimeStop.Enabled = AppForm.tbStop.Enabled = CanStop;
            AppForm.SpeedLabel.Enabled = AppForm.Tlabel.Enabled = AppForm.FPSlabel.Enabled = UsesTime;
            UpdateTimeFactor();
        }

        #endregion

        #region Private Properties

        private GraphicsController Parent;
        private AppController AppController => Parent.Parent;
        private AppForm AppForm { get => Parent.AppForm; }
        private System.Diagnostics.Stopwatch Stopwatch;
        private bool EpilepsyWarningAcknowledged;

        private bool CanAccelerate => UsesTime && VirtualTimeFactor < +32;
        private bool CanDecelerate => UsesTime && VirtualTimeFactor > -32;
        private bool CanPause => UsesTime && Clock.Running;
        private bool CanReverse => UsesTime && (!Clock.Running || VirtualTimeFactor > 0);
        private bool CanStart => UsesTime && (!Clock.Running || VirtualTimeFactor < 0);
        private bool CanStop => UsesTime && Clock.Running;

        private bool UsesTime => AppController.Graph.UsesTime;

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

        private void ClockAccelerate()
        {
            Clock.Accelerate();
            UpdateTimeControls();
        }

        private void ClockDecelerate()
        {
            Clock.Decelerate();
            UpdateTimeControls();
        }

        private void ClockForward()
        {
            VirtualTimeFactor = Math.Abs(VirtualTimeFactor);
            ClockStart();
            UpdateTimeControls();
        }

        private void ClockPause()
        {
            Clock.Stop();
            UpdateTimeControls();
        }

        private void ClockReverse()
        {
            VirtualTimeFactor = -Math.Abs(VirtualTimeFactor);
            ClockStart();
            UpdateTimeControls();
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

        private void ClockStop()
        {
            Clock.Reset();
            UpdateTimeDisplay();
            UpdateTimeControls();
        }

        private void UpdateTimeDisplay()
        {
            Clock.UpdateFPS();
            AppForm.Tlabel.Text = string.Format("t={0:f1}", VirtualSecondsElapsed);
            AppForm.FPSlabel.Text = string.Format("fps={0:f1}", Clock.FramesPerSecond);
            Parent.InvalidateView();
        }

        private void UpdateTimeFactor()
        {
            string speed;
            var factor = VirtualTimeFactor;
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
