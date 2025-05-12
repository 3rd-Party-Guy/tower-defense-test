using System;
using System.Collections.Generic;
using TDTest.Input;
using TDTest.Structural;
using TDTest.Time;

namespace TDTest
{
    public static class Statics
    {
        public static event Action OnInitializationFinish;

        public static GridSystem Grids { get; private set; } = new();
        public static InputHandleSystem Inputs { get; private set; } = new();
        public static TimeSystem Time { get; private set; } = new();

        static List<ISystem> systems;

        public static void Initialize()
        {
            systems = new()
            {
                Inputs,
                Grids,
                Time,
            };

            systems.ForEach(e => e.Initialize());
            OnInitializationFinish?.Invoke();
        }

        public static void Tick(float deltaTime, float unscaledDeltaTime)
        {
            systems.ForEach(e => e.Tick(deltaTime, unscaledDeltaTime));
        }

        public static void Deinitialize()
        {
            systems.ForEach(e => e.Deinitialize());
        }
    }
}
