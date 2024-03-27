using System;
using NewTankio.Code.Gameplay.Player;
using UnityEngine;
namespace NewTankio.Code.Services.MapBoundaries
{
    public sealed class MapBoundariesService : IMapBoundaries
    {
        private readonly RectAreaMarker _rectArea;
        
        public MapBoundariesService(RectAreaMarker rectArea) => 
            _rectArea = rectArea;

        public int BoundariesCount => 4;
        public Vector2 MinPoint => _rectArea.Rect.min;
        public Vector2 MaxPoint => _rectArea.Rect.max;
        public Vector2 Size => _rectArea.Rect.size;
        public Vector2 Center => _rectArea.Rect.center;

        public int OverlapBoundaries(in Rect rect, Span<Vector2> directions)
        {
            var count = 0;

            if (rect.xMin < MinPoint.x)
                directions[count++] = Vector2.left;
            if (rect.yMin < MinPoint.y)
                directions[count++] = Vector2.down;
            if (rect.xMax > MaxPoint.x)
                directions[count++] = Vector2.right;
            if (rect.yMax > MaxPoint.y)
                directions[count++] = Vector2.up;

            return count;
        }
    }
}
