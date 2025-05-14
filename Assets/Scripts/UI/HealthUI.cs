using TMPro;
using UnityEngine;

namespace TDTest.UI
{
    public class HealthUI : UIComponent
    {
        [SerializeField] TMP_Text healthLabel;

        protected override void Initialize()
        {
            base.Initialize();

            Statics.Combat.PlayerHealth.OnHealthChange += UpdateUI;
            UpdateUI(Statics.Combat.PlayerHealth.HP);
        }

        void UpdateUI(int health)
        {
            healthLabel.text = $"Health: {health}";
        }
    }
}
