﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public class PowerPack : Item
    {
        private int power_Restored = 15;

        public PowerPack(Game1 game, Vector2 position)
            :base (game, position)
        {
            this.position = position;
            item_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Pickups/power_pack");
            item_Rect = new Rectangle((int)position.X, (int)position.Y, item_Texture.Width, item_Texture.Height);
        }

        public override void pickUp(MainCharacter character)
        {
            character.changePower(power_Restored); //a simple way to add health back to the character with the damage method
            
            Level.itemList.Remove(this);
        }
    }
}
