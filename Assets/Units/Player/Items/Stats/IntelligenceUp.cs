using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class IntelligenceUp : ItemBase
    {
        public override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Intelligence += 1;
        }
    }
}