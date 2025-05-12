using UniState;

namespace TDTest.Structural
{
    public class CellFSM
    {
        public enum Trigger { ToFree, ToEnemyPath, ToBuilding };
        public enum State { Free, EnemyPath, Building };
        public StateMachine<State, Trigger> StateMachine { get; private set; }

        public CellFSM()
        {
            StateMachine = new(State.Free);

            StateMachine.Configure(State.Free)
                .Permit(Trigger.ToEnemyPath, State.EnemyPath)
                .Permit(Trigger.ToBuilding, State.Building);

            StateMachine.Configure(State.Building)
                .Permit(Trigger.ToFree, State.Free);
        }
    }
}
