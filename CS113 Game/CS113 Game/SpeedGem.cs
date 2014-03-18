using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS113_Game
{
    class SpeedGem : Gem
    {
        public SpeedGem()
            : base()
        {
            power = AbilityPower.SPEED;
            power_Cost = .5f;
            gemName = "SPEED";
        }

        public override void ApplyAbility(Character c)
        {
            c.weaponEffect = Character.Effect.SPEED;
            c.Speed = 10;
        }
    }
}
