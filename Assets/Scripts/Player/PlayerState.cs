using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        public int health = 100;
        
        private const int FallPositionY = 0;

        private void Awake()
        {
            StartCoroutine(SendEventIfPlayerFell());
        }

        public void TakeDamage(int damage)
        {
            if (damage >= health)
            {
                health = 0;
                GlobalEventManager.PlayerDeath.Invoke();
            }
            else
            {
                health -= damage;
            }
        }

        private IEnumerator SendEventIfPlayerFell()
        {
            while (true)
            {
                if (transform.position.y < FallPositionY) 
                {
                    GlobalEventManager.PlayerFellEvent.Invoke();
                }

                yield return new WaitForSeconds(2);
            }
        }
    }
}