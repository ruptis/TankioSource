namespace NewTankio.Code.Tools.StateMachine
{
    public interface IPayloadedState<in TPayload> : IExitable
    {
        public void Enter(TPayload payload);
    }
}
