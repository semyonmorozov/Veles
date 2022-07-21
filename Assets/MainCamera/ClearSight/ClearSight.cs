using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Player.ClearSight
{
    public class ClearSight : MonoBehaviour
    {
        public Material TransparentMaterial = null;
        public float FadeInTimeout = 0.6f;
        public float FadeOutTimeout = 0.2f;
        public float TargetTransparency = 0.3f;
        public LayerMask LayerMask;
        public int Radius = 10;

        public TerrainData terrain;
        
        private GameObject player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void FixedUpdate()
        {
            var colliders = Physics.OverlapCapsule(transform.position, player.transform.position, Radius, LayerMask);
            foreach (var collider in colliders)
            {
                HandleCollision(collider);
            }
        }

        private void HandleCollision(Component other)
        {
            Debug.Log(other.name);
            var hitRenderer = other.gameObject.GetComponent<Renderer>();
            if (hitRenderer == null)
            {
                return;
            }

            // TODO: maybe implement here a check for GOs that should not be affected like the player
            var autoTransparentScript = hitRenderer.GetComponent<AutoTransparent>();
            if (autoTransparentScript == null)
            {
                autoTransparentScript = hitRenderer.gameObject.AddComponent<AutoTransparent>();
                autoTransparentScript.TransparentMaterial = TransparentMaterial;
                autoTransparentScript.FadeInTimeout = FadeInTimeout;
                autoTransparentScript.FadeOutTimeout = FadeOutTimeout;
                autoTransparentScript.TargetTransparency = TargetTransparency;
            }

            autoTransparentScript.BeTransparent();
        }
    }
}