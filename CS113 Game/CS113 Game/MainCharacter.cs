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
        public MainCharacter(Game1 game, InputHandler myHandler, Texture2D characterTexture, Vector2 position, int myNumber)
            : base(game, myHandler)
        {
            this.characterNumber = myNumber;
            character_Texture = characterTexture;
            this.position = position;

            equipped_Weapon = new Pistol(game, this, false, this.position);
            has_Weapon = true;

            if (characterNumber == 1)
                identifyArrow = Game1.content_Manager.Load<Texture2D>("Sprites/Misc/BlueCharacterArrow");
            else
                identifyArrow = Game1.content_Manager.Load<Texture2D>("Sprites/Misc/RedCharacterArrow");

            arrowRect = new Rectangle((int)position.X + character_Rect.Width/4, (int)position.Y, identifyArrow.Width, identifyArrow.Height);
        }

    }
}
