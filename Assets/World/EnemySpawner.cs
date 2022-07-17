using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace World
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject[] Enemies;
        public Camera mainCamera;

        private NavMeshSurface navMeshSurface;
        private GameObject[] spawnPoints;

        private void Awake()
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
            GlobalEventManager.EnemyDeath.AddListener(SpawnEnemy);
        }

        private void Start()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if (Enemies.Length == 0)
                    return;

                var spawnPointTransform = spawnPoint.transform;
                Instantiate(GetEnemy(), spawnPointTransform.position, spawnPointTransform.rotation);
            }
        }

        private void SpawnEnemy(Transform enemyTransform)
        {
            if (spawnPoints.Length == 0)
                return;
            var spawnPointsBehindScreen = spawnPoints
                .Select(spawnPoint => (onScreenPosition: GetPositionOnScreen(spawnPoint), spawnPoint))
                .Where(y => y.onScreenPosition.x < 0 || y.onScreenPosition.y < 0)
                .Select(p => p.spawnPoint)
                .ToArray();
            var spawnPoint = spawnPointsBehindScreen[Random.Range(0, spawnPointsBehindScreen.Length - 1)];

            var spawnPointTransform = spawnPoint.transform;
            Instantiate(GetEnemy(), spawnPointTransform.position, spawnPointTransform.rotation);
        }

        private Vector3 GetPositionOnScreen(GameObject x)
        {
            return mainCamera.WorldToViewportPoint(x.transform.position);
        }

        private GameObject GetEnemy()
        {
            return Enemies[Random.Range(0, Enemies.Length)];
        }
    }
}