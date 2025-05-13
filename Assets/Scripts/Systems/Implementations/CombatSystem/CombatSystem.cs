using System;
using TDTest.Structural;
using TDTest.Time;

namespace TDTest.Combat
{
    public class CombatSystem : ISystem
    {
        Timer tickTimer;

        public void Initialize()
        {
            tickTimer = new();

            tickTimer.IsScaled = true;
            tickTimer.IsRepeating = true;
            
            tickTimer.OnComplete += OnTickTimerCompleted;

        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
            tickTimer = null;
        }

        public void StartWave()
        {

        }

        public void RegisterStructure(Structure structure)
        {

        }

        void OnTickTimerCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
