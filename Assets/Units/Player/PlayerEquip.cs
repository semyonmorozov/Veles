using System.Collections.Generic;
using System.Linq;
using Units.Player.Items;
using Units.Player.Items.Equip;
using UnityEngine;

namespace Units.Player
{
    public class PlayerEquip : MonoBehaviour
    {
        public List<ArmourEquip> ArmourEquip;

        public int Defence()
        {
            return ArmourEquip.Sum(x => x.Defence);
        }
    }
}