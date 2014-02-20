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
        public MainCharacter(Game1 game, Texture2D characterTexture)
            : base(game)
        {
            character_Texture = characterTexture;
            position = new Vector2(400.0f, 400.0f);

            equipped_Weapon = new AssaultRifle(game, position);
        }
    }
}
