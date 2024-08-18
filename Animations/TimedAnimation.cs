using System;
using Lifelike.Timing;

namespace Lifelike.Animations
{
    public abstract class TimedAnimation : IAnimation
    {
        public event EventHandler Completed;

        private readonly ITimer _timer;
        private readonly TimeSpan _duration;
        private TimeSpan _elapsed;
        private readonly Easing _timingFunction;
        private double _progress = 0;

        public AnimationRepetition Repetition { get; set; } = AnimationRepetition.None;

        public AnimationDirection Direction { get; set; } = AnimationDirection.Forward;

        protected TimedAnimation(
            ITimer timer,
            TimeSpan duration, 
            Easing timingFunction)
        {
            _timer = timer;
            _duration = duration;
            _timingFunction = timingFunction;
            _timer.Tick += TimerTick;
        }

        protected virtual void OnCompleted() => Completed?.Invoke(this, EventArgs.Empty);

        public virtual void Start() => _timer.Start();

        public void Pause() => _timer.Stop();

        public void Resume() => _timer.Start();

        public void Reset()
        {
            Pause();
            _progress = 0;
        }

        protected abstract void Update();

        public double Progress => _progress;

        public bool IsComplete => _elapsed >= _duration;

        private void TimerTick(object sender, EventArgs e)
        {
            _elapsed = (sender as AnimationTimer).Elapsed;
            _progress = _timingFunction.Apply(_duration, _elapsed);
            if (_progress > 1)
                _progress = 1;

            Update();

            if (IsComplete)
            {
                switch (Repetition)
                {
                    case AnimationRepetition.Loop:
                        Reset();
                        break;
                    case AnimationRepetition.Reverse:
                        Direction = Direction.Reverse();
                        break;
                    case AnimationRepetition.None:
                        _timer.Stop();
                        OnCompleted();
                        break;
                }
            }
        }
    }
}