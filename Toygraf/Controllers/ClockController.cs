namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class ClockController
    {
        #region Internal Interface

        internal ClockController(GraphicsController graphicsController)
        {
            GraphicsController = graphicsController;
            graphForm.TimeDecelerate.Click += TimeDecelerate_Click;
            graphForm.tbDecelerate.Click += TimeDecelerate_Click;
            graphForm.TimeReverse.Click += TimeReverse_Click;
            graphForm.tbReverse.Click += TimeReverse_Click;
            graphForm.TimeStop.Click += TimeStop_Click;
            graphForm.tbStop.Click += TimeStop_Click;
            graphForm.TimePause.Click += TimePause_Click;
            graphForm.tbPause.Click += TimePause_Click;
            graphForm.TimeForward.Click += TimeForward_Click;
            graphForm.tbForward.Click += TimeForward_Click;
            graphForm.TimeAccelerate.Click += TimeAccelerate_Click;
            graphForm.tbAccelerate.Click += TimeAccelerate_Click;
            Clock = new Clock { Sync = GraphicsController.View };
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
            graphForm.TimeAccelerate.Enabled = graphForm.tbAccelerate.Enabled = CanAccelerate;
            graphForm.TimeDecelerate.Enabled = graphForm.tbDecelerate.Enabled = CanDecelerate;
            graphForm.TimeForward.Enabled = graphForm.tbForward.Enabled = CanStart;
            graphForm.TimePause.Enabled = graphForm.tbPause.Enabled = CanPause;
            graphForm.TimeReverse.Enabled = graphForm.tbReverse.Enabled = CanReverse;
            graphForm.TimeStop.Enabled = graphForm.tbStop.Enabled = CanStop;
            graphForm.SpeedLabel.Enabled = graphForm.Tlabel.Enabled = graphForm.FPSlabel.Enabled = UsesTime;
            UpdateTimeFactor();
        }

        #endregion

        #region Private Properties

        internal AppController AppController => GraphController.AppController;
        private GraphicsController GraphicsController;
        private GraphController GraphController => GraphicsController.GraphController;
        private GraphForm graphForm { get => GraphicsController.graphForm; }
        private System.Diagnostics.Stopwatch Stopwatch;

        private bool CanAccelerate => UsesTime && VirtualTimeFactor < +32;
        private bool CanDecelerate => UsesTime && VirtualTimeFactor > -32;
        private bool CanPause => UsesTime && Clock.Running;
        private bool CanReverse => UsesTime && (!Clock.Running || VirtualTimeFactor > 0);
        private bool CanStart => UsesTime && (!Clock.Running || VirtualTimeFactor < 0);
        private bool CanStop => UsesTime && Clock.Running;

        private bool UsesTime => GraphController.Graph.UsesTime;

        private bool EpilepsyWarningAcknowledged
        {
            get => AppController.EpilepsyWarningAcknowledged;
            set => AppController.EpilepsyWarningAcknowledged = value;
        }

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
                    graphForm,
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
            graphForm.Tlabel.Text = string.Format("t={0:f1}", VirtualSecondsElapsed);
            graphForm.FPSlabel.Text = string.Format("fps={0:f1}", Clock.FramesPerSecond);
            GraphicsController.InvalidateView();
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
            graphForm.SpeedLabel.Text = speed;
        }

        #endregion
    }
}
