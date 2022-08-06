using System.Collections.Generic;
using System.Linq;
using Main.Units.Player.Items.Equip;
using UnityEngine;

namespace Main.Units.Player
{
    public class PlayerEquip : MonoBehaviour
    {
        public HelmetEquip Helmet;
        public ShouldersEquip Shoulders;
        public ChestEquip Chest;
        public GlovesEquip Gloves;
        public PantsEquip Pants;
        public BootsEquip Boots;
        
        
        public int Defence()
        {
            return 0;
        }
    }
}