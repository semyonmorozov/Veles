using System.Collections;
using UnityEngine;

namespace Main.UI.DeathMenu
{
    public class AppearRestartButtonOnDeath: MonoBehaviour
    {
        public GameObject RestartButton;
        
        private void Awake()
        {
            GlobalEventManager.PlayerDeath.AddListener(() => StartCoroutine(Appear()));
        }

        private IEnumerator Appear()
        {
            yield return new WaitForSeconds(2);
            RestartButton.SetActive(true);
        }
    }
}