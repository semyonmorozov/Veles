using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public class RestartButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
