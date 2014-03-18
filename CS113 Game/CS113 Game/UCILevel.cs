using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CS113_Game
{
    public class UCILevel : Level
    {
        public UCILevel(Game1 game)
            : base(game)
        {
            gameRef = game;

            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            level_Texture = Game1.content_Manager.Load<Texture2D>("Backgrounds/Levels/UCI_Panorama1");
            ground_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/TestGround");


            level_Rect = new Rectangle(0, 0, level_Texture.Width, level_Texture.Height);

            ground_Rect = new Rectangle(0, Game1.screen_Height - ground_Texture.Height - 50,
                                            ground_Texture.Width * 2, ground_Texture.Height);

            platform_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/SimplePlatform");

            platformList.Add(new Rectangle(100, 600, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(500, 400, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(1500, 400, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(400, 600, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(800, 400, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(1800, 400, platform_Texture.Width, platform_Texture.Height));

            Spawner spawner_1 = new Spawner(gameRef, new Vector2(1750.0f, 300.0f), Spawner.EnemyType.swagman);
            spawner_1.Max_Enemies = 4;
            spawners.Add(spawner_1);

            Spawner spawner_2 = new Spawner(gameRef, new Vector2(1750.0f, 300.0f), Spawner.EnemyType.bobaBooth);
            spawner_2.Max_Enemies = 1;
            spawners.Add(spawner_2);
        }
    }
}
