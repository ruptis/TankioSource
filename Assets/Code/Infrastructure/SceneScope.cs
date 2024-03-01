using NewTankio.Code.Infrastructure.GameplayStates;
using NewTankio.Code.Services;
using NewTankio.Code.Services.CameraCaster;
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
        [SerializeField] private Transform _mapTransform;
        [SerializeField] private MapData _mapData;
        [SerializeField] private Camera _camera;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("SceneScope Configure");
            base.Configure(builder);
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_mapData.BoundariesStaticData);
            builder.Register<ITransformProvider, MapTransformProvider>(Lifetime.Singleton).WithParameter(_mapTransform);
            builder.Register<IBoundariesProvider, BoundariesProvider>(Lifetime.Singleton).As<ITickable>();
            builder.Register<IMapBoundaries, MapBoundaries>(Lifetime.Singleton);
            builder.Register<ICameraCaster, CameraCaster>(Lifetime.Singleton);
            builder.Register<ICoordinateWrapper, CoordinateWrapper>(Lifetime.Singleton);

            builder.Register<GameplayInitializationState>(Lifetime.Singleton);
            builder.Register<StateFactory>(Lifetime.Singleton);
            builder.Register<GameplayStateMachine>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayStartup>();
            Debug.Log("SceneScope Configure end");
        }
    }
}
