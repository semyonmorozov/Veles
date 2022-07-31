using System;
using System.Collections;
using Main.Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main.UI.MainMenu
{
    public class MainMenuWindow : UIWindow
    {
        private Image blackOutImage;
        public Button ContinueButton;
        public float FadingDelay = 0.01f;
        private IEnumerator fadeInCoroutine;
        private PlayerController playerController;
        public Canvas[] MenuButtonCanvases;

        private void Awake()
        {
            GlobalEventManager.PlayerDeath.AddListener(OnPlayerDeath);
            blackOutImage = GetComponent<Image>();
            fadeInCoroutine = FadeIn();

            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        private void FixedUpdate()
        {
            //Этот метод не вызывается когда время остановлено
        }

        private void Start()
        {
            OffBlackOut();
        }

        private void OffBlackOut()
        {
            var color = blackOutImage.color;
            blackOutImage.color = new Color(color.r, color.g, color.b, 0);
        }

        private void OnPlayerDeath()
        {
            ContinueButton.interactable = false;
            WindowKeyCodes = Array.Empty<KeyCode>();
            StartCoroutine(EnableOnDeath());
        }

        private IEnumerator EnableOnDeath()
        {
            yield return new WaitForSecondsRealtime(3);
            SetEnabled(true);
        }

        protected override void OnEnabling()
        {
            playerController.State = ControllerState.InMenu;
            foreach (var menuButton in MenuButtonCanvases)
            {
                menuButton.enabled = true;
            }
            StartCoroutine(fadeInCoroutine);
            Time.timeScale = 0;
        }

        protected override void OnDisabling()
        {
            playerController.State = ControllerState.ExploreWorld;
            foreach (var menuButton in MenuButtonCanvases)
            {
                menuButton.enabled = false;
            }
            Time.timeScale = 1;
            StopCoroutine(fadeInCoroutine);
            OffBlackOut();
        }

        public void Menu()
        {
            SetEnabled(true);
        }

        public void Continue()
        {
            SetEnabled(false);
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("SampleScene");
        }

        public void Exit()
        {
            // save any game data here
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private IEnumerator FadeIn()
        {
            while (true)
            {
                var color = blackOutImage.color;

                blackOutImage.color = new Color(color.r, color.g, color.b, color.a + 0.01f);
                yield return new WaitForSecondsRealtime(FadingDelay);
            }
        }
    }
}