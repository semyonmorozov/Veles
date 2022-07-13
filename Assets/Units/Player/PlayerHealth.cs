using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public class PlayerHealth : Health
    {
        public int DefaultMaxHealth = 10;
        public int MaxHealthEnduranceMultiplier = 20;
        public override int MaxHealth => DefaultMaxHealth + playerStats.Endurance * MaxHealthEnduranceMultiplier;

        private PlayerStats playerStats;

        protected override void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
            base.Awake();
            StartCoroutine(Regeneration());
        }

        private IEnumerator Regeneration()
        {
            while (true)
            {
                RestoreHealth((playerStats.Endurance + playerStats.WillPower) / 2);
                yield return new WaitForSeconds(0.5f + (2f - playerStats.WillPower / 20f));
            }
        }

        protected override void OnDeath()
        {
            GlobalEventManager.PlayerDeath.Invoke();
        }
    }
}