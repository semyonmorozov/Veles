using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class StrengthUp : PickUpItemBase
    {
        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().Strength += 1;
        }
    }
}