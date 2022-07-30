using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class IntelligenceUp : PickUpItemBase
    {
        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().Intelligence += 1;
        }
    }
}