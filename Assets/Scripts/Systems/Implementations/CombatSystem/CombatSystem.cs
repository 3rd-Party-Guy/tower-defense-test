using System.Collections.Generic;
using TDTest.Structural;
using TDTest.Time;
using UniHelper;
using UnityEngine;

namespace TDTest.Combat
{
    public class CombatSystem : ISystem
    {
        Timer tickTimer;
        List<Structure> registeredStructures;
        Dictionary<int, List<EnemySpawnEvent>> tickSpawnEventLookup;
        Enemy enemyPrefab;

        int tickIndex;

        public void Initialize()
        {
            tickTimer = new()
            {
                IsScaled = true,
                IsRepeating = true
            };

            tickTimer.OnComplete += OnTickTimerCompleted;

            registeredStructures = new();
            tickSpawnEventLookup = new();
            tickIndex = 0;

            Statics.Flow.FSM.StateMachine.Configure(GameFlow.FlowFSM.State.Fighting)
                .OnEntry(StartWave)
                .OnExit(StopWave);
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
            tickTimer = null;
            registeredStructures = null;
        }

        public void SetEnemyPrefab(Enemy newEnemyPrafab)
        {
            enemyPrefab = newEnemyPrafab;
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
        }

        void StopWave()
        {

        }

        void OnTickTimerCompleted()
        {
            SpawnForTick();
            tickIndex++;
        }

        void SpawnForTick()
        {
            if (tickSpawnEventLookup.TryGetValue(tickIndex, out var spawnEvents))
            {
                spawnEvents.ForEach(e => SpawnEnemy(e));
            }
        }

        void SpawnEnemy(EnemySpawnEvent enemySpawn)
        {
            var enemy = Object.Instantiate(enemyPrefab);
            enemy.Initialize(enemySpawn.Description, enemySpawn.Structure);

            var startPosCoords = enemySpawn.Structure.EnemyPath[0];
            var startCell = enemySpawn.Structure.Grid.Cells[startPosCoords.x, startPosCoords.y];
            enemy.transform.position = startCell.WorldPosition;
        }
    }
}
