using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Player.Items
{
    public class ItemDescription : MonoBehaviour
    {
        private GameObject mainCamera;
        
        private void Awake()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            GetComponent<Canvas>().worldCamera = mainCamera.GetComponent<Camera>();
            
            RotateToCamera();
            
            var instantiateTransform = transform;
            var instantiatePosition = instantiateTransform.position;
            instantiatePosition.y += 1f;
            instantiateTransform.position = instantiatePosition;
            
            GetComponent<Rigidbody>().velocity = instantiateTransform.forward * 2;
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