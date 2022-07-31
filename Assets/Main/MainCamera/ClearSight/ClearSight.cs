using UnityEngine;

namespace Main.MainCamera.ClearSight
{
    public class ClearSight : MonoBehaviour
    {
        public float FadeInSpeed = 0.6f;
        public float FadeOutSpeed = 0.2f;
        public float TargetTransparency = 0.3f;
        public LayerMask LayerMask;
        public int Radius = 10;

        
        private GameObject player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void FixedUpdate()
        {
            var sightColliders = Physics.OverlapCapsule(transform.position, player.transform.position, Radius, LayerMask);
            foreach (var sightCollider in sightColliders)
            {
                var componentsInChildren = sightCollider.gameObject.GetComponentsInChildren<Renderer>();
                    foreach (var lodRenderer in componentsInChildren)
                    {
                        HandleCollision(lodRenderer);
                    }
                HandleCollision(sightCollider.gameObject.GetComponent<Renderer>());
            }
        }

        private void HandleCollision(Component hitComponent)
        {
            if (hitComponent == null)
            {
                return;
            }

            var autoTransparentScript = hitComponent.GetComponent<AutoTransparent>();
            if (autoTransparentScript == null)
            {
                autoTransparentScript = hitComponent.gameObject.AddComponent<AutoTransparent>();
                autoTransparentScript.FadeInSpeed = FadeInSpeed;
                autoTransparentScript.FadeOutSpeed = FadeOutSpeed;
                autoTransparentScript.TargetTransparency = TargetTransparency;
            }

            autoTransparentScript.BeTransparent();
        }
    }
}