using System.Drawing;
using System.Windows.Forms;
using Lifelike.Timing;

namespace Lifelike.Animations
{
    public class MoveAnimation : AnimationBase
    {
        private Point _origin;
        private readonly Point _destination;

        public MoveAnimation(Control control, Point destination, Easing? timingFunction = null)
            : base(control, timingFunction)
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