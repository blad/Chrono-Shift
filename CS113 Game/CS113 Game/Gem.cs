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
        public enum AbilityPower{ NORMAL, FIRE, ICE, SPEED}
        protected AbilityPower power;
        protected Color ability_Color;
        protected int power_Cost;

        public int Cost
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
