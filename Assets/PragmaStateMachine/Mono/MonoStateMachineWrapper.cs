using UnityEngine;

namespace Pragma.StateMachine
{
    public class MonoStateMachineWrapper : MonoBehaviour, IStateMachine
    {
        protected StateMachine machine;

        public void StartMachine() => machine.StartMachine();

        public void StopMachine() => machine.StopMachine();

        public SwitchStateResult SwitchState<T>(bool isRestartState = false) where T : class, IState =>
            machine.SwitchState<T>(isRestartState);

        public SwitchStateResult SwitchState<TState, TParam>(TParam param, bool isRestartState = false) where TState : class, IState
            => machine.SwitchState<TState, TParam>(param, isRestartState);

        public SwitchStateResult SwitchToLastState() => machine.SwitchToLastState();
        
        public SwitchStateResult SwitchToDefaultState() => machine.SwitchToDefaultState();
        
        public SwitchStateResult SwitchState(int indexState) => machine.SwitchToDefaultState();

        public void SetStatePreset<TState, TPreset>(TPreset preset) where TState : IState, IPresetState<TPreset>
            => machine.SetStatePreset<TState, TPreset>(preset);

        public void TickState() => machine.TickState();
    }
}