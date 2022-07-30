using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class AgilityUp : PickUpItemBase
    {
        protected override void Awake()
        {
            Name = "Agility Up";
            base.Awake();
        }

        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().Agility += 1;
        }
    }
}