using UnityEngine;

namespace Units.Player.Items
{
    public class WillPowerUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().WillPower += 1;
        }
    }
}