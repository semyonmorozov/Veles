using UnityEngine;

namespace World.Items
{
    public abstract class ItemBase : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var collisionGameObject = other.gameObject;
            if (collisionGameObject.CompareTag("Player"))
            {
                OnPickUp(collisionGameObject);
                Destroy(gameObject);
            }
        }

        protected abstract void OnPickUp(GameObject collisionGameObject);
    }
}