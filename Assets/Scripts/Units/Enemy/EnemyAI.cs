using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Units.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        public int ContactDamage = 10;
        public int ContactDamageDelay = 1;
    
        public EnemyState EnemyState = EnemyState.Attack;

        private Rigidbody enemyRigidbody;
        private Transform playerTransform;
        private GameObject playerGameObject;
        private Health health;
        private readonly IEnumerator dealContactDamageCoroutine;
        private NavMeshAgent navAgent;

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
            navAgent = GetComponent<NavMeshAgent>();

            GlobalEventManager.PlayerDeath.AddListener(() => EnemyState = EnemyState.Calm);
        }

        private void FixedUpdate()
        {
            if (EnemyState == EnemyState.Attack)
            {
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
                health.TakeDamage(ContactDamage);
                yield return new WaitForSeconds(ContactDamageDelay);
            }
        }

        private void MoveToPlayer()
        {
            navAgent.destination = playerTransform.position;
        }
    }

    public enum EnemyState
    {
        Calm,
        Attack
    }
}