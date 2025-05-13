using System;
using System.Collections.Generic;
using TDTest.Structural;
using TDTest.Time;
using UnityEngine;

namespace TDTest.Combat
{
    public class CombatSystem : ISystem
    {
        Timer tickTimer;
        List<Structure> registeredStructures;
        int waveIndex;

        public void Initialize()
        {
            tickTimer = new()
            {
                IsScaled = true,
                IsRepeating = true
            };

            tickTimer.OnComplete += OnTickTimerCompleted;

            registeredStructures = new();
            waveIndex = 0;
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
            tickTimer = null;
            registeredStructures = null;
        }

        public void StartWave()
        {

        }

        public void RegisterStructure(Structure structure)
        {
            Debug.Assert(!registeredStructures.Contains(structure), "CombatSystem: Tried to register structure twice");
            registeredStructures.Add(structure);
        }

        public void UnregisterStructure(Structure structure)
        {
            Debug.Assert(registeredStructures.Contains(structure), "CombatSystem: Tried to unregister unknown structure");
            registeredStructures.Remove(structure);
        }

        void OnTickTimerCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
