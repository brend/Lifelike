using System;
using System.Collections.Generic;

namespace Lifelike.Animations
{
    public class SequenceAnimation : IAnimation
    {
        public event EventHandler Completed;

        public List<IAnimation> Animations { get; private set; }

        private Queue<IAnimation> _animationQueue;

        public SequenceAnimation(IEnumerable<IAnimation> animations)
        {
            Animations = new List<IAnimation>(animations);
        }

        public void Start()
        {
            _animationQueue = new Queue<IAnimation>(Animations);
            _animationQueue.Peek().Completed += ElementCompleted;
            _animationQueue.Peek().Start();
        }

        private void ElementCompleted(object sender, System.EventArgs e)
        {
            var completedElement = _animationQueue.Dequeue();
            completedElement.Completed -= ElementCompleted;

            if (_animationQueue.Count > 0)
            {
                _animationQueue.Peek().Completed += ElementCompleted;
                _animationQueue.Peek().Start();
            }
            else
            {
                Completed?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            if (_animationQueue != null)
            {
                foreach (var animation in _animationQueue)
                    animation.Stop();
            }
        }

        public void Pause()
        {
            if (_animationQueue != null)
            {
                foreach (var animation in _animationQueue)
                    animation.Pause();
            }
        }

        public void Resume()
        {
            if (_animationQueue != null)
            {
                foreach (var animation in _animationQueue)
                    animation.Resume();
            }
        }

        public bool IsComplete
        {
            get { return _animationQueue.Count == 0; }
        }
    }
}