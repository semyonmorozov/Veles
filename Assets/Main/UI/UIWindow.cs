using System.Linq;
using UnityEngine;

namespace Main.UI
{
    public class UIWindow : MonoBehaviour
    {
        public Canvas WindowCanvas;
        public KeyCode[] WindowKeyCodes;

        protected void Update()
        {
            if (WindowKeyCodes.Any(Input.GetKeyDown))
                SetEnabled();
        }

        public void SetEnabled(bool? isEnabled = null)
        {
            var newEnableState = isEnabled ?? !WindowCanvas.enabled;

            if (WindowCanvas.enabled != newEnableState)
            {
                if (newEnableState)
                {
                    OnEnabling();
                }
                else
                {
                    OnDisabling();
                }
            }

            WindowCanvas.enabled = newEnableState;
        }

        protected virtual void OnEnabling()
        {
        }

        protected virtual void OnDisabling()
        {
        }
    }
}