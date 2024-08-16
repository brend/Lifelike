namespace Lifelike.Animations
{
    public interface IAnimation
    {
        void Start();
        void Stop();
        void Pause();
        void Resume();
        void Update();
        bool IsComplete { get; }
    }
}