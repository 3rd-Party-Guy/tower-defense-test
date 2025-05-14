using System;
using System.Collections.Generic;
using TDTest.Structural;
using TDTest.Time;
using UnityEngine;

namespace TDTest.Combat
{
    public class CombatSystem : ISystem
    {
        public Health PlayerHealth { get; private set; }

        EnemyTickSubsystem enemyTickSubsystem;
        
        List<Structure> registeredStructures;
        int waveIndex;

        Timer tickTimer;

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
            enemyTickSubsystem.Initialize();
            enemyTickSubsystem.OnEnemyPathFinish += OnEnemyPathFinished;
            enemyTickSubsystem.OnEnemyWaveFinish += OnEnemyWaveFinished;

            PlayerHealth = new(10);
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
            tickTimer = null;
            registeredStructures = null;

            enemyTickSubsystem.Deinitialize();
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

        public void KillEnemy(Enemy enemy)
        {
            enemyTickSubsystem.PushDestroyEnemy(new(enemy, true));
        }

        void StartWave()
        {
            enemyTickSubsystem.CreateSpawnEventsForWave(registeredStructures, waveIndex);
            enemyTickSubsystem.RestartTick();

            tickTimer.Start(1f);
            
            Statics.Build.FSM.StateMachine.Signal(BuildFSM.Trigger.StartFight);
        }

        void StopWave()
        {
            tickTimer.Pause();
            
            Statics.Build.FSM.StateMachine.Signal(BuildFSM.Trigger.EndFight);
        }

        void OnTickTimerCompleted()
        {
            enemyTickSubsystem.Tick(0f, 0f);
        }

        void OnEnemyPathFinished(Enemy enemy)
        {
            PlayerHealth.Damage(enemy.Description.Damage);
            enemyTickSubsystem.PushDestroyEnemy(new(enemy, false));
        }

        void OnEnemyWaveFinished()
        {
            waveIndex++;
            Statics.Flow.FSM.StateMachine.Signal(GameFlow.FlowFSM.Trigger.RoundEnd);
        }
    }
}
