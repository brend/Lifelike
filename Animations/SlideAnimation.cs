using System;
using System.Drawing;
using System.Windows.Forms;
using Lifelike.Timing;

namespace Lifelike.Animations
{
    public class SlideAnimation : AnimationBase
    {
        private readonly Point _origin;
        private readonly Point _destination;

        public SlideAnimation(Control control, Easing? timingFunction = null)
            : base(control, timingFunction)
        {
            _origin = new Point(-control.Width, control.Location.Y);
            _destination = control.Location;

            Control.Location = _origin;
            Start();
        }

        protected override void Update()
        {
            Control.Location = new Point(
                (int)(_origin.X + (_destination.X - _origin.X) * Progress),
                (int)(_origin.Y + (_destination.Y - _origin.Y) * Progress)
            );
        }
    }
}