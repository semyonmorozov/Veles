using Units.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class SowBallProjectile : MonoBehaviour
{
    public int moveSpeed = 200;
    public int lifeTimeSeconds = 2;
    public int damage = 10;
    public float force = 25000;

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
            target.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * (moveSpeed * Time.fixedDeltaTime);
    }
}
