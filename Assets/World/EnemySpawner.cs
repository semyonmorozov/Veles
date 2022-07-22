using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace World
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject[] Enemies;
        public Camera MainCamera;
        public float EnemySpawnChance = 0f;
        public float EnemySpawnChanceRisingCoefficient = 10000f;
        public int SpawnTimeout = 1;
        public float MaxChance = 0.25f;

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

            StartCoroutine(SpawnByTime());
        }

        private IEnumerator SpawnByTime()
        {
            while (true)
            {
                
                if (EnemySpawnChance <= MaxChance)
                {
                    EnemySpawnChance = Time.realtimeSinceStartup / EnemySpawnChanceRisingCoefficient;
                    if (EnemySpawnChance>MaxChance)
                    {
                        EnemySpawnChance = MaxChance;
                    }
                }

                if (Random.Range(0, 100) <= (EnemySpawnChance * 100))
                {
                    SpawnEnemy();
                }

                yield return new WaitForSeconds(SpawnTimeout);
            }
        }

        private void SpawnEnemy(Transform enemyTransform = null)
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
            return MainCamera.WorldToViewportPoint(x.transform.position);
        }

        private GameObject GetEnemy()
        {
            return Enemies[Random.Range(0, Enemies.Length)];
        }
    }
}