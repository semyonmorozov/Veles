using System.Linq;
using Main.Units.Player.Items.Equip;
using UnityEngine;

namespace Main.World
{
    public class DropSpawner : MonoBehaviour
    {
        public EquipBase[] EquipItems;
        public GameObject[] StatUps;
        public int StatUpDropChance = 10;

        private void Awake()
        {
            GlobalEventManager.EnemyDeath.AddListener(SpawnItem);
        }


        private void SpawnItem(Transform enemyTransform)
        {
            foreach (var equip in EquipItems.OrderBy(x => x.DropChance))
            {
                if (Random.Range(0, 100) <= equip.DropChance)
                {
                    equip.InstantiatePickUp(GetSpawnPosition(enemyTransform.position));
                    break;
                }
            }


            if (Random.Range(0, 100) <= StatUpDropChance)
            {
                Instantiate(
                    StatUps[Random.Range(0, StatUps.Length)],
                    GetSpawnPosition(enemyTransform.position),
                    Quaternion.identity
                );
            }
        }

        private static Vector3 GetSpawnPosition(Vector3 enemyPosition)
        {
            enemyPosition.y += 2;
            return enemyPosition;
        }
    }
}