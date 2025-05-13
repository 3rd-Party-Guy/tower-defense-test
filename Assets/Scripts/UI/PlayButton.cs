using TDTest.GameFlow;
using UnityEngine;
using UnityEngine.UI;

namespace TDTest.UI
{
    public class PlayButton : UIComponent
    {
        [SerializeField] Button playButton;

        protected override void Initialize()
        {
            base.Initialize();

            playButton.onClick.AddListener(PlayGame);
            Statics.Flow.FSM.StateMachine.Configure(FlowFSM.State.Fighting)
                .OnEntry(UpdateUI)
                .OnExit(UpdateUI);
        }

        void OnDestroy()
        {
            playButton.onClick.RemoveAllListeners();
        }
        
        void UpdateUI()
        {
            var isFighting = Statics.Flow.FSM.StateMachine.State is FlowFSM.State.Fighting;
            Debug.Log($"setting active: {!isFighting}");
            playButton.gameObject.SetActive(!isFighting);
        }
        
        void PlayGame()
        {
            Statics.Flow.FSM.StateMachine.Signal(FlowFSM.Trigger.RoundStart);
        }
    }
}
