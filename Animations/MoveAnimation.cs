using System.Drawing;
using System.Windows.Forms;

namespace Lifelike.Animations
{
    public class MoveAnimation : AnimationBase
    {
        private readonly Point _origin;
        private readonly Point _destination;

        public MoveAnimation(Control control, Point destination, Timing.Timing? timingFunction = null)
            : base(control, timingFunction)
        {
            _origin = control.Location;
            _destination = destination;

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