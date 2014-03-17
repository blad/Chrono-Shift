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
    public class AncientEgyptLevel : Level
    {
        public AncientEgyptLevel(Game1 game)
            : base(game)
        {
            gameRef = game;

            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            Game1.backgroundMusic.Stop();
            Game1.backgroundMusic = Game1.content_Manager.Load<SoundEffect>("SoundEffects/BackgroundMusic/Ancient_Egypt_BGM").CreateInstance();
            Game1.backgroundMusic.Play();

            level_Texture = Game1.content_Manager.Load<Texture2D>("Backgrounds/Levels/AE_bg_01");
            ground_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/TestGround");


            level_Rect = new Rectangle(0, 0, level_Texture.Width, level_Texture.Height);

            ground_Rect = new Rectangle(0, Game1.screen_Height - ground_Texture.Height - 50,
                                            ground_Texture.Width, ground_Texture.Height);

            //Everything between this and the next comment are platforms that are made of only textures and rectangles

            platform_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Platforms/SimplePlatform");

            platformList.Add(new Rectangle(100, 600, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(500, 400, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(1500, 400, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(400, 600, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(800, 400, platform_Texture.Width, platform_Texture.Height));
            platformList.Add(new Rectangle(1800, 400, platform_Texture.Width, platform_Texture.Height));
            //end of platforms


            //spawners
            Spawner spawner_1 = new Spawner(gameRef, new Vector2(1000.0f, 250.0f), Spawner.EnemyType.mummy);
            spawners.Add(spawner_1);

            Spawner spawner_4 = new Spawner(gameRef, new Vector2(1100.0f, 600.0f), Spawner.EnemyType.mummy);
            spawners.Add(spawner_4);

            Spawner spawner_2 = new Spawner(gameRef, new Vector2(2000.0f, 250.0f), Spawner.EnemyType.mummy);
            spawners.Add(spawner_2);

            Spawner spawner_3 = new Spawner(gameRef, new Vector2(2750.0f, 300.0f), Spawner.EnemyType.Sphinx);
            spawner_3.Max_Enemies = 1;
            spawners.Add(spawner_3);
            //end of spawners

            text_Editor.word("ANCIENT EGYPT!", new Vector2(350, 200), 0.5f);
        }
    }
}
