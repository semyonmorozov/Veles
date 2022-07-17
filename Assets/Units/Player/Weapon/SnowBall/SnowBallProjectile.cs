using System;
using System.Collections;
using Units.Enemies;
using Units.Enemy;
using UnityEngine;

namespace Units.Player.Weapon.SnowBall
{
    public class SnowBallProjectile : MonoBehaviour
    {
        public float MoveSpeed = 800;
        public float LifeTimeSeconds = 2;
        public int Damage = 10;
        public float Force = 3;
        public float Size = 3;

        private new Rigidbody rigidbody;
        private new Transform transform;
        private Vector3 throwDirection;
        private new Collider collider;
        private Animator animator;
        private Vector3 startScale;
        private Vector3 onePercentOfScale;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            transform = GetComponent<Transform>();
            collider = GetComponent<Collider>();
            
            startScale = new Vector3(Size, Size, Size);
            transform.localScale = startScale;
            onePercentOfScale = startScale / 100f;
            
            throwDirection = transform.forward;
        }

        private void FixedUpdate()
        {
            var localScale = transform.localScale;
            if (localScale.x <= 0)
            {
                Destroy(gameObject);
                return;
            }
            transform.localScale = localScale - (onePercentOfScale * (20 * Time.fixedDeltaTime));
        }

        private void Start ()
        {
            rigidbody.AddForce(throwDirection*Force*10);

            StartCoroutine(EnableCollision());
        }

        private void OnCollisionEnter(Collision other)
        {
            var target = other.gameObject;
        
            if (!target.CompareTag("Enemy"))
                return;
            target.GetComponent<EnemyHealth>().TakeDamage(Damage);
        }
    
        private IEnumerator EnableCollision()
        {
            yield return new WaitForSeconds(0.1f);
            collider.isTrigger = false;
        }
    }
}
