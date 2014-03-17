using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CS113_Game
{
    public class StartScreen : Screen
    {
        private ArrayList buttons;
        private Button start_Button;
        private Game1 gameRef;
        private Texture2D background;
        private Rectangle background_Rect;

        public StartScreen(Game1 game)
        {
            gameRef = game;
            buttons = new ArrayList();

            background = Game1.content_Manager.Load<Texture2D>("Backgrounds/Menus/TitleScreen");
            background_Rect = new Rectangle(0, 0, background.Width, background.Height);

            start_Button = new Button(470, 550, 290, 50);
            buttons.Add(start_Button);
        }

        public override void Update(GameTime gameTime, InputHandler handler)
        {
            if (handler.buttonPressed((Buttons.Start)))
            {
                Game1.addScreenToStack(new LevelSelectScreen(gameRef));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw( background, background_Rect, Color.White);

            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
        
    }
}
