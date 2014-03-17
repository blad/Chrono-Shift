using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS113_Game
{
    public class FireGem : Gem
    {
        public FireGem()
            : base()
        {
            power = AbilityPower.FIRE;
            power_Cost = 2;
        }

        public override void ApplyAbility(Character c)
        {
            c.weaponEffect = Character.Effect.FIRE;
        }
    }
}
