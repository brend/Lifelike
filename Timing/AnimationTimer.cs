using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lifelike.Timing
{
    public class AnimationTimer
    {
        public event EventHandler Tick;

        public double Progress { get; private set; }

        private readonly Timer _timer = new Timer { Interval = 10 };
        private Stopwatch _stopwatch = new Stopwatch();
        private readonly Timing _timingFunction;

        public AnimationTimer(Timing timingFunction)
        {
            _timingFunction = timingFunction;
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
            Progress = _timingFunction.Apply(_stopwatch.Elapsed);
            if (Progress >= 1)
            {
                Progress = 1;
                _timer.Stop();
                _stopwatch.Stop();
            }
            OnTick();
        }

        protected virtual void OnTick() => Tick?.Invoke(this, EventArgs.Empty);

        public bool IsCompleted => Progress >= 0.999;
    }
}
