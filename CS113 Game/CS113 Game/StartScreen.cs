using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public class StartScreen : Screen
    {
        ArrayList buttons;
        Button start_Button;
        ContentManager game_Content;
        Game1 gameRef;

        public StartScreen(Game1 game, ContentManager content)
        {
            gameRef = game;
            game_Content = content;
            buttons = new ArrayList();

            Texture2D buttonTexture = game_Content.Load<Texture2D>("Sprites/Buttons/TestStartButton");

            start_Button = new Button(buttonTexture, 
                                        GraphicsDeviceManager.DefaultBackBufferWidth/2 - buttonTexture.Width/2,
                                        GraphicsDeviceManager.DefaultBackBufferHeight/2 - buttonTexture.Height/2);
            buttons.Add(start_Button);
        }

        public override void Update(GameTime gameTime, InputHandler handler)
        {
            if (start_Button.buttonPressed(handler))
            {
                Game1.addScreenToStack(new GameScreen(gameRef, game_Content));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
        
    }
}
