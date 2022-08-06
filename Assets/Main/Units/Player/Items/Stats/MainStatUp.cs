using Main.Units.Player.Items.Equip;
using UnityEngine;

namespace Main.Units.Player.Items.Stats
{
    [CreateAssetMenu(menuName = "Custom/Drop Items/Create MainStatUp", fileName = "MainStatUp", order = 0)]
    public class MainStatUp : DropBase
    {
        protected override void InnerOnPickUp(GameObject playerGameObject)
        {
            playerGameObject.GetComponent<PlayerMainStats>().Agility += 1;
        }
    }
}