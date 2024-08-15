using System;
using System.Windows.Forms;

namespace Lifelike.Animations
{
    public class SlideAnimation
    {
        public static void SlideInFromLeft(Control control, TimeSpan? duration = null)
        {
            var durationInMilliseconds = (duration ?? FadeAnimation.DefaultAnimationDuration).TotalMilliseconds;

            if (durationInMilliseconds <= 0)
            {
                return;
            }

            var interval = 10;
            var steps = durationInMilliseconds / interval;
            int step = 0;
            var timer = new Timer { Interval = interval };

            var originalLocation = control.Location;
            var targetLocation = new System.Drawing.Point(0, originalLocation.Y);

            timer.Tick += (sender, e) =>
            {
                step++;
                var x = (int)(originalLocation.X * (double)step / steps);
                control.Location = new System.Drawing.Point(x, originalLocation.Y);
                if (step >= steps)
                {
                    control.Location = originalLocation;
                    timer.Stop();
                    timer.Dispose();
                }
            };
            control.Location = new System.Drawing.Point(-control.Width, originalLocation.Y);
            control.Show();
            timer.Start();
        }
    }
}