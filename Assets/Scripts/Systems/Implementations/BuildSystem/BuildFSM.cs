using UniState;

namespace TDTest
{
    public class BuildFSM
    {
        public enum Trigger { CancelBuild, CancelMove, StartBuild, StartMove, FinishBuild, FinishMove, StartFight, EndFight };
        public enum State { Idle, Build, Move, Blocked };
        public StateMachine<State, Trigger> StateMachine { get; private set; }

        public BuildFSM()
        {
            StateMachine = new(State.Idle);

            StateMachine.Configure(State.Idle)
                .Permit(Trigger.StartBuild, State.Build)
                .Permit(Trigger.StartMove, State.Move)
                .Permit(Trigger.StartFight, State.Blocked);

            StateMachine.Configure(State.Move)
                .Permit(Trigger.CancelMove, State.Idle)
                .Permit(Trigger.FinishMove, State.Idle)
                .Permit(Trigger.StartFight, State.Blocked);


            StateMachine.Configure(State.Build)
                .Permit(Trigger.CancelBuild, State.Idle)
                .Permit(Trigger.FinishBuild, State.Idle)
                .Permit(Trigger.StartFight, State.Blocked);

            StateMachine.Configure(State.Blocked)
                .Permit(Trigger.EndFight, State.Idle);
        }
    }
}
