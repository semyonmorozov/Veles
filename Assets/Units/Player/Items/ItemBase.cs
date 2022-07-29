using Unity.VisualScripting;
using UnityEngine;

namespace Units.Player.Items
{
    public abstract class ItemBase : MonoBehaviour
    {
        public string Name;
        private ItemDescription itemDescription;
        private Canvas itemDescriptionCanvas;

        protected virtual void Awake()
        {
            Name = name;
            var itemDescriptionCanvasResource = Resources.Load("Items/ItemDescriptionCanvas");
            var itemTransform = transform;

            var descriptionCanvasObject = Instantiate(itemDescriptionCanvasResource, itemTransform.position, Quaternion.identity, itemTransform);
            itemDescriptionCanvas = descriptionCanvasObject.GetComponent<Canvas>();
            itemDescriptionCanvas.enabled = false;

            itemDescription = descriptionCanvasObject.GetComponent<ItemDescription>();
            itemDescription.SetName(Name);
        }

        public abstract void OnPickUp(GameObject collisionGameObject);
    }
}