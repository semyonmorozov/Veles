using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Units.Player.ClearSight
{
    public class AutoTransparent : MonoBehaviour
    {
        public float TargetTransparency { get; set; }

        public float FadeInTimeout { get; set; }

        public float FadeOutTimeout { get; set; }

        public Material TransparentMaterial { get; set; }

        private bool shouldBeTransparent;
        private new Renderer renderer;
        private Dictionary<Material, float> originalTransparencies;
        private Dictionary<Material, Shader> originalShaders;
        private Material[] materialsList;

        private Shader transparentShader;

        public void BeTransparent()
        {
            shouldBeTransparent = true;
        }

        private void Awake()
        {
            transparentShader = Shader.Find("Transparent/Diffuse");
            renderer = GetComponent<Renderer>();
            var distinctMaterials = renderer.materials.Distinct();
            originalTransparencies = distinctMaterials.ToDictionary(k => k, v => v.color.a);
            originalShaders = distinctMaterials.ToDictionary(k => k, v => v.shader);
            
            materialsList = renderer.materials;
            foreach (var material in materialsList)
            {
                material.shader = transparentShader;
            }
        }

        private void FixedUpdate()
        {
            var transparencyReturnedToOriginal = true;

            foreach (var material in materialsList)
            {
                var color = material.color;
                var currentTransparency = color.a;
                var originalTransparency = originalTransparencies[material];
                
                if (shouldBeTransparent)
                {
                    if (currentTransparency > TargetTransparency)
                    {
                        currentTransparency -= ((originalTransparency - TargetTransparency) * Time.fixedDeltaTime) /
                                               FadeOutTimeout;
                    }
                }
                else
                {
                    currentTransparency += ((originalTransparency - TargetTransparency) * Time.fixedDeltaTime) / FadeInTimeout;
                    if (Math.Abs(originalTransparency - currentTransparency) > TargetTransparency)
                    {
                        transparencyReturnedToOriginal = false;
                    }
                }
                
                color.a = currentTransparency;
                
                material.color = color;
            }

            
            if (!shouldBeTransparent && transparencyReturnedToOriginal)
            {
                foreach (var material in materialsList)
                {
                    material.shader = originalShaders[material];
                }
                Destroy(this);
            }

            shouldBeTransparent = false;
        }
    }
}