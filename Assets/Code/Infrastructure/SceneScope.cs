using NewTankio.Code.Gameplay.Player;
using NewTankio.Code.Infrastructure.GameplayStates;
using NewTankio.Code.Services.CameraCaster;
using NewTankio.Code.Services.CloneService;
using NewTankio.Code.Services.CoordinateWrapper;
using NewTankio.Code.Services.MapBoundaries;
using NewTankio.Code.Tools.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace NewTankio.Code.Infrastructure
{
    public sealed class SceneScope : LifetimeScope
    {
        [SerializeField] private RectAreaMarker _mapArea;
        [SerializeField] private Camera _camera;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance(_camera);
            builder.Register<IMapBoundaries, MapBoundariesService>(Lifetime.Singleton).WithParameter(_mapArea);
            builder.Register<ICameraCaster, CameraCaster>(Lifetime.Singleton);
            builder.Register<ICoordinateWrapper, RectangleWrapper>(Lifetime.Singleton);
            builder.Register<ICloneService, CloneService>(Lifetime.Singleton);

            builder.Register<GameplayInitializationState>(Lifetime.Singleton);
            builder.Register<StateFactory>(Lifetime.Singleton);
            builder.Register<GameplayStateMachine>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayStartup>();
        }
    }
}
