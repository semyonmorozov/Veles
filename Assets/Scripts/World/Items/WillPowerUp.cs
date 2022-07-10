using Units.Player;
using UnityEngine;

namespace World.Items
{
    public class WillPowerUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().WillPower += 1;
        }
    }
}