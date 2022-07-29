using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class WillPowerUp : ItemBase
    {
        public override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().WillPower += 1;
        }
    }
}