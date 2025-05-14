using UnityEngine;

namespace TDTest.Combat
{
    public class TurretSkeleton : MonoBehaviour
    {
        [field: SerializeField] public Transform Body { get; private set; }
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public TurretShootRenderer ShootRenderer { get; private set; }
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
    }
}
