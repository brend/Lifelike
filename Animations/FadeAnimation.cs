using System.Windows.Forms;
using System.Drawing;
using System;

namespace Lifelike.Animations
{
    public class FadeAnimation
    {
        public static TimeSpan DefaultAnimationDuration = TimeSpan.FromMilliseconds(500);

        public static void FadeIn(Control control, TimeSpan? duration = null)
        {
            var durationInMilliseconds = (duration ?? DefaultAnimationDuration).TotalMilliseconds;

            if (durationInMilliseconds <= 0)
            {
                control.Visible = true;
                return;
            }

            var interval = 10;
            var steps = durationInMilliseconds / interval;
            int step = 0;
            var timer = new Timer { Interval = interval };

            var originalColor = control.BackColor;
            var fadedColor = Color.FromArgb(0, originalColor);

            timer.Tick += (sender, e) =>
            {
                step++;
                var alpha = (int)(255 * (double)step / steps);
                control.BackColor = Color.FromArgb(alpha, originalColor);
                if (step >= steps)
                {
                    control.BackColor = originalColor;
                    timer.Stop();
                    timer.Dispose();
                }
            };
            control.BackColor = fadedColor;
            control.Show();
            timer.Start();
        }
    }
}