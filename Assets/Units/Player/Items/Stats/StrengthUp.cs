using UnityEngine;

namespace Units.Player.Items.Stats
{
    public class StrengthUp : ItemBase
    {
        public override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Strength += 1;
        }
    }
}