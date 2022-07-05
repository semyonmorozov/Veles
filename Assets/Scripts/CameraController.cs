using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float cameraPositionX = -15;
    public float cameraPositionY = 20;
    public float cameraPositionZ = 0;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update() 
    {
        transform.position = player.transform.position + new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ);
    }

}
