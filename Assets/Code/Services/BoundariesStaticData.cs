using System;
using System.Collections.Generic;
using UnityEngine;
namespace NewTankio.Code.Services
{
    [Serializable]
    public sealed class BoundariesStaticData
    {
        public List<Vector3> LocalBoundaryPositions = new();
        public List<Vector3> LocalBoundaryNormals = new();
    }
}