﻿using NewTankio.Code.Services.CoordinateWrapper;
using UnityEngine;
using VContainer;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class TransformWrapper : MonoBehaviour
    {
        private ICoordinateWrapper _coordinateWrapper;

        [Inject]
        public void Construct(ICoordinateWrapper coordinateWrapper) 
            => _coordinateWrapper = coordinateWrapper;

        private void Update()
        {
            //transform.position = _coordinateWrapper.WrapPoint(transform.position);
            Vector3 newPosition = _coordinateWrapper.WrapPoint(transform.position);

            if (transform.position == newPosition)
                return;
            
            Debug.Log("Wrap");
            transform.position = newPosition;
        }
    }
}
