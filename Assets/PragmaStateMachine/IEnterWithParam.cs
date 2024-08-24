namespace Pragma.StateMachine
{
    public interface IEnterWithParam<in TParam>
    {
        public void Enter(TParam param);
    }
}