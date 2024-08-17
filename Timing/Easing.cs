using System;
using TimingFunc = System.Func<double, double>;

namespace Lifelike.Timing
{
    public readonly struct Easing
    {
        public TimingFunc Function { get; }

        public Easing(TimingFunc function)
        {
            Function = function;
        }

        public double Apply(TimeSpan duration, TimeSpan elapsed) => 
            Function(elapsed.TotalMilliseconds / duration.TotalMilliseconds);
    }
}