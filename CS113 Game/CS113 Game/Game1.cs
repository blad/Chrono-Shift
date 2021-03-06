using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CS113_Game.VideoScreen;

namespace CS113_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int screen_Width = 1200;
        public static int screen_Height = 800;
        public static GameTime currentGameTime;
        public static GameTime previousGameTime;
        public static SoundEffectInstance backgroundMusic;

        private static Stack<Screen> screen_Stack;
        private static Screen current_Screen;

        public InputHandler input_Handler_Player1;
        public InputHandler input_Handler_Player2;
        public static ContentManager content_Manager;

        public static Camera2D cam;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screen_Width;
            graphics.PreferredBackBufferHeight = screen_Height;
            Content.RootDirectory = "Content";

            cam = new Camera2D();
            cam.position = new Vector2((float)-screen_Width / 2, (float)-screen_Height / 2);

            screen_Stack = new Stack<Screen>();
            input_Handler_Player1 = new InputHandler(PlayerIndex.One);
            input_Handler_Player2 = new InputHandler(PlayerIndex.Two);

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            content_Manager = Content;

            backgroundMusic = content_Manager.Load<SoundEffect>("SoundEffects/BackgroundMusic/Level_Select_BGM").CreateInstance();;
            backgroundMusic.IsLooped = true;
            backgroundMusic.Volume = 1.0f;
            backgroundMusic.Play();


            //addScreenToStack(new CutSceneMain(this));
            addScreenToStack(new StartScreen(this));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            previousGameTime = currentGameTime;
            currentGameTime = gameTime;

            // Allows the game to exit
            

            input_Handler_Player1.Update(gameTime);
            input_Handler_Player2.Update(gameTime);

            if (input_Handler_Player1.buttonPressed(Buttons.Back))
            {
                if (screen_Stack.Count > 1)
                    popScreenStack();
                else
                    this.Exit();
            }

            if (input_Handler_Player1.keyReleased(Keys.Escape))
            {
                IsMouseVisible = true;

                if (screen_Stack.Count == 1)
                {
                    this.Exit();
                }
                else
                    popScreenStack();
            }

            current_Screen.Update(gameTime, input_Handler_Player1);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, 
                                BlendState.AlphaBlend, 
                                null, null, null, null, 
                                cam.getTransformation(GraphicsDevice));

            current_Screen.Draw(gameTime, spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }

        public static void addScreenToStack(Screen screen)
        {
            screen_Stack.Push(screen);
            current_Screen = screen_Stack.Peek();
        }

        public static void popScreenStack()
        {
            screen_Stack.Pop();
            current_Screen = screen_Stack.Peek();

            if (screen_Stack.Count == 2)
            {
                backgroundMusic.Stop();
                backgroundMusic = content_Manager.Load<SoundEffect>("SoundEffects/BackgroundMusic/Level_Select_BGM").CreateInstance();
                backgroundMusic.Play();
            }
        }

    }
}
