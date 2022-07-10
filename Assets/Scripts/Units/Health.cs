using System;
using UnityEngine;

namespace Units
{
    public class Health : MonoBehaviour
    {
        public int MaxHealth = 100;
        public int CurrentHealth;

        public bool IsDead() => CurrentHealth == 0;
        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead())
            {
                return;
            }
            if (damage >= CurrentHealth)
            {
                CurrentHealth = 0;
                OnDeath();
            }
            else
            {
                CurrentHealth -= damage;
            }
        }

        protected virtual void OnDeath()
        {
        }
    }
}