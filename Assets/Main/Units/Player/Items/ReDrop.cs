using System;
using UnityEngine;

namespace Main.Units.Player.Items
{
    public class ReDrop : MonoBehaviour
    {
        public Vector3 SpawnPosition;

        void Start()
        {
            SpawnPosition = gameObject.transform.position;
        }

        void Update()
        {
            var position = gameObject.transform.position;
            if (position.y < 0)
            {
                SpawnPosition.y += 1;
                gameObject.transform.position = SpawnPosition;
            }
        }
    }
}
