using TDTest.Combat;
using UnityEngine;

namespace TDTest
{
    [System.Serializable]
    public struct EnemyDestructionEntry
    {
        public Enemy Enemy { get; private set; }
        public bool GiveGold { get; private set; }

        public EnemyDestructionEntry(Enemy enemy, bool giveMoney)
        {
            Enemy = enemy;
            GiveGold = giveMoney;
        }
    }
}
