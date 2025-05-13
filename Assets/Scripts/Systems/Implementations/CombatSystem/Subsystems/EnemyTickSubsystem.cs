using System.Collections.Generic;
using TDTest.Structural;
using UnityEngine;
using UniHelper;
using System;

namespace TDTest.Combat
{
    public class EnemyTickSubsystem : ISystem
    {
        public Action OnEnemyWaveFinish;
        public Action<Enemy> OnEnemyPathFinish;

        Dictionary<int, List<EnemySpawnEvent>> tickSpawnEventLookup;
        List<Enemy> registeredEnemies;
        Stack<EnemyDestructionEntry> enemiesToDestroy;

        int tickIndex;

        Enemy enemyPrefab;

        public void Initialize()
        {
            registeredEnemies = new();
            enemiesToDestroy = new();
            tickSpawnEventLookup = new();
            tickIndex = 0;
        }

        public void Tick(float _, float __)
        {
            MoveEnemies();
            SpawnForTick();
            
            while (enemiesToDestroy.TryPop(out var entry))
            {
                DestroyEnemy(entry);
            }
            
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

        public void PushDestroyEnemy(EnemyDestructionEntry entry)
        {
            Debug.Assert(registeredEnemies.Contains(entry.Enemy), "EnemyTickSubsytem: Tried to destroy unknown enemy");
            enemiesToDestroy.Push(entry);
        }

        void DestroyEnemy(EnemyDestructionEntry entry)
        {
            registeredEnemies.Remove(entry.Enemy);
            UnityEngine.Object.Destroy(entry.Enemy.gameObject);

            if (entry.GiveGold)
            {
                // TODO: Give Player Coins
            }

            Debug.Log($"Registered enemies: {registeredEnemies.Count}, TickSpawnEvents: {tickSpawnEventLookup.Count}");
            if (registeredEnemies.Count == 0 && tickSpawnEventLookup.Count == 0)
            {
                OnEnemyWaveFinish?.Invoke();
            }
        }

        void SpawnForTick()
        {
            if (tickSpawnEventLookup.TryGetValue(tickIndex, out var spawnEvents))
            {
                spawnEvents.ForEach(e => SpawnEnemy(e));
                tickSpawnEventLookup.Remove(tickIndex);
            }
        }

        void SpawnEnemy(EnemySpawnEvent enemySpawn)
        {
            var enemy = UnityEngine.Object.Instantiate(enemyPrefab);
            enemy.Initialize(enemySpawn.Description, enemySpawn.Structure);

            var startPosCoords = enemySpawn.Structure.EnemyPath[0];
            var startCell = enemySpawn.Structure.Grid.Cells[startPosCoords.x, startPosCoords.y];
            enemy.transform.position = startCell.WorldPosition;

            registeredEnemies.Add(enemy);
        }

        void MoveEnemies()
        {
            registeredEnemies.ForEach(enemy =>
            {
                enemy.PathIndex++;

                if (enemy.Structure.EnemyPath.TryGetValueAt(enemy.PathIndex, out var posCoords))
                    enemy.SetPosition(posCoords);
                else
                    OnEnemyPathFinish?.Invoke(enemy);
            });
        }
    }
}
