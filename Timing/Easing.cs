using System;
using TimingFunc = System.Func<double, double>;

namespace Lifelike.Timing
{
    public readonly struct Easing
    {
        public TimeSpan Duration { get; }

        public TimingFunc Function { get; }

        public Easing(TimeSpan duration, TimingFunc function)
        {
            Duration = duration;
            Function = function;
        }

        public double Apply(TimeSpan elapsed) => 
            Function((double)elapsed.TotalMilliseconds / Duration.TotalMilliseconds);
    }
}