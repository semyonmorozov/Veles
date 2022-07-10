using System;
using Units.Player;
using UnityEngine;

namespace Units
{
    public class Health : MonoBehaviour
    {
        public virtual int MaxHealth => 100;
        public int CurrentHealth;

        public bool IsDead() => CurrentHealth == 0;
        protected virtual void Awake()
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

        public void RestoreHealth(int restorationAmount)
        {
            if (IsDead())
            {
                return;
            }
            if (CurrentHealth + restorationAmount >= MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            else
            {
                CurrentHealth += restorationAmount;
            }
        }

        protected virtual void OnDeath()
        {
        }
    }
}