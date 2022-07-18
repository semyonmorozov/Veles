using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Units.Player.Weapon.SnowBall
{
    public enum CastingState
    {
        NonCasting,
        Casting,
        SuccessCast
    }

    public abstract class SpellBase : MonoBehaviour
    { protected virtual float Cooldown => 1;
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
            audioSource.outputAudioMixerGroup = GetComponent<PlayerSounds>().SpellMixer;
        }

        public void FinishCasting()
        {
            OnAttack();
            prepareSpell = null;
            StartCoroutine(StartCooldown());
        }

        private IEnumerator PrepareSpell()
        {
            audioSource.PlayOneShot(CastSound);
            yield return new WaitForSeconds(CastTime);
            
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
            
            audioSource.Stop();
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