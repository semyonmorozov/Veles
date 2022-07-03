using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class Respawn : MonoBehaviour
    {
        public int respawnTriggerPositionY = -10;
        public int waitSecondsUntilUpdateRespawnPosition = 2;
        private Vector3? respawnPosition = null;

        private void Awake()
        {
            StartCoroutine(UpdateRespawnPosition());
        }

        private void FixedUpdate()
        {
            RespawnIfUnderTerrain();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void RespawnIfUnderTerrain()
        {
            if (transform.position.y < respawnTriggerPositionY)
            {
                if (respawnPosition == null)
                {
                    Debug.Log("Need respawn, but respawnPosition is null.");
                    return;
                }
                
                transform.position = respawnPosition.Value;
                GlobalEventManager.PlayerFell.Invoke();
            }
        }

        private IEnumerator UpdateRespawnPosition()
        {
            while (true)
            {
                if (respawnPosition == null || Math.Abs(transform.position.y - respawnPosition.Value.y) == 0)
                    respawnPosition = transform.position;
                yield return new WaitForSeconds(waitSecondsUntilUpdateRespawnPosition);
            }
        }
    }
}
