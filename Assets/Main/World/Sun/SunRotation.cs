using UnityEngine;

namespace Main.World
{
    public class SunRotation : MonoBehaviour
    {
        //https://forum.unity.com/threads/sun-rotation-script.436253/
        
        [HideInInspector] public GameObject Sun;
        [HideInInspector] public Light SunLight;
        public AnimationCurve LightIntensityCurve;

        public float ExactDayTime = 12;
        public int Hours = 0;
        public int Minutes = 0;
        public int Seconds = 0;

        [HideInInspector] public float SecondsPerMinute = 60.0f;
        [HideInInspector] public float SecondsPerHour;
        [HideInInspector] public float SecondsPerDay;

        public int CurrentDay = 1;
        public int CurrentMonth = 1;
        public int CurrentYear = 1;

        public float TimeMultiplier = 1;

        private void Start()
        {
            Sun = gameObject;
            SunLight = gameObject.GetComponent<Light>();

            SecondsPerHour = SecondsPerMinute * 60.0f;
            SecondsPerDay = SecondsPerHour * 24.0f;
        }

        private void Update()
        {
            SunUpdate();

            ExactDayTime += Time.deltaTime * TimeMultiplier;

            if (ExactDayTime >= 24)
            {
                CurrentDay += 1;
                if (CurrentDay > 30)
                {
                    CurrentDay = 1;
                    CurrentMonth += 1;
                    if (CurrentMonth > 12)
                    {
                        CurrentYear += 1;
                        CurrentMonth = 1;
                    }
                }

                ExactDayTime = 0;
            }

            Hours = (int)ExactDayTime;
            Minutes = (int)(((ExactDayTime + 1) % (Hours + 1)) * 60);
        }

        private void SunUpdate()
        {
            SunLight.intensity = LightIntensityCurve.Evaluate(Hours+Minutes/60f);
            Sun.transform.localRotation = Quaternion.Euler((ExactDayTime / 24) * 360 - 90, 90, 0);
        }
    }
}