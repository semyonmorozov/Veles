using System;
using UnityEngine;

namespace World
{
    public class SunRotation : MonoBehaviour
    {
        public float Speed = 1;

        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(1, 0) * (Speed * Time.fixedDeltaTime));
        }
    }
}