using System.Collections.Generic;

namespace Pragma.StateMachine
{
    public abstract class MonoStateMachine<TMachine> : MonoStateMachineWrapper where TMachine : IStateMachine
    {
        protected abstract List<IState> GetStates();
        protected abstract int GetDefaultStateIndex();
        
        protected virtual void Awake()
        {
            if (this is not TMachine convertMachine)
            {
                return;
            }
            
            machine = new StateMachine<TMachine>(GetStates(), GetDefaultStateIndex(), convertMachine);
        }

        protected virtual void Update()
        {
            machine.TickState();
        }
    }
}