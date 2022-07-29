using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class AgilityUp : ItemBase
    {
        public override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Agility += 1;
        }
    }
}