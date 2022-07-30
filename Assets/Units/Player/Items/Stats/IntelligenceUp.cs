using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class IntelligenceUp : PickUpItemBase
    {
        protected override void Awake()
        {
            Name = "Intelligence Up";
            base.Awake();
        }

        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().Intelligence += 1;
        }
    }
}