using System;

namespace Lifelike.Timing
{
    public interface ITimer
    {
        event EventHandler Tick;
        void Start();
        void Stop();
    }
}