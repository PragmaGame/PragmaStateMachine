using System;
using UnityEngine;

namespace Pragma.StateMachine
{
    [Serializable]
    public abstract class PresetState<TPreset, TMachine> : State<TMachine>, IPresetState<TPreset> 
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