using UniState;

namespace TDTest.Combat
{
    public class TurretFSM
    {
        public enum Trigger { TargetFound, NoTarget };
        public enum State { Fighting, Idle }
        public StateMachine<State, Trigger> StateMachine { get; private set; }

        public TurretFSM()
        {
            StateMachine = new(State.Idle);

            StateMachine.Configure(State.Idle)
                .Permit(Trigger.TargetFound, State.Fighting)
                .Ignore(Trigger.NoTarget);

            StateMachine.Configure(State.Fighting)
                .Permit(Trigger.NoTarget, State.Idle)
                .Ignore(Trigger.TargetFound);
        }
    }
}
