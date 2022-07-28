using UnityEngine;

namespace Units.Player.Items
{
    public class EnduranceUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Endurance += 1;
        }
    }
}