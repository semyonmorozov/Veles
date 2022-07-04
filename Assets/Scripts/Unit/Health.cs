using UnityEngine;

namespace Unit
{
    public class Health : MonoBehaviour
    {
        public int health = 100;

        public void TakeDamage(int damage)
        {
            if (damage >= health)
            {
                health = 0;
                OnDeath();
            }
            else
            {
                health -= damage;
            }
        }

        protected virtual void OnDeath()
        {
        }
    }
}