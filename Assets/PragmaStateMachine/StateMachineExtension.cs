namespace Pragma.StateMachine
{
    public static class StateMachineExtension
    {
        public static bool IsCompleted(this SwitchStateResult result)
        {
            return result == SwitchStateResult.Completed;
        }
    }
}