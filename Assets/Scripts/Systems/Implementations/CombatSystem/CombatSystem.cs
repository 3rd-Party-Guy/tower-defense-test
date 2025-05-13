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

        EnemyTickSubsystem enemyTickSubsystem;

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

            Statics.Flow.FSM.StateMachine.Configure(GameFlow.FlowFSM.State.Fighting)
                .OnEntry(StartWave)
                .OnExit(StopWave);

            enemyTickSubsystem = new();
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
            tickTimer = null;
            registeredStructures = null;
        }

        public void SetEnemyPrefab(Enemy newEnemyPrefab)
        {
            enemyTickSubsystem.SetEnemyPrefab(newEnemyPrefab);
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

        void StartWave()
        {
            enemyTickSubsystem.CreateSpawnEventsForWave(registeredStructures, waveIndex);
            tickTimer.Start(1f);
        }

        void StopWave()
        {
            tickTimer.Pause();
        }

        void OnTickTimerCompleted()
        {
            enemyTickSubsystem.Tick(0f, 0f);
        }
    }
}
