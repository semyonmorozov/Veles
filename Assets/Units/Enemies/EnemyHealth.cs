using UnityEngine;

namespace Units.Enemies
{
    public class EnemyHealth : Health
    {
        public AudioClip DeadSound;
        private Animator animator;
        private EnemyAIBase enemyAI;
        private new Collider collider;
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            enemyAI = GetComponent<EnemyAIBase>();
            collider = GetComponent<Collider>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = GetComponent<EnemySounds>().Mixer;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            animator.SetTrigger("Damage");
            if(enemyAI.EnemyState != EnemyState.Dead)
                enemyAI.EnemyState = EnemyState.Chasing;
        }

        protected override void OnDeath()
        {
            GlobalEventManager.EnemyDeath.Invoke(transform);
            enemyAI.EnemyState = EnemyState.Dead;
            collider.isTrigger = true;
            animator.SetTrigger("Die");
            audioSource.PlayOneShot(DeadSound);
        }

        public void DeathAnimationEnded()
        {
            Destroy(gameObject);
        }
    }
}