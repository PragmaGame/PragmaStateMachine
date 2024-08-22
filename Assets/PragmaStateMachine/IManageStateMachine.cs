namespace Pragma.StateMachine
{
    public interface IManageStateMachine<in T> where T : IStateMachine
    {
        public void SetMachine(T machine);
    }
}