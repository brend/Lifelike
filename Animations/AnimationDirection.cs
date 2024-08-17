namespace Lifelike.Animations
{
    public enum AnimationDirection
    {
        Forward,
        Reverse
    }

    public static class AnimationDirectionExtensions
    {
        public static AnimationDirection Reverse(this AnimationDirection direction) =>
            direction == AnimationDirection.Forward
                ? AnimationDirection.Reverse
                : AnimationDirection.Forward;
    }
}