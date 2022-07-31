using UnityEngine;

namespace Main.UI.Inventory
{
    public class InventoryWindow : MonoBehaviour
    {
        private Canvas inventoryCanvas;

        private void Awake()
        {
            inventoryCanvas = GetComponent<Canvas>();
        }
    }
}
