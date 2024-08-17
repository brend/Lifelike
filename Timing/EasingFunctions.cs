using System;

namespace Lifelike.Timing
{
    public static class EasingFunctions
    {
        public static readonly Easing Linear = new Easing(t => t);

        public static readonly Easing EaseIn = new Easing(t => t * t);

        public static readonly Easing EaseOut = new Easing(t => t * (2 - t));

        public static readonly Easing EaseInOut = new Easing(t => t < 0.5 ? 2 * t * t : -1 + (4 - 2 * t) * t);

        private static double BounceFunc(double t)
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

        public static readonly Easing Bounce = new Easing(BounceFunc);
    }
}