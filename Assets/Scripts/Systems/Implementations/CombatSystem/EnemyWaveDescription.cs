using System.Collections.Generic;
using UnityEngine;

namespace TDTest.Combat
{
    [System.Serializable]
    public struct EnemySpawnEntry
    {
        [field: SerializeField] public EnemyDescription EnemyToSpawn { get; private set; }
        [field: SerializeField] public int TickToSpawnOn { get; private set; }
    }

    [System.Serializable]
    public struct EnemyWaveDescription
    {
        public List<EnemySpawnEntry> EnemySpawns;
    }
}
