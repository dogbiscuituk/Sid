namespace Sid.Expressions
{
    using System;
    using System.Timers;

    public class Clock
    {
        public Clock()
        {
            _timer = new Timer
            {
                AutoReset = true,
                Interval = 25,
                Enabled = true
            };
            _timer.Elapsed += _timer_Elapsed;
            Start();
        }

        #region Fields

        private bool _running;
        private DateTime _startedAt;
        private TimeSpan _timeElapsed;
        private Timer _timer;

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
                        _timer.Stop();
                        _timeElapsed += now - _startedAt;
                    }
                    _running = value;
                    if (Running)
                    {
                        _startedAt = now;
                        _timer.Start();
                    }
                }
            }
        }

        public double SecondsElapsed => TimeElapsed.TotalSeconds;

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

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Tick;

        #endregion
    }
}
