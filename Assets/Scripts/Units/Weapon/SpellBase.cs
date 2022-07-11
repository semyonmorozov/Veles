using System.Collections;
using System.Linq;
using UnityEngine;

namespace Units.Weapon
{
    public enum CastingState
    {
        NonCasting,
        Casting,
        SuccessCast
    }

    public abstract class SpellBase : MonoBehaviour
    {
        protected virtual float Cooldown => 1;

        private bool isReloaded = true;

        private static readonly int CastingTriggerName = Animator.StringToHash("CastingState");
        private Coroutine prepareSpell;

        private Animator animator;

        protected void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void FinishCasting()
        {
            OnAttack();
            prepareSpell = null;
            StartCoroutine(StartCooldown());
        }

        private IEnumerator PrepareSpell()
        {
            yield return new WaitForSeconds(3);
            
            animator.SetInteger(CastingTriggerName, (int)CastingState.SuccessCast);
        }

        public void StartAttack()
        {
            if (prepareSpell != null || !isReloaded) 
                return;
            
            prepareSpell = StartCoroutine(PrepareSpell());
            animator.SetInteger(CastingTriggerName, (int)CastingState.Casting);
        }

        public void CancelAttack()
        {
            if (prepareSpell == null) 
                return;
            
            StopCoroutine(prepareSpell);
            animator.SetInteger(CastingTriggerName, (int)CastingState.NonCasting);
            prepareSpell = null;
        }

        protected abstract void OnAttack();

        private IEnumerator StartCooldown()
        {
            isReloaded = false;
            yield return new WaitForSeconds(Cooldown);
            isReloaded = true;
        }
    }
}