using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;


namespace CS113_Game
{
    public class PrehistoricLevel : Level
    {
        public PrehistoricLevel(Game1 game)
            : base(game)
        {
            gameRef = game;

            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            level_Texture = Game1.content_Manager.Load<Texture2D>("Backgrounds/Levels/PE_BG");
            ground_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/TestGround");


            level_Rect = new Rectangle(0, 0, level_Texture.Width, level_Texture.Height);

            ground_Rect = new Rectangle(0, Game1.screen_Height - ground_Texture.Height - 50,
                                            ground_Texture.Width, ground_Texture.Height);


        }
    }
}
