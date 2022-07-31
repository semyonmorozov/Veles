using System.Collections;
using Main.Units.Enemies;
using UnityEngine;

namespace Main.Units.Player.Weapon.FireBall
{
    public class FireBallProjectile : MonoBehaviour
    {
        public float MoveSpeed = 1;
        public float LifeTimeSeconds = 2;
        public int Damage = 10;
        public float Size = 1;

        private new Rigidbody rigidbody;
        private new Transform transform;
        private Animator animator;
        private Vector3 startScale;
        private Vector3 onePercentOfScale;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            transform = GetComponent<Transform>();

            startScale = new Vector3(Size, Size, Size);
            transform.localScale = startScale;
        }

        private void Start()
        {
            StartCoroutine(DestroyAfterSeconds(LifeTimeSeconds));
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = transform.forward * (MoveSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject;

            if (!target.CompareTag("Enemy"))
                return;
            target.GetComponent<EnemyHealth>().TakeDamage(Damage);
            StartCoroutine(DestroyAfterSeconds(0.3f));
        }

        private IEnumerator DestroyAfterSeconds(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}