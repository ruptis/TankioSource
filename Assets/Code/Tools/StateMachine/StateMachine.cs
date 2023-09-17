using System;
using UnityEngine;
namespace NewTankio.Code.Tools.StateMachine
{
    public abstract class StateMachine<TStateType>
    {
        private readonly IStateFactory<TStateType> _stateFactory;

        protected StateMachine(IStateFactory<TStateType> stateFactory)
        {
            _stateFactory = stateFactory;
        }
        protected IExitable CurrentState { get; private set; }

        public Type CurrentStateType => CurrentState?.GetType();

        public void Enter<TState>() where TState : class, TStateType, IState
        {
            ChangeState(out TState state);
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, TStateType, IPayloadedState<TPayload>
        {
            ChangeState(out TState state);
            state.Enter(payload);
        }

        private void ChangeState<TState>(out TState state) where TState : class, TStateType, IExitable
        {
            CurrentState?.Exit();
            Debug.Log($"Changing state from {CurrentStateType?.ToString() ?? "Start"} to {typeof(TState)}");
            state = _stateFactory.GetState<TState>();
            CurrentState = state;
        }
    }

}
