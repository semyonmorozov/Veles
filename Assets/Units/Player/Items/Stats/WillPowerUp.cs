using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class WillPowerUp : PickUpItemBase
    {
        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().WillPower += 1;
        }
    }
}