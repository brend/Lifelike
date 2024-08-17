using System;
using System.Drawing;
using System.Windows.Forms;
using Lifelike.Timing;

namespace Lifelike.Animations
{
    public class MoveAnimation : ControlAnimation
    {
        private Point _origin;
        private readonly Point _destination;

        public MoveAnimation(Control control, TimeSpan duration, Easing timingFunction, Point destination)
            : base(control, duration, timingFunction)
        {
            _origin = control.Location;
            _destination = destination;
        }

        public override void Start()
        {
            _origin = Control.Location;
            base.Start();
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