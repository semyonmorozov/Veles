using Units.Player;
using UnityEngine;

namespace World.Items
{
    public class IntelligenceUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Intelligence += 1;
        }
    }
}