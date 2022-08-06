using System.Collections;
using UnityEngine;

namespace Main.Units.Player
{
    public class PlayerHealth : Health
    {
        public int DefaultMaxHealth = 10;
        public int MaxHealthEnduranceMultiplier = 20;
        public override int MaxHealth => DefaultMaxHealth + playerMainStats.Endurance * MaxHealthEnduranceMultiplier;

        private PlayerMainStats playerMainStats;
        private AudioSource audioSource;
        private PlayerEquip playerEquip;

        protected override void Awake()
        {
            playerMainStats = GetComponent<PlayerMainStats>();
            playerEquip = GetComponent<PlayerEquip>();
            base.Awake();
            StartCoroutine(Regeneration());
            audioSource = GetComponent<AudioSource>();
        }

        private IEnumerator Regeneration()
        {
            while (true)
            {
                RestoreHealth((playerMainStats.Endurance + playerMainStats.WillPower) / 2);
                yield return new WaitForSeconds(0.5f + (2f - playerMainStats.WillPower / 20f));
            }
        }

        protected override void OnDeath()
        {
            GlobalEventManager.PlayerDeath.Invoke();
        }

        public override void TakeDamage(int damage)
        {
            damage -= playerEquip.Defence();
            if(damage<=0)
                return;
            
            base.TakeDamage(damage);
            
            if (!IsDead())
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.Play();
            }
        }
    }
}