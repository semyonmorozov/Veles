using System;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int moveSpeed = 200;
    public int lifeTimeSeconds = 2;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start ()
    {
        Destroy (gameObject, lifeTimeSeconds);
    }
    
    private void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * (moveSpeed * Time.fixedDeltaTime);
        
        var forward = transform.TransformDirection(Vector3.forward) * 1;
        Debug.DrawRay(transform.position, forward, Color.red);
    }
}
