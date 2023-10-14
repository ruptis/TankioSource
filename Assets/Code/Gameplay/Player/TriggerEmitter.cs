using System;
using UnityEngine;
namespace NewTankio.Code.Gameplay.Player
{
    public abstract class TriggerEmitter : MonoBehaviour
    {
        public Action<Collider2D, Collider2D> TriggerEntered;
        public Action<Collider2D, Collider2D> TriggerStayed;
        public Action<Collider2D, Collider2D> TriggerExited;
    }
}
