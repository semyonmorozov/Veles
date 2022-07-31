using System.Collections;
using UnityEngine;

namespace Main.Units.Player.Weapon
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
        protected virtual float CastTime => 1;

        protected AudioClip CastSound;

        private bool isReloaded = true;

        private static readonly int CastingTriggerName = Animator.StringToHash("CastingState");
        private Coroutine prepareSpell;

        private Animator animator;
        private AudioSource audioSource;

        protected void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = GetComponent<UnitAttackSound>().AttackMixer;
        }

        public void OnCastingAnimationFinish()
        {
            prepareSpell = null;
        }

        public void OnSpellCasted()
        {
            DealSpell();
            StartCoroutine(StartCooldown());
            animator.SetInteger(CastingTriggerName, (int)CastingState.NonCasting);
        }

        private IEnumerator PrepareSpell()
        {
            if (CastTime != 0)
            {
                animator.SetInteger(CastingTriggerName, (int)CastingState.Casting);
                if (CastSound != null)
                    audioSource.PlayOneShot(CastSound);

                yield return new WaitForSeconds(CastTime);
            }

            animator.SetInteger(CastingTriggerName, (int)CastingState.SuccessCast);
        }

        public void StartAttack()
        {
            if (prepareSpell != null || !isReloaded)
                return;
            prepareSpell = StartCoroutine(PrepareSpell());
        }

        public void CancelAttack()
        {
            audioSource.Stop();
            animator.SetInteger(CastingTriggerName, (int)CastingState.NonCasting);
            
            if (prepareSpell == null)
                return;
            StopCoroutine(prepareSpell);
            prepareSpell = null;
        }

        protected abstract void DealSpell();

        private IEnumerator StartCooldown()
        {
            isReloaded = false;
            yield return new WaitForSeconds(Cooldown);
            isReloaded = true;
        }

        public void OnDestroy()
        {
            Destroy(audioSource);
        }
    }
}