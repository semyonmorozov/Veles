using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class EnduranceUp : ItemBase
    {
        public override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Endurance += 1;
        }
    }
}