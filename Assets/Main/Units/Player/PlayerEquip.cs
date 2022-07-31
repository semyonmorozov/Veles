using System.Collections.Generic;
using System.Linq;
using Main.Units.Player.Items.Equip;
using UnityEngine;

namespace Main.Units.Player
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