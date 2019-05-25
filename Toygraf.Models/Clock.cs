namespace ToyGraf.Models
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Timers;

    public class Clock
    {
        public Clock()
        {
            Timer = new Timer
            {
                AutoReset = true,
                Interval = 100,
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

        public double NextInterval = 100;
        private double[] Ticks = new double[64];
        private int TickCount, TickIndex;

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

        public double RealSecondsElapsed => RealTimeElapsed.TotalSeconds;
        public double VirtualSecondsElapsed => VirtualTimeElapsed.TotalSeconds;
        public double FramesPerSecond;
        public const int LimitFactor = 32;

        public ISynchronizeInvoke Sync
        {
            get => Timer.SynchronizingObject;
            set => Timer.SynchronizingObject = value;
        }

        public double Interval
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
            TickCount = 0;
            TickIndex = 0;
            Array.ForEach(Ticks, p => p = 0);
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

        public void UpdateFPS()
        {
            Ticks[TickIndex = (TickIndex + 1) % Ticks.Length] = RealSecondsElapsed;
            if (TickCount < Ticks.Length - 1) TickCount++;
            var fps = 0.0;
            if (TickCount > 1)
            {
                var ticks = Ticks.Take(TickCount);
                fps = TickCount / (ticks.Max() - ticks.Min());
            }
            FramesPerSecond = fps;
        }

        public void Accelerate() => VirtualTimeFactor = +Adjust(+VirtualTimeFactor);
        public void Decelerate() => VirtualTimeFactor = -Adjust(-VirtualTimeFactor);

        private static double Adjust(double factor)
        {
            switch (factor)
            {
                case double f when f < -1.0 / LimitFactor:
                    return factor / 2;
                case -1.0 / LimitFactor:
                    return 0;
                case 0:
                    return 1.0 / LimitFactor;
                case double f when f < LimitFactor:
                    return factor * 2;
                default:
                    return LimitFactor;
            }
        }

        private TimeSpan GetVirtualIncrement(DateTime now) =>
            TimeSpan.FromSeconds((now - _startedAt).TotalSeconds * VirtualTimeFactor);

        #endregion

        #region Events

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Tick?.Invoke(this, EventArgs.Empty);
            Interval = NextInterval;
        }

        public event EventHandler<EventArgs> Tick;

        #endregion
    }
}
