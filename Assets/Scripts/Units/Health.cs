using System;
using UnityEngine;

namespace Units
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth;

        public bool IsDead() => currentHealth == 0;
        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead())
            {
                return;
            }
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