namespace Pragma.StateMachine
{
    public interface IPresetState<in TPreset>
    {
        public void SetPreset(TPreset preset)
        {
        }

        public void SetDefaultPreset()
        {
        }
    }
}