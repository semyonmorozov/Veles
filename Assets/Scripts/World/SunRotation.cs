using System;
using UnityEngine;

namespace World
{
    public class SunRotation : MonoBehaviour
    {
        public float speed = 1;

        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(1, 0) * (speed * Time.fixedDeltaTime));
        }
    }
}