using UnityEngine;

namespace TDTest.Combat
{
    [CreateAssetMenu(fileName = "EnemyDescription", menuName = "Game/Enemy Description")]
    public class EnemyDescription : ScriptableObject
    {
        [field: SerializeField] public GameObject Skeleton { get; private set; }
        [field: SerializeField] public int StartHealth { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int GoldReward { get; private set; }
    }
}
