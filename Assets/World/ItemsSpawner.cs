using System.Linq;
using Units.Player.Items;
using Unity.VisualScripting;
using UnityEngine;

namespace World
{
    public class ItemsSpawner: MonoBehaviour
    {
        private Object[] items;
        private GameObject[] spawnPoints;

        private void Awake()
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("ItemSpawnPoint");
            items = Resources.LoadAll("Items").Where(x=>x.GetComponent<ItemBase>() != null).ToArray();
            GlobalEventManager.EnemyDeath.AddListener(SpawnItem);
        }

        private void Start()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                SpawnItem(spawnPoint.transform);
            }
        }

        private void SpawnItem(Transform enemyTransform)
        {
            if (Random.Range(0, 100) >= 30)
            {
                return;
            }
            
            var item = items[Random.Range(0, items.Length)];
            var position = enemyTransform.position;
            position.y += 2;
            Instantiate(item, position, enemyTransform.rotation);
        }
    }
}