using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        public int respawnTriggerPositionY = 0;

        private void Awake()
        {
            StartCoroutine(SendEventIfPlayerFell());
        }
        
        private IEnumerator SendEventIfPlayerFell()
        {
            while (true)
            {
                if (transform.position.y < respawnTriggerPositionY)
                {
                    GlobalEventManager.PlayerFellEvent.Invoke();
                }

                yield return new WaitForSeconds(2);
            }
        }
    }
}