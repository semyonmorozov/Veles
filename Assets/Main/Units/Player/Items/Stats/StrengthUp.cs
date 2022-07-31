using UnityEngine;

namespace Main.Units.Player.Items.Stats
{
    public class StrengthUp : PickUpItemBase
    {        
        protected override void Awake()
        {
            Name = "Strength Up";
            base.Awake();
        }
        public override void OnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerStats>().Strength += 1;
        }
    }
}