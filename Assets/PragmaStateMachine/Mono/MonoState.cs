using UnityEngine;

namespace Pragma.StateMachine
{
    public abstract class MonoState<TMachine> : MonoState, IManageStateMachine<TMachine> where TMachine : IStateMachine
    {
        protected TMachine machine;
        
        public void SetMachine(TMachine machine)
        {
            this.machine = machine;
        }
    }
    
    public abstract class MonoState : MonoBehaviour, IState
    {
        public virtual void Initialize()
        {
        }

        public bool IsPossibleToSwitch()
        {
            return true;
        }

        public virtual void Run()
        {
        }

        public virtual void Stop()
        {
        }
    }

    public abstract class PresetMonoState<TPreset, TMachine> : MonoState<TMachine>, IPresetState<TPreset>
        where TPreset : class where TMachine : IStateMachine
    {
        [SerializeField] private TPreset _defaultPreset;

        protected TPreset presetData;

        public override void Initialize()
        {
            presetData = _defaultPreset;
        }

        public void SetPreset(TPreset preset)
        {
            presetData = preset ?? _defaultPreset;
        }

        public void SetDefaultPreset()
        {
            presetData = _defaultPreset;
        }
    }
}