﻿using System;
using UnityEngine;
namespace NewTankio.Code.Services.MapBoundaries
{
    public sealed class MapBoundariesService : IMapBoundaries
    {
        public MapBoundariesService(Bounds bounds)
        {
            MinPoint = bounds.min;
            MaxPoint = bounds.max;
        }

        public int BoundariesCount => 4;
        public Vector2 MinPoint { get; }
        public Vector2 MaxPoint { get; }

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
