using System.Collections.Generic;
using UnityEngine;

namespace Pragma.StateMachine
{
    public abstract class MonoStateMachineSerializeReferenceStates<TMachine> : MonoStateMachine<TMachine> where TMachine : IStateMachine
    {
        [SerializeReference] protected List<IState> states;
        [SerializeField] protected int defaultStateIndex;

        protected override List<IState> GetStates() => states;
        protected override int GetDefaultStateIndex() => defaultStateIndex;
    }

    public class MonoStateMachineSerializeReferenceStates : MonoStateMachineSerializeReferenceStates<MonoStateMachineSerializeReferenceStates>
    {
    }
}