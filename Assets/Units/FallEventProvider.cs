using System.Collections;
using UnityEngine;

namespace Units
{
    public class FallEventProvider : MonoBehaviour
    {
        private const int FallPositionY = 0;

        private void Awake()
        {
            StartCoroutine(SendEventIfPlayerFell());
        }

        private IEnumerator SendEventIfPlayerFell()
        {
            while (true)
            {
                if (transform.position.y < FallPositionY) 
                {
                    GlobalEventManager.UnitFellEvent.Invoke(gameObject);
                }

                yield return new WaitForSeconds(2);
            }
        }
    }
}