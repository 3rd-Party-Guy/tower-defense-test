using System;
using UnityEngine;
using UnityEngine.UI;

namespace TDTest.UI
{
    public class TimeToggles : UIComponent
    {
        [SerializeField] Button speedOne;
        [SerializeField] Button speedTwo;
        [SerializeField] Button speedThree;

        [Space(5)]

        [SerializeField] float speedOneScale;
        [SerializeField] float speedTwoScale;
        [SerializeField] float speedThreeScale;

        protected override void Initialize()
        {
            base.Initialize();

            speedOne.onClick.AddListener(() => ChangeTimeScale(speedOneScale));
            speedTwo.onClick.AddListener(() => ChangeTimeScale(speedTwoScale));
            speedThree.onClick.AddListener(() => ChangeTimeScale(speedThreeScale));
        }

        private void OnDestroy()
        {
            speedOne.onClick.RemoveAllListeners();
            speedTwo.onClick.RemoveAllListeners();
            speedThree.onClick.RemoveAllListeners();
        }

        void ChangeTimeScale(float scale)
        {
            UnityEngine.Time.timeScale = scale;
        }
    }
}
