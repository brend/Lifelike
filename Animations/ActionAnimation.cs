using System;
using System.Windows.Forms;
using Lifelike.Timing;

using Progress = System.Double;

namespace Lifelike.Animations
{
    public class ActionAnimation : AnimationBase
    {
        private readonly Action<Control, Progress> _action;

        public ActionAnimation(Control control, Easing easing, Action<Control, Progress> action)
            : base(control, easing)
        {
            _action = action ?? throw new ArgumentNullException("action");
        }

        protected override void Update()
        {
            _action(Control, Progress);
        }
    }
}