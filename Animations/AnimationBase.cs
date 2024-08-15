using System;
using System.Windows.Forms;

namespace Lifelike.Animations
{
    public abstract class AnimationBase
    {
        private readonly AnimationTimer _timer;
        public Control Control { get; }

        protected AnimationBase(Control control, TimeSpan duration, Func<double, double> timingFunction = null)
        {
            Control = control;
            timingFunction = timingFunction ?? TimingFunctions.EaseOut;
            _timer = new AnimationTimer(duration, timingFunction);
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