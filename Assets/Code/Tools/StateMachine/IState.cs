namespace NewTankio.Code.Tools.StateMachine
{
    public interface IState : IExitable
    {
        public void Enter();
    }
}
