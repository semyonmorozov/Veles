using UnityEngine;

namespace Units.Enemies
{
    public class EnemyHealth : Health
    {
        private Animator animator;
        private EnemyAIBase enemyAI;
        private new Collider collider;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            enemyAI = GetComponent<EnemyAIBase>();
            collider = GetComponent<Collider>();
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
        }

        public void DeathAnimationEnded()
        {
            Destroy(gameObject);
        }
    }
}