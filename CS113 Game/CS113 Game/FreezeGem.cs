using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS113_Game
{
    public class FreezeGem : Gem
    {
        public FreezeGem()
            : base()
        {
            power = AbilityPower.ICE;
            power_Cost = 3;
            gemName = "FREEZE";
        }

        public override void ApplyAbility(Character c)
        {
            c.weaponEffect = Character.Effect.ICE;
        }
    }
}
