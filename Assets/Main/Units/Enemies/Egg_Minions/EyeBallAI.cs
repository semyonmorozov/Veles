using System.Collections;
using UnityEngine;

namespace Main.Units.Enemies.Egg_Minions
{
    public class EyeBallAI : EnemyAIBase
    {
        public int MeleeAttackCooldown = 1;
        private bool meleeAttackIsReady = true;

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
            playerHealth.TakeDamage(AttackDamage);
            StartCoroutine(StartCooldownOfMeleeAttack());
        }

        private IEnumerator StartCooldownOfMeleeAttack()
        {
            meleeAttackIsReady = false;
            yield return new WaitForSeconds(MeleeAttackCooldown);
            meleeAttackIsReady = true;
        }

        protected override int GetAttackDistance() => 0;
    }
}