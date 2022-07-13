using Units.Player;
using UnityEngine;

namespace World.Items
{
    public class StrengthUp : ItemBase
    {
        protected override void OnPickUp(GameObject collisionGameObject)
        {
            collisionGameObject.GetComponent<PlayerStats>().Strength += 1;
        }
    }
}