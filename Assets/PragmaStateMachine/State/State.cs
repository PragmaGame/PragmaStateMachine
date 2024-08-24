namespace Pragma.StateMachine
{
    public abstract class State<TMachine> : IState, IManageStateMachine<TMachine> where TMachine : IStateMachine
    {
        protected TMachine machine;

        public void SetMachine(TMachine machine)
        {
            this.machine = machine;
        }
        
        public virtual void Initialize()
        {
        }

        public virtual bool IsAvailable()
        {
            return true;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}