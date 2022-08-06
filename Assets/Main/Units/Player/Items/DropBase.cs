using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Main.Units.Player.Items
{
    public abstract class DropBase : ScriptableObject
    {
        public int DropChance = 0;
        public GameObject DropModel;
        public string NameInGame;

        private void OnPickUp(GameObject pickUpObject, GameObject playerGameObject)
        {
            InnerOnPickUp(playerGameObject);
            Destroy(pickUpObject);
        }

        protected abstract void InnerOnPickUp(GameObject playerGameObject);

        public void OnDrop(Vector3 dropPosition)
        {
            var pickUpObject = new GameObject(NameInGame)
            {
                transform =
                {
                    position = dropPosition
                }
            };

            var dropGameModel = InstantiateDropGameModel(pickUpObject);
            Instantiate(Resources.Load("Items/FireFlies"), dropGameModel.transform.position, Quaternion.identity, dropGameModel.transform);
            InstantiateDropDescription(pickUpObject, dropGameModel);
            
            InnerOnDrop(pickUpObject);
        }

        protected virtual void InnerOnDrop(GameObject pickUpObject)
        {
        }

        private GameObject InstantiateDropGameModel(GameObject pickUpObject)
        {
            var pickUpTransform = pickUpObject.transform;

            var instantiateDropModel = Instantiate(DropModel, pickUpTransform.position, Quaternion.identity, pickUpTransform);
            instantiateDropModel.AddComponent<ReDrop>();
            
            return  instantiateDropModel;
        }

        private void InstantiateDropDescription(GameObject pickUpObject, GameObject dropGameModel)
        {
            var pickUpTransform = pickUpObject.transform;
            var itemDescriptionResource = Resources.Load("Items/ItemDescriptionCanvas");
            var descriptionObject = Instantiate(itemDescriptionResource, pickUpTransform.position, Quaternion.identity, pickUpTransform);


            var dropDescription = descriptionObject.GetComponent<DropDescription>();
            dropDescription.SetDropName(NameInGame);
            dropDescription.SetOnPickUpAction(playerGameObject => OnPickUp(pickUpObject, playerGameObject));
            dropDescription.SetDropModel(dropGameModel);
        }
    }
}