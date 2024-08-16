using System;

namespace Lifelike.Animations
{
    public interface IAnimation
    {
        event EventHandler Completed;

        void Start();
        void Stop();
        void Pause();
        void Resume();
        bool IsComplete { get; }
    }
}