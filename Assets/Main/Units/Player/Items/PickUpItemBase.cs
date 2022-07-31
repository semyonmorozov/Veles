using Unity.VisualScripting;
using UnityEngine;

namespace Main.Units.Player.Items
{
    public abstract class PickUpItemBase : MonoBehaviour
    {
        protected string Name;
        private ItemDescription itemDescription;
        private Canvas itemDescriptionCanvas;

        protected virtual void Awake()
        {
            Name ??= name;
            var itemDescriptionCanvasResource = Resources.Load("Items/ItemDescriptionCanvas");
            var itemTransform = transform;

            var descriptionCanvasObject = Instantiate(itemDescriptionCanvasResource, itemTransform.position,
                Quaternion.identity, itemTransform);
            itemDescriptionCanvas = descriptionCanvasObject.GetComponent<Canvas>();
            itemDescriptionCanvas.enabled = false;

            itemDescription = descriptionCanvasObject.GetComponent<ItemDescription>();
        }

        private void Start()
        {
            itemDescription.SetName(Name);
        }

        public abstract void OnPickUp(GameObject playerGameObject);
    }
}