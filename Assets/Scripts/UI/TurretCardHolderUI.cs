using System;
using System.Collections.Generic;
using UniHelper;
using UnityEngine;

namespace TDTest.UI
{
    public class TurretCardHolderUI : UIComponent
    {
        [SerializeField] Transform cardHolder;
        [SerializeField] TurretCard cardPrefab;

        List<TurretCard> cards;

        protected override void Initialize()
        {
            cards = new();

            Statics.Build.OnAvailableTurretsChange += CreateCards;
            Statics.Gold.OnGoldChange += ChangeCardsInteractable;
        }

        void CreateCards()
        {
            var availableTurrets = Statics.Build.AvailableTurrets;

            availableTurrets.ForEach(e =>
            {
                var card = Instantiate(cardPrefab, cardHolder);
                card.InitializeForDescription(e);

                cards.Add(card);
            });

            ChangeCardsInteractable(Statics.Gold.Gold);
        }

        void ChangeCardsInteractable(int goldAvailable)
        {
            cards.ForEach(e =>
            {
                var shouldBeInteractable = goldAvailable >= e.Description.Cost;
                e.UpdateInteractable(shouldBeInteractable);
            });
        }
    }
}
