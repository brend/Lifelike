using System;
using System.Windows.Forms;
using Lifelike.Timing;

namespace Lifelike.Animations
{
    public abstract class ControlAnimation : TimedAnimation
    {
        public Control Control { get; }

        public ControlAnimation(
            Control control,
            ITimer timer,
            TimeSpan duration, 
            Easing timingFunction)
            : base(timer, duration, timingFunction)
        {
            Control = control;
        }
    }
}