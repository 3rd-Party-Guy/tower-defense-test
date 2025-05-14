using System;
using UnityEngine;
using UnityEngine.UI;

namespace TDTest.UI
{
    public class MoveButtons : UIComponent
    {
        [SerializeField] Button cancelButton;
        [SerializeField] Button moveButton;

        protected override void Initialize()
        {
            base.Initialize();

            cancelButton.onClick.AddListener(Cancel);
            moveButton.onClick.AddListener(Move);

            Statics.Build.OnPreviewChange += CanMoveCheck;
        }

        void Cancel()
        {
            Statics.Build.CancelMove();
        }

        void Move()
        {
            Statics.Build.PlaceTurretOnGrid(false);
        }

        void CanMoveCheck()
        {
            moveButton.interactable = Statics.Build.CanPlaceOnGrid();
        }
    }
}
