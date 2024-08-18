using System;
using System.Drawing;
using System.Windows.Forms;
using Lifelike.Timing;

namespace Lifelike.Animations
{
    public static class ControlExtensions
    {
        private static ITimer CreateTimer() => new AnimationTimer();

        public static IAnimation AnimateMove(
            this Control control, 
            Point destination, 
            TimeSpan? duration = null, 
            Easing? timingFunction = null)
        {
            var animation = new MoveAnimation(
                control, 
                CreateTimer(),
                duration ?? AnimationDefaults.Duration, 
                timingFunction ?? AnimationDefaults.TimingFunction, 
                destination);
            animation.Start();
            return animation;
        }
    }
}