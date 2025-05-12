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
        [field: SerializeField] public int CooldownTicks { get; private set; }
    }

    [CreateAssetMenu(fileName = "EnemyWaveDescription", menuName = "Scriptable Objects/EnemyWaveDescription")]
    public class EnemyWaveDescription : ScriptableObject
    {
        public List<EnemyWaveEntry> EnemyWaves;
    }
}
