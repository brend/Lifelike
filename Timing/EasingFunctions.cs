using System;

namespace Lifelike.Timing
{
    public static class EasingFunctions
    {
        private static double Linear(double t) => t;

        public static Easing Linear(TimeSpan duration) => new Easing(duration, Linear);

        private static double EaseIn(double t) => t * t;

        public static Easing EaseIn(TimeSpan duration) => new Easing(duration, EaseIn);

        private static double EaseOut(double t) => t * (2 - t);

        public static Easing EaseOut(TimeSpan duration) => new Easing(duration, EaseOut);

        private static double EaseInOut(double t) => t < 0.5 ? 2 * t * t : -1 + (4 - 2 * t) * t;

        public static Easing EaseInOut(TimeSpan duration) => new Easing(duration, EaseInOut);

        private static double Bounce(double t)
        {
            if (t < 1 / 2.75)
                return 7.5625 * t * t;
            else if (t < 2 / 2.75)
                return 7.5625 * (t -= 1.5 / 2.75) * t + 0.75;
            else if (t < 2.5 / 2.75)
                return 7.5625 * (t -= 2.25 / 2.75) * t + 0.9375;
            else
                return 7.5625 * (t -= 2.625 / 2.75) * t + 0.984375;
        }

        public static Easing Bounce(TimeSpan duration) => new Easing(duration, Bounce);
    }
}