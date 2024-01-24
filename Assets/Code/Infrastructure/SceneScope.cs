using NewTankio.Code.Services.CameraCaster;
using NewTankio.Code.Services.CoordinateWrapper;
using NewTankio.Code.Services.MapBoundaries;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace NewTankio.Code.Infrastructure
{
    public sealed class SceneScope : LifetimeScope
    {
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private Camera _camera;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance(_camera);
            builder.RegisterInstance<IMapBoundaries>(new MapBoundariesService(_background.bounds));
            builder.Register<ICameraCaster, CameraCaster>(Lifetime.Singleton);
            builder.Register<ICoordinateWrapper, CoordinateWrapper>(Lifetime.Singleton);
        }
    }
}
