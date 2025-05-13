using System.Collections.Generic;
using UnityEngine;

namespace TDTest.Combat
{
    [System.Serializable]
    public struct EnemyWaveEntry
    {
        [field: SerializeField] public EnemyDescription EnemyToSpawn { get; private set; }
        [field: SerializeField] public int AmountToSpawn { get; private set; }
        [field: SerializeField] public int TickRate { get; private set; }
    }

    [System.Serializable]
    public struct EnemyWaveDescription
    {
        public List<EnemyWaveEntry> EnemyWaves;
    }
}
