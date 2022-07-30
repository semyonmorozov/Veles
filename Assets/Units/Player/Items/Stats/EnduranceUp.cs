using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class EnduranceUp : PickUpItemBase
    {
        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().Endurance += 1;
        }
    }
}