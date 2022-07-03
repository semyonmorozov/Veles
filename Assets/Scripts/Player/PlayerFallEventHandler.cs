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
            
            Debug.Log($"Respawn {respawnPosition.Value.x}, {respawnPosition.Value.y}, {respawnPosition.Value.z}");
            transform.position = respawnPosition.Value;
        }

        private IEnumerator UpdateRespawnPosition()
        {
            while (true)
            {
                Debug.Log($"Try set new respawnPosition {transform.position.x}, {transform.position.y}, {transform.position.z}");
                if (respawnPosition == null || Math.Abs(transform.position.y - respawnPosition.Value.y) < 0.5) // надо переделать на определения расстояния от земли
                {
                    respawnPosition = transform.position;
                }
                
                Debug.Log($"New respawnPosition {respawnPosition.Value.x}, {respawnPosition.Value.y}, {respawnPosition.Value.z}");
                yield return new WaitForSeconds(waitSecondsUntilUpdateRespawnPosition);
            }
        }
    }
}