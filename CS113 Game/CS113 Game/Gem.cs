using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    
    public abstract class Gem
    {
        protected Character character;
        public enum AbilityPower{ NORMAL, ARMOR, DAMAGE, FIRE, ICE, SPEED}
        public String gemName;
        protected AbilityPower power;
        protected Color ability_Color;
        protected float power_Cost;

        public float Cost
        {
            get { return power_Cost; }
        }

        public AbilityPower Power
        {
            get { return power; }
        }

        public Gem()
        {

        }

        public abstract void ApplyAbility(Character c);
    }
}
