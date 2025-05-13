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

        int waveIndex;
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
            waveIndex = 0;
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
            CreateSpawnEventsForWave();
            tickTimer.Start(1f);
        }

        void StopWave()
        {
            tickTimer.Pause();
        }

        void OnTickTimerCompleted()
        {
            Debug.Log($"Handling tick {tickIndex}");
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
        
        void CreateSpawnEventsForWave()
        {
            tickSpawnEventLookup = new();
            registeredStructures.ForEach(structure =>
            {
                if (structure.EnemyWaveDescriptions.TryGetValueAt(waveIndex, out var waveDescription))
                {
                    waveDescription.EnemySpawns.ForEach(spawn =>
                    {
                        if (!tickSpawnEventLookup.ContainsKey(spawn.TickToSpawnOn))
                            tickSpawnEventLookup[spawn.TickToSpawnOn] = new();

                        tickSpawnEventLookup[spawn.TickToSpawnOn].Add(new(spawn.EnemyToSpawn, structure));
                    });
                }
            });
        }
    }
}
