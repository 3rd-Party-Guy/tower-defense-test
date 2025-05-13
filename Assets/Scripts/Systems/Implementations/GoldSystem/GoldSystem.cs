using System;
using UnityEngine;

namespace TDTest.Gold
{
    public class GoldSystem : ISystem
    {
        public Action<int> OnGoldChange;
        public int Gold
        {
            get => gold;
            private set
            {
                gold = value;
                OnGoldChange?.Invoke(gold);
            }
        }

        int gold;

        public void Initialize()
        {
            Gold = 0;
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {

        }

        public void AddGold(int amount)
        {
            Gold += amount;
            OnGoldChange?.Invoke(Gold);
        }

        public void RemoveGold(int amount)
        {
            Debug.Assert(Gold >= amount, "GoldSystem: Tried to remove more gold than possible");

            Gold -= amount;
            OnGoldChange?.Invoke(Gold);
        }
    }
}
