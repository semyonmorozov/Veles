using UnityEngine;

namespace Units.Player.Items
{
    public class IntelligenceUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Intelligence += 1;
        }
    }
}