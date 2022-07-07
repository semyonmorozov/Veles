using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    private GameObject[] spawnPoints;
    private Object enemyPreph;

    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        enemyPreph = Resources.Load("Enemy");
        GlobalEventManager.EnemyDeath.AddListener(SpawnEnemy);
    }

    void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var spawnPointTransform = spawnPoint.transform;
            Instantiate(enemyPreph, spawnPointTransform.position, spawnPointTransform.rotation);
        }
    }

    private void SpawnEnemy()
    {
        var spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length-1)];
        if (spawnPoint == null)
        {
            return;
        }

        var spawnPointTransform = spawnPoint.transform;
        Instantiate(enemyPreph, spawnPointTransform.position, spawnPointTransform.rotation);
    }
}