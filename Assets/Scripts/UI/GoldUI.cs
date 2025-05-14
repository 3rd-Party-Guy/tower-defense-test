using TMPro;
using UnityEngine;

namespace TDTest.UI
{
    public class GoldUI : UIComponent
    {
        [SerializeField] TMP_Text goldLabel;

        protected override void Initialize()
        {
            base.Initialize();

            Statics.Gold.OnGoldChange += UpdateUI;
            UpdateUI(Statics.Gold.Gold);
        }

        void UpdateUI(int gold)
        {
            goldLabel.text = $"Gold: {gold}";
        }
    }
}
