using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Units.Player.Items.Equip
{
    public abstract class EquipBase : ScriptableObject
    {
        public int DropChance = 0;
        public string Name;
        public string ModelPath;
        public EquipType EquipType;
        public GameObject PickUpEquip;
        public string[] DefaultModelNames;

        public void SetEquipModel(GameObject player)
        {
            ChangeModelState(player, ModelPath, true);
        }

        private void ChangeModelState(GameObject player, string modelPath, bool enabled)
        {
            var transform = player.transform.Find(modelPath);
            if (transform != null)
                transform.GetComponent<Renderer>().enabled = enabled;
            else
            {
                Debug.Log($"Missing model {Name}");
            }
        }

        public void RemoveEquipModel(GameObject player)
        {
            ChangeModelState(player, ModelPath, false);
        }

        public void SetDefaultModel(GameObject player)
        {
            foreach (var defaultModelName in DefaultModelNames)
            {
                ChangeModelState(player, defaultModelName, true);
            }
        }

        public void RemoveDefaultModel(GameObject player)
        {
            foreach (var defaultModelName in DefaultModelNames)
            {
                ChangeModelState(player, defaultModelName, false);
            }
        }

        public void InstantiatePickUp(Vector3 spawnPosition)
        {
            PickUpEquip.GetComponent<PickUpEquip>().Equip = this;
            var gameObject = Instantiate(PickUpEquip, spawnPosition, PickUpEquip.transform.rotation);
            var vector3 = new Vector3(Random.Range(-100, 100),200, Random.Range(-100, 100));
            gameObject.GetComponent<Rigidbody>().AddForce(vector3);
        }
    }
}