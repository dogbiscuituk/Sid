namespace Sid.Expressions
{
    using System;
    using System.ComponentModel;
    using System.Timers;

    public class Clock
    {
        public Clock()
        {
            Timer = new Timer
            {
                AutoReset = true,
                Interval = 50,
                Enabled = true
            };
            Timer.Elapsed += Timer_Elapsed;
        }

        #region Fields

        private bool _running;
        private DateTime _startedAt;
        private TimeSpan _timeElapsed;
        private Timer Timer;

        #endregion

        #region Properties

        public bool Running
        {
            get => _running;
            set
            {
                if (Running != value)
                {
                    var now = DateTime.Now;
                    if (Running)
                    {
                        Timer.Enabled = false;
                        _timeElapsed += now - _startedAt;
                    }
                    _running = value;
                    if (Running)
                    {
                        _startedAt = now;
                        Timer.Enabled = true;
                    }
                }
            }
        }

        public double SecondsElapsed => TimeElapsed.TotalSeconds;

        public ISynchronizeInvoke Sync
        {
            get => Timer.SynchronizingObject;
            set => Timer.SynchronizingObject = value;
        }

        public double Tick_ms
        {
            get => Timer.Interval;
            set => Timer.Interval = value;
        }

        public TimeSpan TimeElapsed =>
            Running ? _timeElapsed + (DateTime.Now - _startedAt) : _timeElapsed;

        #endregion

        #region Methods

        public void Reset()
        {
            Stop();
            _timeElapsed = TimeSpan.Zero;
        }

        public void Start()
        {
            Running = true;
        }

        public void Stop()
        {
            Running = false;
        }

        #endregion

        #region Events

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Tick;

        #endregion
    }
}
