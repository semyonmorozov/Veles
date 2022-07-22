using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MainCamera.ClearSight
{
    public class AutoTransparent : MonoBehaviour
    {
        public float TargetTransparency { get; set; }

        public float FadeInSpeed { get; set; }

        public float FadeOutSpeed { get; set; }

        private bool shouldBeTransparent;
        private new Renderer renderer;
        private Dictionary<Material, float> originalTransparencies;
        private Dictionary<Material, Shader> originalShaders;
        private List<Material> materialsList;

        private Shader transparentShader;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int Color = Shader.PropertyToID("_Color");

        public void BeTransparent()
        {
            shouldBeTransparent = true;
        }

        private void Awake()
        {
            transparentShader = Shader.Find("Transparent/Diffuse");
            renderer = GetComponent<Renderer>();

            var distinctMaterials = renderer.materials.Distinct().ToArray();

            originalTransparencies = distinctMaterials.ToDictionary(
                k => k,
                v => v.enabledKeywords.Any(x => x.name == "_Color")
                    ? v.color.a
                    : 1f
            );
            originalShaders = distinctMaterials.ToDictionary(k => k, v => v.shader);

            materialsList = renderer.materials
                .Where(x => x.name != "Default-Material (Instance)")
                .ToList();
            if (materialsList.Count == 0)
            {
                Destroy(this);
                return;
            }

            foreach (var material in materialsList)
            {
                material.SetColor(Color, UnityEngine.Color.black);

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
                        currentTransparency -= FadeOutSpeed * Time.fixedDeltaTime;
                    }
                }
                else
                {
                    currentTransparency += FadeInSpeed * Time.fixedDeltaTime;
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