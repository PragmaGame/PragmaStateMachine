namespace Pragma.StateMachine
{
    public enum SwitchStateResult
    {
        Completed = 0,
        Locked = 1,
        EqualCurrent = 2,
        NotFound = 3,
        Reject = 4,
    }
}