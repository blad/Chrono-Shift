using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace CS113_Game
{
    public class WWIILevel : Level
    {
        public WWIILevel(Game1 game)
            : base(game)
        {
            gameRef = game;

            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            level_Texture = Game1.content_Manager.Load<Texture2D>("Backgrounds/Levels/WWII_BG1");
            ground_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/TestGround");


            level_Rect = new Rectangle(0, 0, level_Texture.Width, level_Texture.Height);

            ground_Rect = new Rectangle(0, Game1.screen_Height - ground_Texture.Height - 50,
                                            ground_Texture.Width, ground_Texture.Height);

            //Everything between this and the next comment are platforms that are made of only textures and rectangles

            platform_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/SimplePlatform");

            //end of platforms
            Spawner spawner_1 = new Spawner(gameRef, new Vector2(1500.0f, 50.0f), Spawner.EnemyType.naziPlane);
            spawner_1.Max_Enemies = 2;
            spawners.Add(spawner_1);

            Spawner spawner_2 = new Spawner(gameRef, new Vector2(200.0f, 600.0f), Spawner.EnemyType.basic);
            spawner_2.Max_Enemies = 5;
            spawners.Add(spawner_2);

            Spawner spawner_4 = new Spawner(gameRef, new Vector2(1500.0f, 600.0f), Spawner.EnemyType.naziTank);
            spawner_4.Max_Enemies = 1;
            spawners.Add(spawner_4);

            //spawners

            //end of spawners


        }
    }
}

