using System;
using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public class PlayerFallRespawner : MonoBehaviour
    {
        public int WaitSecondsUntilUpdateRespawnPosition = 2;
        private int fallDamage = 10;

        private Vector3? respawnPosition = null;
        private PlayerHealth playerHealth;

        private void Awake()
        {
            GlobalEventManager.UnitFellEvent.AddListener(Handle);
            StartCoroutine(UpdateRespawnPosition());
            playerHealth = GetComponent<PlayerHealth>();
        }

        private void Handle(GameObject unit)
        {
            if (!unit.CompareTag("Player"))
            {
                return;
            }

            if (respawnPosition == null)
            {
                Debug.Log("Need respawn, but respawnPosition is null.");
                return;
            }

            transform.position = respawnPosition.Value;
            fallDamage = 10;
            playerHealth.TakeDamage(fallDamage);
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

                yield return new WaitForSeconds(WaitSecondsUntilUpdateRespawnPosition);
            }
        }
    }
}