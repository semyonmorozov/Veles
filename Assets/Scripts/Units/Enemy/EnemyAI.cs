using System.Collections;
using UnityEngine;

namespace Units.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        public enum EnemyState
        {
            Calm,
            Attack
        }

        public int moveSpeed = 1;
        public int rotationSpeed = 1;
        public int contactDamage = 10;
        public int contactDamageDelay = 1;
    
        public EnemyState enemyState = EnemyState.Attack;

        private Rigidbody enemyRigidbody;
        private Transform playerTransform;
        private GameObject playerGameObject;
        private Health health;
        private readonly IEnumerator dealContactDamageCoroutine;

        public EnemyAI()
        {
            dealContactDamageCoroutine = DealContactDamage();
        }

        private void Awake()
        {
            enemyRigidbody = GetComponent<Rigidbody>();
            playerGameObject = GameObject.FindWithTag("Player");
            health = playerGameObject.GetComponent<Health>();
            playerTransform = playerGameObject.GetComponent<Transform>();

            GlobalEventManager.PlayerDeath.AddListener(() => enemyState = EnemyState.Calm);
        }

        private void FixedUpdate()
        {
            if (enemyState == EnemyState.Attack)
            {
                LookAtPlayer();
                MoveToPlayer();
                DrawForwardRay();
            }
        }
        private void DrawForwardRay()
        {
            var enemyTransform = transform;
            var forward = enemyTransform.TransformDirection(Vector3.forward) * 2;
            Debug.DrawRay(enemyTransform.position, forward, Color.red);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collisionGameObject = collision.gameObject;
            if (collisionGameObject.CompareTag("Player"))
            {
                StartCoroutine(dealContactDamageCoroutine);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var collisionGameObject = collision.gameObject;
            if (collisionGameObject.CompareTag("Player"))
            {
                StopCoroutine(dealContactDamageCoroutine);
            }
        }

        private IEnumerator DealContactDamage()
        {
            while (true)
            {
                health.TakeDamage(contactDamage);
                yield return new WaitForSeconds(contactDamageDelay);
            }
        }

        private void MoveToPlayer()
        {
            enemyRigidbody.velocity = transform.forward * (moveSpeed * Time.fixedDeltaTime);
        }

        private void LookAtPlayer()
        {
            var lookRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
            lookRotation.z = 0;
            lookRotation.x = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}