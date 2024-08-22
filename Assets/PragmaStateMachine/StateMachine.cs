using System;
using System.Collections.Generic;

namespace Pragma.StateMachine
{
    public class StateMachine<TMachine> : StateMachine where TMachine : IStateMachine
    {
        protected readonly TMachine machine;
        
        public StateMachine(List<IState> states, int indexDefaultState, TMachine machine) : base(states, indexDefaultState)
        {
            this.machine = machine;
            
            foreach (var state in states)
            {
                if (state is IManageStateMachine<TMachine> manageStateMachine)
                {
                    manageStateMachine.SetMachine(machine);
                }
            }
        }
    }
    
    public class StateMachine : IStateMachine
    {
        private List<IState> _states;

        private IState _currentState;
        private IState _lastState;
        private IState _defaultState;

        public event Action<IState> SwitchedStateEvent;
        public event Action TickStateEvent;
        
        public bool IsLockedState { get; set; }

        public StateMachine(List<IState> states, int indexDefaultState)
        {
            _states = states;
            
            foreach (var state in states)
            {
                state.Initialize();
            }
            
            _defaultState = states[indexDefaultState];
        }

        public void StartMachine()
        {
            SwitchToDefaultState();
        }

        public void StopMachine()
        {
            _currentState?.Stop();
        }

        public void TickState()
        {
            TickStateEvent?.Invoke();
        }

        public void SwitchToLastState() => SwitchState(_lastState);
        
        public void SwitchToDefaultState() => SwitchState(_defaultState);
        
        public void SwitchState(int indexState) => SwitchState(_states[indexState]);

        private void SwitchState(IState state) => SwitchState<object>(state);

        public void SwitchState<TState>(bool isRestartState = false) where TState : class, IState =>
            SwitchState<TState, object>(null, isRestartState);

        public void SwitchState<TState, TParam>(TParam param, bool isRestartState = false) where TState : class, IState
        {
            if (!IsPossibleToSwitch<TState>(out var state, isRestartState))
            {
                return;
            }
            
            SwitchState(state, param);
        }

        private void SwitchState<TParam>(IState state, TParam param = default)
        {
            if (_currentState != null)
            {
                _currentState.Stop();
            }

            _lastState = _currentState;

            _currentState = state;

            if (param != null && state is IRunWithParam<TParam> paramState)
            {
                paramState.Run(param);
            }
            else
            {
                state.Run();
            }
            
            SwitchedStateEvent?.Invoke(_currentState);
        }

        private bool IsPossibleToSwitch<T>(out T state, bool isRestartState = false) where T : IState
        {
            state = default;
            
            if (IsLockedState)
            {
                return false;
            }
            
            if (!isRestartState && _currentState is T)
            {
                return false;
            }

            state = GetState<T>();

            return state != null && state.IsPossibleToSwitch();
        }
        
        private T GetState<T>() where T : IState
        {
            foreach (var state in _states)
            {
                if (state is T found)
                {
                    return found;
                }
            }
            
            return default;
        }

        public void SetStatePreset<TState, TPreset>(TPreset preset) where TState : IState, IPresetState<TPreset>
        {
            var state = GetState<TState>();

            if (state == null)
            {
                return;
            }
            
            state.SetPreset(preset);
        }
    }
}
