using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Units.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        public int ContactDamage = 10;
        public int MeleeAttackCooldown = 1;
        public int PlayerSearchingDistance = 15;

        public EnemyState EnemyState = EnemyState.Calm;

        private Transform playerTransform;
        private GameObject playerGameObject;
        private Health playerHealth;
        private NavMeshAgent navAgent;
        private Animator animator;

        private bool meleeAttackIsReady = true;


        private void Awake()
        {
            playerGameObject = GameObject.FindWithTag("Player");
            playerHealth = playerGameObject.GetComponent<Health>();
            playerTransform = playerGameObject.GetComponent<Transform>();
            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            GlobalEventManager.PlayerDeath.AddListener(() => EnemyState = EnemyState.Calm);
        }

        private void FixedUpdate()
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
                case EnemyState.Dead:
                    StandStill();
                    break;
            }
        }

        private void StandStill()
        {
            navAgent.destination = transform.position;
        }

        private void SearchPlayer()
        {
            var distance = Vector3.Distance(playerTransform.position,transform.position);
            if (distance <= PlayerSearchingDistance && EnemyState != EnemyState.Dead)
            {
                EnemyState = EnemyState.Chasing;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            MakeMeleeAttack(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            MakeMeleeAttack(collision);
        }

        private void MakeMeleeAttack(Collision collision)
        {
            var collisionGameObject = collision.gameObject;
            if (
                !collisionGameObject.CompareTag("Player")
                || EnemyState != EnemyState.Chasing
                || !meleeAttackIsReady
            )
                return;
            
            animator.SetTrigger("Attack");
            playerHealth.TakeDamage(ContactDamage);
            StartCoroutine(StartCooldownOfMeleeAttack());
        }

        private IEnumerator StartCooldownOfMeleeAttack()
        {
            meleeAttackIsReady = false;
            yield return new WaitForSeconds(MeleeAttackCooldown);
            meleeAttackIsReady = true;
        }

        private void MoveToPlayer()
        {
            navAgent.destination = playerTransform.position;
        }
    }

    public enum EnemyState
    {
        Calm,
        Chasing,
        Dead
    }
}