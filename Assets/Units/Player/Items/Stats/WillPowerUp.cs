using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class WillPowerUp : PickUpItemBase
    {        
        protected override void Awake()
        {
            Name = "Willpower Up";
            base.Awake();
        }
        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().WillPower += 1;
        }
    }
}