using System.Collections;
using UnityEngine;

namespace World
{
    public class AppearRestartButtonOnDeath: MonoBehaviour
    {
        public GameObject restartButton;
        
        private void Awake()
        {
            GlobalEventManager.PlayerDeath.AddListener(() => StartCoroutine(Appear()));
        }

        private IEnumerator Appear()
        {
            yield return new WaitForSeconds(2);
            restartButton.SetActive(true);
        }
    }
}