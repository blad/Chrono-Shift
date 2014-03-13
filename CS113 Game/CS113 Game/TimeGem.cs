using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS113_Game
{
    public class TimeGem : Gem
    {
        public TimeGem()
            : base()
        {
            power = AbilityPower.NORMAL;
            power_Cost = 0;
        }

        public override void ApplyAbility()
        {
            Level.current_Character.weaponEffect = Character.Effect.NORMAL;
        }
    }
}
