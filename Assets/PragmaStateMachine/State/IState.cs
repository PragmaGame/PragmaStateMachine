namespace Pragma.StateMachine
{
    public interface IState
    {
        public void Initialize();
        public bool IsAvailable();
        public void Enter();
        public void Exit();
    }
}