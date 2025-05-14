namespace TDTest
{
    public interface ISystem
    {
        void Initialize();
        void Tick(float deltaTime, float unscaledDeltaTime);
        void Deinitialize();
    }
}
