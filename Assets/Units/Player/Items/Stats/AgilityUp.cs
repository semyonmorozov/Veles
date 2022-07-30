using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class AgilityUp : PickUpItemBase
    {
        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().Agility += 1;
        }
    }
}