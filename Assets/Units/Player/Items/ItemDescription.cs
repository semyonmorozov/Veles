using System;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Player.Items
{
    public class ItemDescription : MonoBehaviour
    {
        private Camera mainCamera;
        private Canvas canvas;
        private ItemBase item;
        private const float PointerHighlightDistance = 1f;

        private void Awake()
        {
            mainCamera = Camera.main;
            canvas = GetComponent<Canvas>();
            canvas.worldCamera = mainCamera;
            
            RotateToCamera();
            
            var instantiateTransform = transform;
            var instantiatePosition = instantiateTransform.position;
            instantiatePosition.y += 1f;
            instantiateTransform.position = instantiatePosition;
            
            GetComponent<Rigidbody>().velocity = instantiateTransform.forward * 2;

            item = gameObject.GetComponentInParent<ItemBase>();
            canvas.GetComponent<Button>().onClick.AddListener(InnerOnPickUp);
        }

        private void InnerOnPickUp()
        {
            item.OnPickUp(GameObject.FindWithTag("Player"));
            Destroy(item.gameObject);
        }

        private void FixedUpdate()
        {
            var highlighted = Input.GetKey(KeyCode.LeftAlt) || PointerNearItem();
            canvas.enabled = highlighted;
        }

        private bool PointerNearItem()
        {
            var canvasPosition = canvas.transform.position;
            var mousePosition = mainCamera.GetMousePosition(canvasPosition.y);
            var delta = canvasPosition - mousePosition;
            return Math.Abs(delta.x) < PointerHighlightDistance && Math.Abs(delta.z) < PointerHighlightDistance;
        }

        public void SetName(string itemName)
        {
            var nameTextMesh = GetComponentInChildren<TextMeshProUGUI>();
            nameTextMesh.autoSizeTextContainer = true;
            nameTextMesh.text = itemName;
            nameTextMesh.Rebuild(CanvasUpdate.Prelayout);
            
            var horizontalMarginsWidth = nameTextMesh.margin.x + nameTextMesh.margin.z;
            var textWidth = nameTextMesh.rectTransform.rect.width;
            
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textWidth + horizontalMarginsWidth);
        }

        public void SetPositionByItemPosition(Vector3 itemPosition)
        {
            transform.position = itemPosition;
        }

        protected void Update()
        {
            RotateToCamera();
        }

        private void RotateToCamera()
        {
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}