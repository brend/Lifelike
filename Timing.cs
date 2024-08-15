namespace Lifelike
{
    using System;
    using TimingFunc = System.Func<double, double>;

    public struct Timing
    {
        public TimeSpan Duration { get; }

        public TimingFunc Function { get; }

        public Timing(TimeSpan duration, TimingFunc function)
        {
            Duration = duration;
            Function = function;
        }

        public double Apply(TimeSpan elapsed) => 
            Function((double)elapsed.TotalMilliseconds / Duration.TotalMilliseconds);
    }
}