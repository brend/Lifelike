using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lifelike.Animations
{
    public class SlideAnimation
    {
        private readonly AnimationTimer _timer;
        private readonly Control _control;
        private readonly Point _origin;
        private readonly Point _destination;

        public SlideAnimation(Control control, TimeSpan duration, Func<double, double> timingFunction = null)
        {
            _control = control;
            _origin = new Point(-control.Width, control.Location.Y);
            _destination = control.Location;
            timingFunction = timingFunction ?? TimingFunctions.EaseOut;
            _timer = new AnimationTimer(duration, timingFunction);
            _timer.Tick += TimerTick;

            _control.Location = _origin;
            _timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _control.Location = new Point(
                (int)(_origin.X + (_destination.X - _origin.X) * _timer.Progress),
                (int)(_origin.Y + (_destination.Y - _origin.Y) * _timer.Progress)
            );
            
            if (_timer.Progress >= 1)
            {
                _timer.Stop();
            }
        }
    }
}