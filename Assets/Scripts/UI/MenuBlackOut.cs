using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuBlackOut : MonoBehaviour
    {
        private Image blackOutImage;
        public float fadingDelay = 0.01f;

        private void Awake()
        {
            GlobalEventManager.PlayerDeath.AddListener(() => StartCoroutine(FadeIn()));
            blackOutImage = GetComponent<Image>();
            blackOutImage.enabled = true;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                var color = blackOutImage.color;
                if (color.a <= 0)
                {
                    break;
                }

                blackOutImage.color = new Color(color.r, color.g, color.b, color.a - 0.01f);
                yield return new WaitForSeconds(fadingDelay);
            }
        
            blackOutImage.enabled = false;
        }

        private IEnumerator FadeIn()
        {
            Debug.Log("Start");
            blackOutImage.enabled = true;
            while (true)
            {
                var color = blackOutImage.color;
                if (color.a >= 255)
                {
                    break;
                }

                blackOutImage.color = new Color(color.r, color.g, color.b, color.a + 0.01f);
                yield return new WaitForSeconds(fadingDelay);
            }
        }
    }
}