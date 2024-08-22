namespace Pragma.StateMachine
{
    public interface IStateMachine
    {
        public void StartMachine();
        public void StopMachine();
        public void SwitchState<TState>(bool isRestartState = false) where TState : class, IState;
        public void SwitchState<TState, TParam>(TParam param, bool isRestartState = false) where TState : class, IState;
        public void SwitchToLastState();
        public void SwitchToDefaultState();
        public void SwitchState(int indexState);
        public void SetStatePreset<TState, TPreset>(TPreset preset) where TState : IState, IPresetState<TPreset>;
        public void TickState();
    }
}