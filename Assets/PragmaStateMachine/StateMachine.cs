using System;
using System.Collections.Generic;
using System.Linq;

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

        private readonly IState _defaultState;
        private IState _currentState;
        private IState _lastState;

        public event Action<IState> SwitchedStateEvent;
        public event Action TickStateEvent;

        public StateMachine(List<IState> states, int indexDefaultState)
        {
            _states = states.ToList();
            
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
            _currentState?.Exit();
        }

        public void TickState()
        {
            TickStateEvent?.Invoke();
        }
        
        public bool IsCurrentState<TState1>() where TState1 : IState
        {
            return _currentState is TState1;
        }
        
        public bool IsCurrentState<TState1, TState2>() where TState1 : IState where TState2 : IState
        {
            return _currentState is TState1 or TState2;
        }

        public bool IsCurrentState<TState1, TState2, TState3>() where TState1 : IState where TState2 : IState where TState3 : IState
        {
            return _currentState is TState1 or TState2 or TState3;
        }

        public SwitchStateResult SwitchToLastState()
        {
            return SwitchState(_lastState, true);
        }

        public SwitchStateResult SwitchToDefaultState()
        {
            return SwitchState(_defaultState, true);
        }

        public SwitchStateResult SwitchState(int indexState)
        {
            return SwitchState(_states[indexState], true);
        }

        private SwitchStateResult SwitchState(IState state, bool isRestartState = false)
        {
            return SwitchState<IState, object>(state, null, isRestartState);
        }

        public SwitchStateResult SwitchState<TState>(bool isRestartState = false) where TState : class, IState
        {
            return SwitchState<TState, object>(null, isRestartState);
        }
        
        public SwitchStateResult SwitchState<TState, TParam>(TParam param, bool isRestartState = false) where TState : class, IState
        {
            return SwitchState<TState, TParam>(null, param, isRestartState);
        }

        private SwitchStateResult SwitchState<TState, TParam>(TState state, TParam param, bool isRestartState) where TState : IState
        {
            state ??= GetState<TState>();
            
            var isAvailableToSwitch = IsAvailableToSwitch(state, isRestartState);

            if (!isAvailableToSwitch.IsCompleted())
            {
                return isAvailableToSwitch;
            }
            
            ProcessSwitchState(state, param);
            
            return isAvailableToSwitch;
        }

        private void ProcessSwitchState<TParam>(IState state, TParam param = default)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _lastState = _currentState;

            _currentState = state;

            if (param != null && state is IEnterWithParam<TParam> paramState)
            {
                paramState.Enter(param);
            }
            else
            {
                state.Enter();
            }
            
            SwitchedStateEvent?.Invoke(_currentState);
        }

        private SwitchStateResult IsAvailableToSwitch(IState state, bool isRestartState = false)
        {
            if (state == null)
            {
                return SwitchStateResult.NotFound;
            }

            if (!isRestartState && _currentState == state)
            {
                return SwitchStateResult.EqualCurrent;
            }

            if (!state.IsAvailable())
            {
                return SwitchStateResult.Reject;
            }

            return SwitchStateResult.Completed;
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

        public void SwitchOnAvailableState(bool isRestartState = false)
        {
            foreach (var state in _states)
            {
                if (!state.IsAvailable())
                {
                    continue;
                }

                SwitchState(state, isRestartState);
                return;
            }
        }
    }
}
