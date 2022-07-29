using UnityEngine;

namespace MainCamera
{
    public class CameraController : MonoBehaviour
    {
        public Transform Player;
        public float CameraPositionX = -15;
        public float CameraPositionY = 25;
        public float CameraPositionZ = 0;

        private void Awake()
        {
            Player = GameObject.FindWithTag("Player").transform;
        }

        private void Update() 
        {
            transform.position = Player.transform.position + new Vector3(CameraPositionX, CameraPositionY, CameraPositionZ);
        }

    }
}
