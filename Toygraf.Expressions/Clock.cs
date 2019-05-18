namespace ToyGraf.Expressions
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
                Enabled = false
            };
            Timer.Elapsed += Timer_Elapsed;
        }

        #region Fields

        private bool _running;
        private DateTime _startedAt;
        private TimeSpan _realTimeElapsed, _virtualTimeElapsed;
        private int _suspendCount;
        private Timer Timer;
        private double _virtualTimeFactor = 1;

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
                        var elapsed = now - _startedAt;
                        _realTimeElapsed += elapsed;
                        _virtualTimeElapsed += GetVirtualIncrement(now);
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

        private TimeSpan GetVirtualIncrement(DateTime now) =>
            TimeSpan.FromSeconds((now - _startedAt).TotalSeconds * VirtualTimeFactor);

        public double RealSecondsElapsed => RealTimeElapsed.TotalSeconds;
        public double VirtualSecondsElapsed => VirtualTimeElapsed.TotalSeconds;

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

        public TimeSpan RealTimeElapsed => Running
            ? _realTimeElapsed + (DateTime.Now - _startedAt)
            : _realTimeElapsed;

        public TimeSpan VirtualTimeElapsed => Running
            ? _virtualTimeElapsed + GetVirtualIncrement(DateTime.Now)
            : _virtualTimeElapsed;

        public double VirtualTimeFactor
        {
            get => _virtualTimeFactor;
            set
            {
                if (VirtualTimeFactor != value)
                {
                    var running = Running;
                    Stop();
                    _virtualTimeFactor = value;
                    if (running)
                        Start();
                }
            }
        }

        #endregion

        #region Methods

        public void Reset()
        {
            Running = false;
            _realTimeElapsed = TimeSpan.Zero;
            _virtualTimeElapsed = TimeSpan.Zero;
            _virtualTimeFactor = 1;
        }

        public void Resume()
        {
            if (_suspendCount > 0 && --_suspendCount == 0)
                Running = true;
        }

        public void Start()
        {
            _suspendCount = 0;
            Running = true;
        }

        public void Stop()
        {
            _suspendCount = 0;
            Running = false;
        }

        public void Suspend()
        {
            _suspendCount++;
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
