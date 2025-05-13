using TDTest.Structural;
using UnityEngine;

namespace TDTest.Combat
{
    [System.Serializable]
    public struct EnemySpawnEvent
    {
        [field: SerializeField] public EnemyDescription Description { get; private set; }
        [field: SerializeField] public Vector2Int EnemyPath { get; private set; }
        [field: SerializeField] public Structure Structure { get; private set; }
    }
}
