using TDTest.GameFlow;
using UnityEngine;

namespace TDTest.UI
{
    public class Header : UIComponent
    {
        [SerializeField] PlayButton playButton;
        [SerializeField] TimeToggles timeToggles;

        protected override void Initialize()
        {
            base.Initialize();

            Statics.Flow.FSM.StateMachine.Configure(FlowFSM.State.Fighting)
                .OnEntry(UpdateUI);

            Statics.Flow.FSM.StateMachine.Configure(FlowFSM.State.Building)
                .OnEntry(UpdateUI);

            UpdateUI();
        }

        void UpdateUI()
        {
            var isFighting = Statics.Flow.FSM.StateMachine.State is FlowFSM.State.Fighting;

            playButton.gameObject.SetActive(!isFighting);
            timeToggles.gameObject.SetActive(isFighting);
        }
    }
}
