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
            power_Cost = 5;
        }

        public override void ApplyAbility()
        {
            Level.current_Character.weaponEffect = Character.Effect.FIRE;
        }
    }
}
