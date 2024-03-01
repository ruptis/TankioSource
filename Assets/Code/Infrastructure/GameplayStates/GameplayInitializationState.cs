using NewTankio.Code.Services;
using NewTankio.Code.Services.MapBoundaries;
using NewTankio.Code.Tools.StateMachine;
namespace NewTankio.Code.Infrastructure.GameplayStates
{
    public class GameplayInitializationState : IState
    {
        private readonly IBoundariesProvider _boundariesProvider;
        private readonly IMapBoundaries _mapBoundaries;
        
        public GameplayInitializationState(IBoundariesProvider boundariesProvider, IMapBoundaries mapBoundaries)
        {
            _boundariesProvider = boundariesProvider;
            _mapBoundaries = mapBoundaries;
        }

        public void Exit()
        {
            
        }
        public void Enter()
        {
          
        }
    }
}
