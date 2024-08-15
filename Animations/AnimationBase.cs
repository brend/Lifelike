using System;
using System.Windows.Forms;

namespace Lifelike.Animations
{
    public abstract class AnimationBase
    {
        public static Timing DefaultTimingFunction = 
            TimingFunctions.EaseInOut(TimeSpan.FromSeconds(1));

        private readonly AnimationTimer _timer;
        public Control Control { get; }

        protected AnimationBase(Control control, Timing? timingFunction = null)
        {
            Control = control;
            _timer = new AnimationTimer(timingFunction ?? DefaultTimingFunction);
            _timer.Tick += TimerTick;
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        protected abstract void Update();

        public double Progress => _timer.Progress;

        private void TimerTick(object sender, EventArgs e)
        {
            Update();
            if (_timer.Progress >= 1)
            {
                _timer.Stop();
            }
        }
    }
}