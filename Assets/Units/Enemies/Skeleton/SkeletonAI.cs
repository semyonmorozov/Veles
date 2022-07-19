using System.Collections;
using UnityEngine;

namespace Units.Enemies.Skeleton
{
    public class SkeletonAI : EnemyAIBase
    {
        public int MeleeAttackCooldown = 1;

        public int AttackDistance = 3;
        protected override int GetAttackDistance() => AttackDistance;
        
        private bool meleeAttackIsReady;
        private UnitAttackSound unitAttackSound;

        protected override void Awake()
        {
            base.Awake();
            meleeAttackIsReady = true;
            unitAttackSound = GetComponent<UnitAttackSound>();
        }

        protected override void FixedUpdate()
        {
            if (EnemyState == EnemyState.Chasing && meleeAttackIsReady)
            {
                if (PlayerInAttackDistance())
                {
                    EnemyState = EnemyState.Attack;
                    animator.SetTrigger("Attack");
                    StartCoroutine(StartCooldownOfMeleeAttack());
                }
            }
            base.FixedUpdate();
        }

        private bool PlayerInAttackDistance()
        {
            return Vector3.Distance(playerTransform.position, transform.position) <= GetAttackDistance();
        }

        public void AttackAnimationFinished()
        {
            unitAttackSound.PlayAttackSound();
            
            if (PlayerInAttackDistance())
            {
                playerHealth.TakeDamage(10);
            }

            EnemyState = EnemyState.Chasing;
        }
        
        

        private IEnumerator StartCooldownOfMeleeAttack()
        {
            meleeAttackIsReady = false;
            yield return new WaitForSeconds(MeleeAttackCooldown);
            meleeAttackIsReady = true;
        }
    }
}