namespace NewTankio.Tools.StateMachine
{
    public interface IStateFactory<in TStateType>
    {
        public TState GetState<TState>() where TState : class, TStateType, IExitable;
    }
}
