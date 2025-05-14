namespace TDTest.GameFlow
{
    public class FlowSystem : ISystem
    {
        public FlowFSM FSM { get; private set; }

        public void Initialize()
        {
            FSM = new();
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
        
        }
    }
}
