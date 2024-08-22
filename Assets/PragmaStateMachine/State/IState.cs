namespace Pragma.StateMachine
{
    public interface IState
    {
        public void Initialize();
        public bool IsPossibleToSwitch();
        public void Run();
        public void Stop();
    }
}