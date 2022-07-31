using UnityEngine;
using UnityEngine.Audio;

namespace Main.Units
{
    public class UnitAttackSound : MonoBehaviour
    {
        public AudioMixerGroup AttackMixer;
        public AudioClip[] AttackSounds;
        public AudioSource AttackAudioSource;

        protected void Awake()
        {
            AttackAudioSource = gameObject.AddComponent<AudioSource>();
            AttackAudioSource.outputAudioMixerGroup = AttackMixer;
        }

        public void PlayAttackSound()
        {
            if (AttackSounds.Length > 0)
            {
                AttackAudioSource.pitch = Random.Range(0.9f, 1.1f);
                var randomMovingSound = AttackSounds[Random.Range(0,AttackSounds.Length)];
                AttackAudioSource.PlayOneShot(randomMovingSound);
            }
        }
    }
}