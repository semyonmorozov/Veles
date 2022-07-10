using Units.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class SnowBallProjectile : MonoBehaviour
{
    public int MoveSpeed = 800;
    public int LifeTimeSeconds = 2;
    public int Damage = 10;
    public float Force = 3;
    public float Size = 3;

    private new Rigidbody rigidbody;
    private new Transform transform;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        transform.localScale = new Vector3(Size, Size, Size);
    }

    private void Start ()
    {
        Destroy (gameObject, LifeTimeSeconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        if (target.CompareTag("Enemy"))
        {
            target.GetComponent<EnemyHealth>().TakeDamage(Damage);
            
            var dir = rigidbody.velocity.normalized;
            target.GetComponent<Rigidbody>().AddForce(dir*Force);
            
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * (MoveSpeed * Time.fixedDeltaTime);
    }
}
