using System;
using Lifelike.Timing;

namespace Lifelike.Animations
{
    public static class AnimationDefaults
    {
        public static TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(1);

        public static Easing TimingFunction { get; set; } = EasingFunctions.EaseInOut;
    }
}