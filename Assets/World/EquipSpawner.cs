using System.Linq;
using Units.Player.Items;
using Units.Player.Items.Equip;
using Unity.VisualScripting;
using UnityEngine;

namespace World
{
    public class EquipSpawner : MonoBehaviour
    {
        private EquipBase[] items;
        private GameObject[] spawnPoints;

        private void Awake()
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("ItemSpawnPoint");
            items = Resources.LoadAll("Items/Equip").Select(x => (EquipBase)x).ToArray();
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
            if (Random.Range(0, 100) <= 100)
            {
                var item = items[Random.Range(0, items.Length)];
                var position = enemyTransform.position;
                position.y += 2;
                item.InstantiatePickUp(position);
            }
        }
    }
}