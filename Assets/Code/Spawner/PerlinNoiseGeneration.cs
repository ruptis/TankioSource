using System.Collections.Generic;
using UnityEngine;
namespace NewTankio.Code.Spawner
{
    public static class PerlinNoiseGeneration
    {
        public static IEnumerable<Vector2> GeneratePoints(float radius, Vector2 boundsSize)
        {
            var points = new List<Vector2>();
            var grid = new Vector2Int((int)(boundsSize.x / radius), (int)(boundsSize.y / radius));
            for (var x = 0; x < grid.x; x++)
            {
                for (var y = 0; y < grid.y; y++)
                {
                    var point = new Vector2(x, y) * radius;
                    var noise = Mathf.PerlinNoise(point.x, point.y);
                    if (noise > 0.5f)
                    {
                        points.Add(point);
                    }
                }
            }
            return points;
        }
    }
}
