using UnityEngine;

namespace Units.Player.Items
{
    public class AgilityUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Agility += 1;
        }
    }
}