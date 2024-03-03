﻿using System;
using System.Collections.Generic;
using NewTankio.Code.Tools.StateMachine;
using VContainer;
namespace NewTankio.Code.Infrastructure.GameStates
{
    public sealed class GameStateFactory : IStateFactory<IGameState>
    {
        private readonly IObjectResolver _resolver;
        
        public GameStateFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public TState GetState<TState>() where TState : class, IGameState, IExitable
        {
            return _resolver.Resolve<TState>();
        }
    }
}