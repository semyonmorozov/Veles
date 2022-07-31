using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.UI.DeathMenu
{
    public class RestartButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
