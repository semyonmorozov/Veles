using System;
using Main.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Units.Player.Items
{
    public class DropDescription : MonoBehaviour
    {
        private Camera mainCamera;
        private Canvas canvas;
        private const float PointerHighlightDistance = 1f;
        private static float pickUpDistance;

        private Action<GameObject> onPickUp;
        private string dropName;
        private GameObject dropModel;
        private GameObject playerGameObject;
        private Button button;

        private void Awake()
        {
            mainCamera = Camera.main;
            canvas = GetComponent<Canvas>();
            button = GetComponent<Button>();
            canvas.worldCamera = mainCamera;
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            pickUpDistance = playerGameObject.GetComponent<PlayerUtilityStats>()?.PickUpDistance ?? 5f;
        }

        public void SetDropName(string newDropName)
        {
            dropName = newDropName;

            var nameTextMesh = GetComponentInChildren<TextMeshProUGUI>();
            nameTextMesh.autoSizeTextContainer = true;
            nameTextMesh.text = dropName;
            nameTextMesh.Rebuild(CanvasUpdate.Prelayout);

            var horizontalMarginsWidth = nameTextMesh.margin.x + nameTextMesh.margin.z;
            var textWidth = nameTextMesh.rectTransform.rect.width;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textWidth + horizontalMarginsWidth);
        }

        public void SetOnPickUpAction(Action<GameObject> newOnPickUp)
        {
            onPickUp = newOnPickUp;
            canvas.GetComponent<Button>().onClick.AddListener(() => onPickUp(playerGameObject));
        }

        public void SetDropModel(GameObject newDropModel)
        {
            dropModel = newDropModel;
        }

        protected void Update()
        {
            var playerPosition = playerGameObject.transform.position;
            var dropModelPosition = dropModel.transform.position;

            var playerPositionV2 = new Vector2(playerPosition.x, playerPosition.z);
            var dropModelPositionV2 = new Vector2(dropModelPosition.x, dropModelPosition.z);
            button.interactable = !(Vector2.Distance(playerPositionV2,dropModelPositionV2) > pickUpDistance);

            if(dropModel == null) return;

            var descriptionTransform = transform;

            descriptionTransform.rotation = mainCamera.transform.rotation;
            descriptionTransform.position = dropModelPosition - descriptionTransform.forward * 2;

            canvas.enabled = Input.GetKey(KeyCode.LeftAlt)
                             || PointerNear(dropModelPosition)
                             || PointerNear(canvas.transform.position);
        }

        private bool PointerNear(Vector3 position)
        {
            var mousePosition = mainCamera.GetMousePosition(position.y);
            var delta = position - mousePosition;
            return Math.Abs(delta.x) < PointerHighlightDistance && Math.Abs(delta.z) < PointerHighlightDistance;
        }
    }
}