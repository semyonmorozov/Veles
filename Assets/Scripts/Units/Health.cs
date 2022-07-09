using System;
using UnityEngine;

namespace Units
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage >= currentHealth)
            {
                currentHealth = 0;
                OnDeath();
            }
            else
            {
                currentHealth -= damage;
            }
        }

        protected virtual void OnDeath()
        {
        }
    }
}