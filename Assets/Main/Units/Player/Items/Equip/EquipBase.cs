using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Main.Units.Player.Items.Equip
{
    public abstract class EquipBase : DropBase
    {
        public int Defence;

        public string ModelPath;
        public string[] DefaultModelNames;
        protected abstract EquipBase GetEquippedItem(PlayerEquip playerEquip);
        protected abstract void SetEquip(PlayerEquip playerEquip);
        
        protected override void InnerOnDrop(GameObject pickUpObject)
        {
            var vector3 = new Vector3(Random.Range(-100, 100),200, Random.Range(-100, 100));
            pickUpObject.GetComponentInChildren<Rigidbody>().AddForce(vector3);
        }

        protected override void InnerOnPickUp(GameObject playerGameObject)
        {
            var playerEquip = playerGameObject.GetComponent<PlayerEquip>();
            var equippedItem = GetEquippedItem(playerEquip);
            if (equippedItem != null)
            {
                equippedItem.RemoveEquipModel(playerGameObject);
                equippedItem.OnDrop(playerGameObject.transform.position);
            }
            else
            {
                RemoveDefaultModel(playerGameObject);
            }

            SetEquip(playerEquip);
            SetEquipModel(playerGameObject);
        }

        private void SetEquipModel(GameObject player)
        {
            ChangeModelState(player, ModelPath, true);
        }

        private void RemoveEquipModel(GameObject player)
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

        private void RemoveDefaultModel(GameObject player)
        {
            foreach (var defaultModelName in DefaultModelNames)
            {
                ChangeModelState(player, defaultModelName, false);
            }
        }

        private void ChangeModelState(GameObject player, string modelPath, bool enabled)
        {
            var transform = player.transform.Find(modelPath);
            if (transform != null)
                transform.GetComponent<Renderer>().enabled = enabled;
            else
            {
                Debug.Log($"Missing model {NameInGame}");
            }
        }
    }
}