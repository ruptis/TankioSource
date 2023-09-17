using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public static class BoundsExtension
    {
        public static bool IsBoundsInside(this Bounds outerBounds, in Bounds innerBounds)
        {
            return outerBounds.Contains(innerBounds.min) && outerBounds.Contains(innerBounds.max);
        }

        public static Vector2 ClosestPointFromInside(this Bounds bounds, in Vector2 point)
        {
            var xDistance = Mathf.Min(Mathf.Abs(bounds.min.x - point.x), Mathf.Abs(bounds.max.x - point.x));
            var yDistance = Mathf.Min(Mathf.Abs(bounds.min.y - point.y), Mathf.Abs(bounds.max.y - point.y));

            return xDistance < yDistance ? new Vector2(point.x < bounds.center.x ? bounds.min.x : bounds.max.x, point.y) 
                : new Vector2(point.x, point.y < bounds.center.y ? bounds.min.y : bounds.max.y);
        }
    }
}
