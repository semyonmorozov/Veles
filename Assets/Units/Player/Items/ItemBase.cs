using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Player.Items
{
    public abstract class ItemBase : MonoBehaviour
    {
        public string Name;
        private bool highlighted = false;
        private ItemDescription itemDescription;

        private void Awake()
        {
            Name = name;
            var itemDescriptionCanvasResource = Resources.Load("Items/ItemDescriptionCanvas");
            var itemTransform = transform;
            var itemDescriptionCanvas = Instantiate(itemDescriptionCanvasResource, itemTransform.position, Quaternion.identity, itemTransform);
            itemDescription = itemDescriptionCanvas.GetComponentInChildren<ItemDescription>();
            itemDescription.SetName(Name);
        }

        private void OnTriggerEnter(Collider other)
        {
            var collisionGameObject = other.gameObject;
            if (collisionGameObject.CompareTag("Player"))
            {
                OnPickUp(collisionGameObject);
                Destroy(gameObject);
            }
        }

        protected abstract void OnPickUp(GameObject collisionGameObject);
    }
}