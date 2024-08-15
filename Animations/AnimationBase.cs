using System;
using System.IO;
using System.Windows.Forms;
using Lifelike.Timing;

namespace Lifelike.Animations
{
    public abstract class AnimationBase : IAnimation
    {
        public static Easing DefaultTimingFunction = 
            EasingFunctions.EaseInOut(TimeSpan.FromSeconds(1));

        private readonly AnimationTimer _timer;
        public Control Control { get; }

        protected AnimationBase(Control control, Easing? timingFunction = null)
        {
            Control = control;
            _timer = new AnimationTimer(timingFunction ?? DefaultTimingFunction);
            _timer.Tick += TimerTick;
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        public void Pause() => throw new NotImplementedException();

        public void Resume() => throw new NotImplementedException();

        protected abstract void Update();

        public double Progress => _timer.Progress;

        private void TimerTick(object sender, EventArgs e)
        {
            Update();
            if (_timer.IsCompleted)
            {
                _timer.Stop();
            }
        }
    }
}