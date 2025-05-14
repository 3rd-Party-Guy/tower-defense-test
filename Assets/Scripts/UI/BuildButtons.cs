using System;
using UnityEngine;
using UnityEngine.UI;

namespace TDTest.UI
{
    public class BuildButtons : UIComponent
    {
        [SerializeField] Button cancelButton;
        [SerializeField] Button buildButton;

        protected override void Initialize()
        {
            base.Initialize();

            cancelButton.onClick.AddListener(Cancel);
            buildButton.onClick.AddListener(Build);

            Statics.Build.OnPreviewChange += CanBuildCheck;
        }

        void Cancel()
        {
            Statics.Build.ExitBuild();
        }

        void Build()
        {
            Statics.Build.PlaceTurretOnGrid(true);
        }

        void CanBuildCheck()
        {
            buildButton.interactable = Statics.Build.CanPlaceOnGrid();
        }
    }
}
