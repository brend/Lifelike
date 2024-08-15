using System;
using System.Diagnostics;
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
        private Stopwatch _stopwatch = new Stopwatch();
        private readonly Func<double, double> _timingFunction = t => t;

        public AnimationTimer(TimeSpan? duration = null, Func<double, double> timingFunction = null)
        {
            _timingFunction = timingFunction ?? TimingFunctions.EaseOut;
            Duration = duration ?? DefaultAnimationDuration;
            _timer.Tick += TimerTick;
        }

        public void Start()
        {
            Progress = 0;
            _stopwatch.Restart();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _stopwatch.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var elapsed = _stopwatch.Elapsed;
            Progress = _timingFunction(Math.Min(1, elapsed.TotalMilliseconds / Duration.TotalMilliseconds));
            if (Progress >= 1)
            {
                Progress = 1;
                _timer.Stop();
                _stopwatch.Stop();
            }
            OnTick();
        }

        protected virtual void OnTick() => Tick?.Invoke(this, EventArgs.Empty);
    }
}
