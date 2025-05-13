using System.Collections.Generic;
using TDTest.Structural;
using UnityEngine;
using UniHelper;

namespace TDTest.Combat
{
    public class EnemyTickSubsystem : ISystem
    {
        Dictionary<int, List<EnemySpawnEvent>> tickSpawnEventLookup;
        List<Enemy> registeredEnemies;

        int tickIndex;

        Enemy enemyPrefab;

        public void Initialize()
        {
            registeredEnemies = new();
            tickSpawnEventLookup = new();
            tickIndex = 0;
        }

        public void Tick(float _, float __)
        {
            SpawnForTick();
            tickIndex++;
        }

        public void Deinitialize()
        {
            registeredEnemies = null;
        }

        public void RegisterEnemy(Enemy enemy)
        {
            Debug.Assert(!registeredEnemies.Contains(enemy), "EnemyTickSubsystem: Tried to register enemy twice");
            registeredEnemies.Add(enemy);
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            Debug.Assert(registeredEnemies.Contains(enemy), "EnemyTickSubsystem: Tried to unregister unknown enemy");
            registeredEnemies.Remove(enemy);
        }

        public void SetEnemyPrefab(Enemy enemy)
        {
            enemyPrefab = enemy;
        }

        public void CreateSpawnEventsForWave(List<Structure> structures, int waveIndex)
        {
            tickSpawnEventLookup.Clear();
            structures.ForEach(structure =>
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
