using Units.Player;
using UnityEngine;

namespace World.Items
{
    public class EnduranceUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Endurance += 1;
        }
    }
}