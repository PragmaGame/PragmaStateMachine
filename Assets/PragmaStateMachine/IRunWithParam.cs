namespace Pragma.StateMachine
{
    public interface IRunWithParam<in TParam>
    {
        public void Run(TParam param);
    }
}