using System;
using System.Windows.Forms;
using Lifelike.Timing;
using Progress = System.Double;

namespace Lifelike.Animations
{
    public class ActionAnimation : ControlAnimation
    {
        private readonly Action<Control, Progress> _action;

        public ActionAnimation(
            Control control, 
            ITimer timer,
            TimeSpan duration, 
            Easing timingFunction,
            Action<Control, Progress> action)
            : base(control, timer, duration, timingFunction)
        {
            _action = action ?? throw new ArgumentNullException("action");
        }

        protected override void Update()
        {
            _action(Control, Progress);
        }
    }
}