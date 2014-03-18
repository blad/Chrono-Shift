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
    public class Boss : Enemy
    {
        public Boss(Game1 game, Vector2 position)
            : base(game)
        {
            health = 2000;
            movement_Speed = 2;
            this.position = position;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/Robama");
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Texture.Width * 2, character_Texture.Height * 2);
            texture_Offset = character_Texture.Height * 2;
        }
    }
}
