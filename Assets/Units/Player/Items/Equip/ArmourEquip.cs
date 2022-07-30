using UnityEngine;

namespace Units.Player.Items.Equip
{
    [CreateAssetMenu(menuName = "Custom/Items/ArmourEquip", fileName = "ArmourEquip", order = 0)]
    public class ArmourEquip : EquipBase
    {
        public ArmourType ArmourType;
        public int Defence;
    }
}