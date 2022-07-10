using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
