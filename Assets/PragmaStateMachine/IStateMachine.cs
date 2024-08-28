using System;

namespace Pragma.StateMachine
{
    public interface IStateMachine
    {
        public event Action<IState> SwitchedStateEvent;
        public event Action TickStateEvent;
        public void StartMachine();
        public void StopMachine();
        public SwitchStateResult SwitchState<TState>(bool isRestartState = false) where TState : class, IState;
        public SwitchStateResult SwitchState<TState, TParam>(TParam param, bool isRestartState = false) where TState : class, IState;
        public SwitchStateResult SwitchToLastState();
        public SwitchStateResult SwitchToDefaultState();
        public SwitchStateResult SwitchState(int indexState);
        public void SetStatePreset<TState, TPreset>(TPreset preset) where TState : IState, IPresetState<TPreset>;
        public void TickState();
        public bool IsCurrentState<TState1>() where TState1 : IState;
        public bool IsCurrentState<TState1, TState2>() where TState1 : IState where TState2 : IState;
        public bool IsCurrentState<TState1, TState2, TState3>() where TState1 : IState where TState2 : IState where TState3 : IState;
        public void SwitchOnAvailableState(bool isRestartState = false);
    }
}