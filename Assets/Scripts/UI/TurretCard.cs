using System;
using TDTest.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TDTest.UI
{
    public class TurretCard : UIComponent
    {
        public TurretDescription Description { get; private set; }

        [SerializeField] Button button;
        [SerializeField] TMP_Text nameLabel;
        [SerializeField] TMP_Text costLabel;

        public void InitializeForDescription(TurretDescription description)
        {
            Description = description;

            nameLabel.text = description.Name;
            costLabel.text = $"{description.Cost} Gold";

            button.onClick.AddListener(OnCardSelected);
        }

        public void UpdateInteractable(bool isInteractable)
        {
            button.interactable = isInteractable;
        }
        
        void OnCardSelected()
        {
            Statics.Build.EnterBuild(Description);
        }
    }
}
