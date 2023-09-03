namespace NewTankio.Tools.StateMachine
{
    public interface IState : IExitable
    {
        public void Enter();
    }
}
