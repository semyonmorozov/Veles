using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Units.Enemies
{
    public abstract class EnemyAIBase : MonoBehaviour
    {
        public int ContactDamage = 10;
        public int PlayerSearchingDistance = 15;
        
        public AudioClip[] MovingSounds;

        public EnemyState EnemyState = EnemyState.Calm;

        protected Transform playerTransform;
        protected GameObject playerGameObject;
        protected Health playerHealth;
        protected NavMeshAgent navAgent;
        protected Animator animator;
        private AudioSource movingAudioSource;
        private IEnumerator playMovingSounds;
        private bool movingSoundPlaying;


        protected virtual void Awake()
        {
            playerGameObject = GameObject.FindWithTag("Player");
            playerHealth = playerGameObject.GetComponent<Health>();
            playerTransform = playerGameObject.GetComponent<Transform>();
            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            
            movingAudioSource = gameObject.AddComponent<AudioSource>();
            movingAudioSource.outputAudioMixerGroup = GetComponent<EnemySounds>().Mixer;

            playMovingSounds = PlayMovingSounds();

            navAgent.stoppingDistance = GetAttackDistance() - 1;

            GlobalEventManager.PlayerDeath.AddListener(() => EnemyState = EnemyState.Calm);
        }

        protected virtual void FixedUpdate()
        {
            if (playerHealth.IsDead())
            {
                StandStill();
                return;
            }
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
            if (movingSoundPlaying)
            {
                movingSoundPlaying = false;
                StopCoroutine(playMovingSounds);
            }
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
            if (!movingSoundPlaying)
            {
                movingSoundPlaying = true;
                StartCoroutine(playMovingSounds);
            }
            navAgent.destination = playerTransform.position;
        }

        protected abstract int GetAttackDistance();

        private IEnumerator PlayMovingSounds()
        {
            while (true)
            {
                if (MovingSounds.Length > 0)
                {
                    movingAudioSource.pitch = Random.Range(0.9f, 1.1f);
                    var randomMovingSound = MovingSounds[Random.Range(0,MovingSounds.Length)];
                    Debug.Log(randomMovingSound.length);
                    movingAudioSource.PlayOneShot(randomMovingSound);
                    yield return new WaitForSeconds(randomMovingSound.length);
                }
                yield return new WaitForSeconds(Random.Range(0,2));
            }
        }
    }

    public enum EnemyState
    {
        Calm,
        Chasing,
        Attack,
        Dead
    }
}