using Units.Player;
using UnityEngine;

namespace World.Items
{
    public class AgilityUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Agility += 1;
        }
    }
}