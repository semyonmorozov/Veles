using System.Linq;
using UnityEngine;

namespace Units.Player.Items.Equip
{
    public class PickUpEquip : PickUpItemBase
    {
        [HideInInspector]
        public EquipBase Equip;
        protected override void Awake()
        {
            base.Awake();
            Name = Equip.Name;
        }

        public override void OnPickUp(GameObject playerGameObject)
        {
            switch (Equip.EquipType)
            {
                case EquipType.Armour:
                    var armourEquip = (ArmourEquip)Equip;
                    var playerArmourEquip = playerGameObject.GetComponent<PlayerEquip>().ArmourEquip;
                    var equippedArmour = playerArmourEquip.FirstOrDefault(x => x.ArmourType == armourEquip.ArmourType);
                    if (equippedArmour != null)
                    {
                        playerArmourEquip.Remove(equippedArmour);
                        equippedArmour.RemoveEquipModel(playerGameObject);
                    }
                    else
                    {
                        armourEquip.RemoveDefaultModel(playerGameObject);
                    }
                    
                    playerArmourEquip.Add(armourEquip);
                    armourEquip.SetEquipModel(playerGameObject);

                    break;
                case EquipType.Weapon:
                    break;
            }
        }
    }
}