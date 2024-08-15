using System;
using System.Windows.Forms;

namespace Lifelike
{
    public class AnimationTimer
    {
        public event EventHandler Tick;

        public static TimeSpan DefaultAnimationDuration = TimeSpan.FromMilliseconds(500);

        public TimeSpan Duration { get; }
        public double Progress { get; private set; }
        
        private readonly Timer _timer = new Timer { Interval = 10 };
        private TimeSpan _elapsed = TimeSpan.Zero;
        private readonly Func<double, double> _timingFunction = t => t;

        private DateTime _lastTick = DateTime.Now;

        public AnimationTimer(TimeSpan? duration = null)
        {
            Duration = duration ?? DefaultAnimationDuration;
            _timer.Tick += TimerTick;
        }

        public void Start()
        {
            _elapsed = TimeSpan.Zero;
            _lastTick = DateTime.Now;
             _timer.Start();
        }

        public void Stop() => _timer.Stop();

        private void TimerTick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            _elapsed += now - _lastTick;
            Progress = _timingFunction(Math.Min(1, _elapsed.TotalMilliseconds / Duration.TotalMilliseconds));
            if (Progress >= 1)
            {
                Progress = 1;
                _timer.Stop();
            }
            OnTick();
        }

        protected virtual void OnTick() => Tick?.Invoke(this, EventArgs.Empty);
    }
}