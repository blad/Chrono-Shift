using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CS113_Game
{
    public class MainCharacter : PlayableCharacter
    {
        public MainCharacter(Game1 game, Texture2D characterTexture, Vector2 position)
            : base(game)
        {
            character_Texture = characterTexture;
            this.position = position;

            equipped_Weapon = new Pistol(game, this, false, this.position);
            has_Weapon = true;
        }

    }
}
