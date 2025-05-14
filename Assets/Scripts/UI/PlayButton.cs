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
        }

        void OnDestroy()
        {
            playButton.onClick.RemoveAllListeners();
        }
        
        void PlayGame()
        {
            Statics.Flow.FSM.StateMachine.Signal(FlowFSM.Trigger.RoundStart);
        }
    }
}
