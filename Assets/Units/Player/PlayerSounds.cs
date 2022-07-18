using Units.Enemies;
using UnityEngine.Audio;

namespace Units.Player
{
    public class PlayerSounds : UnitMovingSound
    {
        public AudioMixerGroup SpellMixer;
        protected override void Awake()
        {
            base.Awake();
            MaxSoundDelay = 0;
        }
    }
}