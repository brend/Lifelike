using System;
using System.Collections.Generic;

namespace Lifelike.Animations
{
    /// <summary>
    /// Execute a group of animations simultaneously.
    /// </summary>
    public class GroupAnimation : IAnimation
    {
        public event EventHandler Completed;

        public List<IAnimation> Animations { get; private set; }

        public GroupAnimation(IEnumerable<IAnimation> animations)
        {
            Animations = new List<IAnimation>(animations);
        }

        public void Start()
        {
            foreach (var animation in Animations)
            {
                animation.Completed += ElementCompleted;
                animation.Start();
            }
        }

        private void ElementCompleted(object sender, EventArgs e)
        {
            var completedElement = (IAnimation)sender;
            completedElement.Completed -= ElementCompleted;

            if (Animations.TrueForAll(a => a.IsComplete))
                Completed?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            foreach (var animation in Animations)
                animation.Stop();
        }

        public void Pause()
        {
            foreach (var animation in Animations)
                animation.Pause();
        }

        public void Resume()
        {
            foreach (var animation in Animations)
                animation.Resume();
        }

        public bool IsComplete => Animations.TrueForAll(a => a.IsComplete);
    }
}