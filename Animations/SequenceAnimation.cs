using System.Collections.Generic;
using System.Windows.Forms;

namespace Lifelike.Animations
{
    public class SequenceAnimation : IAnimation
    {
        public Control Control { get; private set; }
        public List<IAnimation> Animations { get; private set; }

        private Queue<IAnimation> _animationQueue;

        public SequenceAnimation(Control control, IEnumerable<IAnimation> animations)
        {
            Control = control;
            Animations = new List<IAnimation>(animations);
        }

        public void Start()
        {
            _animationQueue = new Queue<IAnimation>(Animations);
            _animationQueue.Peek().Start();
        }

        public void Update()
        {
            if (_animationQueue.Count == 0)
                return;

            _animationQueue.Peek().Update();

            if (_animationQueue.Peek().IsComplete)
            {
                _animationQueue.Dequeue();

                if (_animationQueue.Count > 0)
                    _animationQueue.Peek().Start();
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