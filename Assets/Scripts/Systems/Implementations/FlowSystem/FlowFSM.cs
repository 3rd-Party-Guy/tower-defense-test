using UniState;

namespace TDTest.GameFlow
{
    public class FlowFSM
    {
        public enum Trigger { RoundStart, RoundEnd, Death, Win };
        public enum State { Building, Fighting, Lose, Win };
        public StateMachine<State, Trigger> StateMachine { get; private set; }

        public FlowFSM()
        {
            StateMachine = new(State.Building);

            StateMachine.Configure(State.Building)
                .Permit(Trigger.RoundStart, State.Fighting);
            StateMachine.Configure(State.Fighting)
                .Permit(Trigger.RoundEnd, State.Building)
                .Permit(Trigger.Death, State.Lose)
                .Permit(Trigger.Win, State.Win);
        }
    }
}
