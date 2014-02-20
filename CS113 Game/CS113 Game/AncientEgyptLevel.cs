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
    public class AncientEgyptLevel : Level
    {
        public AncientEgyptLevel(Game1 game)
            : base(game)
        {
            gameRef = game;

            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            level_Texture = Game1.content_Manager.Load<Texture2D>("Backgrounds/Levels/AE_bg_01");
            ground_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/TestGround");


            level_Rect = new Rectangle(0, 0, level_Texture.Width, level_Texture.Height);

            ground_Rect = new Rectangle(0, Game1.screen_Height - ground_Texture.Height - 50,
                                            ground_Texture.Width, ground_Texture.Height);

            //Everything between this and the next comment are platforms that are made of only textures and rectangles

            platform_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/SimplePlatform");

            platformList.Add(new Rectangle(100, 600, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(300, 400, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(800, 400, platform_Texture.Width, platform_Texture.Height));

            //end of platforms

            spawners.Add(new Spawner(gameRef, new Vector2(500.0f, 300.0f)));
            spawners.Add(new Spawner(gameRef, new Vector2(2000.0f, 300.0f)));
        }
    }
}
