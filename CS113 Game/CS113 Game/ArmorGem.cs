using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS113_Game
{
    public class ArmorGem : Gem
    {
        public ArmorGem()
            : base()
        {
            power = AbilityPower.ARMOR;
            power_Cost = 2;
            gemName = "ARMOR";
        }

        public override void ApplyAbility(Character c)
        {
            c.weaponEffect = Character.Effect.ARMOR;
        }
    }
}
