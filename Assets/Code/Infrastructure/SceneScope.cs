using NewTankio.Code.Services;
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

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance<IMapBoundaries>(new MapBoundariesService(_background.bounds));
            builder.Register<ICoordinateWrapper, RectangleWrapper>(Lifetime.Singleton);
        }
    }
}
