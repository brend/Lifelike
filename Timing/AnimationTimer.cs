using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lifelike.Timing
{
    public class AnimationTimer : IDisposable
    {
        public event EventHandler Tick;

        public TimeSpan Elapsed => _stopwatch.Elapsed;

        private readonly Timer _timer = new Timer { Interval = 10 };
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public AnimationTimer()
        {
            _timer.Tick += TimerTick;
        }

        public void Start()
        {
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
            OnTick();
        }

        protected virtual void OnTick() => Tick?.Invoke(this, EventArgs.Empty);

        public void Dispose()
        {
            _timer.Dispose();
            _stopwatch.Stop();
        }
    }
}
