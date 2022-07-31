using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Main.Units
{
    public class UnitMovingSound : MonoBehaviour
    {
        public AudioMixerGroup MovingMixer;

        public AudioClip[] MovingSounds;
        
        protected AudioSource MovingAudioSource;
        public int MaxSoundDelay = 2;
        private IEnumerator playMovingSounds;
        private bool movingSoundPlaying = false;

        protected virtual void Awake()
        {
            MovingAudioSource = gameObject.AddComponent<AudioSource>();
            MovingAudioSource.outputAudioMixerGroup = MovingMixer;
            playMovingSounds = InnerPlayMovingSounds();
        }

        public void PlayMovingSounds()
        {
            if (movingSoundPlaying) 
                return;
            
            movingSoundPlaying = true;
            StartCoroutine(playMovingSounds);
        }
        
        public void StopMovingSounds()
        {
            if (!movingSoundPlaying) 
                return;
            
            movingSoundPlaying = false;
            StopCoroutine(playMovingSounds);
        }

        private IEnumerator InnerPlayMovingSounds()
        {
            while (true)
            {
                if (MovingSounds.Length > 0)
                {
                    MovingAudioSource.pitch = Random.Range(0.9f, 1.1f);
                    var randomMovingSound = MovingSounds[Random.Range(0,MovingSounds.Length)];
                    MovingAudioSource.PlayOneShot(randomMovingSound);
                    yield return new WaitForSeconds(randomMovingSound.length);
                }
                yield return new WaitForSeconds(Random.Range(0,MaxSoundDelay));
            }
        }
    }
}