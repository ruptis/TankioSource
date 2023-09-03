using System;
using System.Collections.Generic;
using NewTankio.Tools.StateMachine;
namespace NewTankio.Infrastructure.GameStates
{
    public sealed class GameStateFactory : IStateFactory<IGameState>
    {
        private readonly Dictionary<Type, IGameState> _gameStates;
        public GameStateFactory(BootstrapGameState bootstrapState)
        {
            _gameStates = new Dictionary<Type, IGameState>
            {
                {
                    typeof(BootstrapGameState), bootstrapState
                }
            };
        }

        public TState GetState<TState>() where TState : class, IGameState, IExitable
        {
            return _gameStates[typeof(TState)] as TState;
        }
    }
}
