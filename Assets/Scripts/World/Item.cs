using Units.Player;
using UnityEngine;

namespace World
{
    public class Item : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var collisionGameObject = other.gameObject;
            if (collisionGameObject.CompareTag("Player"))
            {
                collisionGameObject.GetComponent<PlayerStats>().Agility = 1;
                Destroy(gameObject);
            }
        }
    }
}