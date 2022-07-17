using UnityEngine;
using UnityEngine.AI;

namespace Units.Enemies
{
    public abstract class EnemyAIBase : MonoBehaviour
    {
        public int ContactDamage = 10;
        public int PlayerSearchingDistance = 15;

        public EnemyState EnemyState = EnemyState.Calm;

        protected Transform playerTransform;
        protected GameObject playerGameObject;
        protected Health playerHealth;
        protected NavMeshAgent navAgent;
        protected Animator animator;


        protected virtual void Awake()
        {
            playerGameObject = GameObject.FindWithTag("Player");
            playerHealth = playerGameObject.GetComponent<Health>();
            playerTransform = playerGameObject.GetComponent<Transform>();
            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            navAgent.stoppingDistance = GetAttackDistance() - 1;

            GlobalEventManager.PlayerDeath.AddListener(() => EnemyState = EnemyState.Calm);
        }

        protected virtual void FixedUpdate()
        {
            switch (EnemyState)
            {
                case EnemyState.Calm:
                    StandStill();
                    SearchPlayer();
                    animator.SetTrigger("Idle");
                    break;
                case EnemyState.Chasing:
                    MoveToPlayer();
                    animator.SetTrigger("Moving");
                    break;
                case EnemyState.Attack:
                case EnemyState.Dead:
                    StandStill();
                    break;
            }
        }

        private void StandStill()
        {
            navAgent.destination = transform.position;
        }

        protected virtual void SearchPlayer()
        {
            var distance = Vector3.Distance(playerTransform.position,transform.position);
            if (distance <= PlayerSearchingDistance && EnemyState != EnemyState.Dead)
            {
                EnemyState = EnemyState.Chasing;
            }
        }
        
        protected virtual void MoveToPlayer()
        {
            navAgent.destination = playerTransform.position;
        }

        protected abstract int GetAttackDistance();
    }

    public enum EnemyState
    {
        Calm,
        Chasing,
        Attack,
        Dead
    }
}