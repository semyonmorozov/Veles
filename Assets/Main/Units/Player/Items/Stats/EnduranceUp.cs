using UnityEngine;

namespace Main.Units.Player.Items.Stats
{
    public class EnduranceUp : PickUpItemBase
    {
        protected override void Awake()
        {
            Name = "Endurance Up";
            base.Awake();
        }
        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().Endurance += 1;
        }
    }
}