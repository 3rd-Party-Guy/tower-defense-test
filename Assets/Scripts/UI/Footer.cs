using UnityEngine;

namespace TDTest.UI
{
    public class Footer : UIComponent
    {
        [SerializeField] TurretCardHolderUI turretCardHolderUI;
        [SerializeField] MoveButtons moveButtons;
        [SerializeField] BuildButtons buildButtons;

        protected override void Initialize()
        {
            base.Initialize();

            Statics.Build.FSM.StateMachine.Configure(BuildFSM.State.Idle)
                .OnEntry(OnChangeBuildState);
            Statics.Build.FSM.StateMachine.Configure(BuildFSM.State.Move)
                .OnEntry(OnChangeBuildState);
            Statics.Build.FSM.StateMachine.Configure(BuildFSM.State.Build)
                .OnEntry(OnChangeBuildState);
            Statics.Build.FSM.StateMachine.Configure(BuildFSM.State.Blocked)
                .OnEntry(OnChangeBuildState);

            OnChangeBuildState();
        }

        void OnChangeBuildState()
        {
            if (Statics.Build.FSM.StateMachine.State is BuildFSM.State.Blocked)
            {
                turretCardHolderUI.gameObject.SetActive(false);
                moveButtons.gameObject.SetActive(false);
                buildButtons.gameObject.SetActive(false);

                return;
            }

            var isIdle = Statics.Build.FSM.StateMachine.State is BuildFSM.State.Idle;
            var isMove = Statics.Build.FSM.StateMachine.State is BuildFSM.State.Move;
            var isBuild = Statics.Build.FSM.StateMachine.State is BuildFSM.State.Build;

            turretCardHolderUI.gameObject.SetActive(isIdle);
            moveButtons.gameObject.SetActive(isMove);
            buildButtons.gameObject.SetActive(isBuild);
        }
    }
}
