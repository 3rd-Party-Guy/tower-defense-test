using System.Collections.Generic;
using UnityEngine;

namespace TDTest.Combat
{
    [CreateAssetMenu(fileName = "TurretDescription", menuName = "Game/Turret Description")]
    public class TurretDescription : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public TurretSkeleton SkeletonPrefab { get; private set; }
        [field: SerializeField] public float ShootDelay { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public List<Vector2Int> GridOffsets { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
    }
}
