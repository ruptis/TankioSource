﻿using System;
using UnityEngine;
namespace NewTankio.Code.Services.MapBoundaries
{
    public interface IMapBoundaries
    {
        public int BoundariesCount { get; }
        public Vector2 MinPoint { get; }
        public Vector2 MaxPoint { get; }
        Vector2 Size { get; }
        Vector2 Center { get; }

        public int OverlapBoundaries(in Rect rect, Span<Vector2> directions);
    }
}
