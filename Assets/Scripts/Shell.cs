using System;
using Unit;
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

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        if (target.CompareTag("Enemy"))
        {
            target.GetComponent<EnemyHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * (moveSpeed * Time.fixedDeltaTime);
    }
}
