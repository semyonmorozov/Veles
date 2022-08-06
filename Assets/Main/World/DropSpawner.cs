using System.Linq;
using Main.Units.Player.Items;
using UnityEngine;

namespace Main.World
{
    public class DropSpawner : MonoBehaviour
    {
        public DropBase[] EquipItems;
        public DropBase[] StatUps;

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
                    equip.OnDrop(GetSpawnPosition(enemyTransform.position));
                    break;
                }
            }

            foreach (var statUp in StatUps.OrderBy(x => x.DropChance))
            {
                if (Random.Range(0, 100) <= statUp.DropChance)
                {
                    statUp.OnDrop(GetSpawnPosition(enemyTransform.position));
                    break;
                }
            }
        }

        private static Vector3 GetSpawnPosition(Vector3 enemyPosition)
        {
            enemyPosition.y += 2;
            return enemyPosition;
        }
    }
}