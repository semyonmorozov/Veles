using UnityEngine;

namespace Main.World
{
    public class Escape :MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }
    }
}