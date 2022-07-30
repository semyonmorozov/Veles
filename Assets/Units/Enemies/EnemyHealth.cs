using UnityEngine;

namespace Units.Enemies
{
    public class EnemyHealth : Health
    {
        public AudioClip DeadSound;
        public int EnemyMaxHealth = 100;
        private EnemyAIBase enemyAI;
        private new Collider collider;
        private AudioSource deathAudioSource;

        public override int MaxHealth => EnemyMaxHealth;

        protected override void Awake()
        {
            base.Awake();
            enemyAI = GetComponent<EnemyAIBase>();
            collider = GetComponent<Collider>();
            deathAudioSource = gameObject.AddComponent<AudioSource>();
            deathAudioSource.outputAudioMixerGroup = GetComponent<UnitStateSounds>().StateMixer;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if(enemyAI.EnemyState != EnemyState.Dead)
                enemyAI.EnemyState = EnemyState.Chasing;
        }

        protected override void OnDeath()
        {
            GlobalEventManager.EnemyDeath.Invoke(transform);
            enemyAI.EnemyState = EnemyState.Dead;
            collider.isTrigger = true;
            deathAudioSource.pitch = Random.Range(0.9f, 1.1f);
            deathAudioSource.PlayOneShot(DeadSound);
        }

        public void DeathAnimationEnded()
        {
            Destroy(gameObject);
        }
    }
}