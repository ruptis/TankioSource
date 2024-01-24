using UnityEngine;
namespace NewTankio.Code.Tools
{
    public class DiagonalMover : MonoBehaviour
    {
        public float Speed;
    
        void Update()
        {
            transform.position += new Vector3(Speed, Speed, 0) * Time.deltaTime;
        }
    }
}
