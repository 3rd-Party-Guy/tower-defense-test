using System.Collections.Generic;
using UnityEngine;

namespace TDTest.Time
{
    public class TimeSystem : ISystem
    {
        List<Timer> timers = new();

        public void Initialize()
        {
        }

        public void Tick(float deltaTime, float unscaledDeltaTime)
        {
            timers.ForEach(e => e.Tick(deltaTime, unscaledDeltaTime));
        }

        public void Deinitialize() 
        {
            timers.Clear();
            timers = null;

            UnityEngine.Time.timeScale = 1f;
        }

        public void Register(Timer timer)
        {
            Debug.Assert(!timers.Contains(timer), "TimeSystem: Tried to register timer twice");
            timers.Add(timer);
        }

        public void Unregister(Timer timer)
        {
            Debug.Assert(timers.Contains(timer), "TimeSystem: Tried to unregister unknown timer");
            timers.Remove(timer);
        }
    }
}
