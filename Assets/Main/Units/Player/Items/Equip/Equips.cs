using UnityEngine;

namespace Main.Units.Player.Items.Equip
{
    [CreateAssetMenu(menuName = "Custom/Drop Items/Equip/Helmet", fileName = "Helmet", order = 0)]
    public class HelmetEquip : EquipBase
    {
        protected override EquipBase GetEquippedItem(PlayerEquip playerEquip) => playerEquip.Helmet;

        protected override void SetEquip(PlayerEquip playerEquip)
        {
            playerEquip.Helmet = this;
        }
    }

    [CreateAssetMenu(menuName = "Custom/Drop Items/Equip/Shoulders", fileName = "Shoulders", order = 0)]
    public class ShouldersEquip : EquipBase
    {
        protected override EquipBase GetEquippedItem(PlayerEquip playerEquip) => playerEquip.Shoulders;

        protected override void SetEquip(PlayerEquip playerEquip)
        {
            playerEquip.Shoulders = this;
        }
    }

    [CreateAssetMenu(menuName = "Custom/Drop Items/Equip/Chest", fileName = "Chest", order = 0)]
    public class ChestEquip : EquipBase
    {
        protected override EquipBase GetEquippedItem(PlayerEquip playerEquip) => playerEquip.Chest;

        protected override void SetEquip(PlayerEquip playerEquip)
        {
            playerEquip.Chest = this;
        }
    }

    [CreateAssetMenu(menuName = "Custom/Drop Items/Equip/Gloves", fileName = "Gloves", order = 0)]
    public class GlovesEquip : EquipBase
    {
        protected override EquipBase GetEquippedItem(PlayerEquip playerEquip) => playerEquip.Gloves;

        protected override void SetEquip(PlayerEquip playerEquip)
        {
            playerEquip.Gloves = this;
        }
    }

    [CreateAssetMenu(menuName = "Custom/Drop Items/Equip/Pants", fileName = "Pants", order = 0)]
    public class PantsEquip : EquipBase
    {
        protected override EquipBase GetEquippedItem(PlayerEquip playerEquip) => playerEquip.Pants;

        protected override void SetEquip(PlayerEquip playerEquip)
        {
            playerEquip.Pants = this;
        }
    }

    [CreateAssetMenu(menuName = "Custom/Drop Items/Equip/Boots", fileName = "Boots", order = 0)]
    public class BootsEquip : EquipBase
    {
        protected override EquipBase GetEquippedItem(PlayerEquip playerEquip) => playerEquip.Boots;

        protected override void SetEquip(PlayerEquip playerEquip)
        {
            playerEquip.Boots = this;
        }
    }
}