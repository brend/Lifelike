using Lifelike.Timing;

namespace Lifelike.Animations
{
    public static class ControlExtensions
    {
        public static void SlideIn(this System.Windows.Forms.Control control, int durationInMilliseconds)
        {
            var timing = EasingFunctions.EaseInOut(System.TimeSpan.FromMilliseconds(durationInMilliseconds));
            var animation = new SlideAnimation(control, timing);
            animation.Start();
        }
    }
}