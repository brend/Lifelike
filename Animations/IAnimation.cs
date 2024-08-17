using System;

namespace Lifelike.Animations
{
    public interface IAnimation
    {
        event EventHandler Completed;

        void Start();
        void Pause();
        void Resume();
        void Reset();
        bool IsComplete { get; }
    }
}