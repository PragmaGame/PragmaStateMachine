using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pragma.StateMachine
{
    public abstract class MonoStateMachineMonoStates<TMachine> : MonoStateMachine<TMachine> where TMachine : IStateMachine
    {
        [SerializeField] protected List<MonoState> states;
        [SerializeField] protected int defaultStateIndex;

        protected override List<IState> GetStates() => states.OfType<IState>().ToList();
        protected override int GetDefaultStateIndex() => defaultStateIndex;
    }

    public class MonoStateMachineMonoStates : MonoStateMachineMonoStates<MonoStateMachineMonoStates>
    {
    }
}