using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerFallEventHandler : MonoBehaviour
    {
        public int waitSecondsUntilUpdateRespawnPosition = 2;
        
        private Vector3? respawnPosition = null;

        private void Awake()
        {
            GlobalEventManager.PlayerFellEvent.AddListener(Handle);
            StartCoroutine(UpdateRespawnPosition());
        }

        private void Handle()
        {
            if (respawnPosition == null)
            {
                Debug.Log("Need respawn, but respawnPosition is null.");
                return;
            }
            
            transform.position = respawnPosition.Value;
        }

        private IEnumerator UpdateRespawnPosition()
        {
            while (true)
            {
                //TODO надо переделать, если медленно спускаться к обрыву, то можно зациклиться в падении
                if (respawnPosition == null || Math.Abs(transform.position.y - respawnPosition.Value.y) < 0.5) 
                {
                    respawnPosition = transform.position;
                }
                
                yield return new WaitForSeconds(waitSecondsUntilUpdateRespawnPosition);
            }
        }
    }
}