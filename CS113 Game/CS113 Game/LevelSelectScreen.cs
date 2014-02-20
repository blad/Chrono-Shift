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
    public class LevelSelectScreen : Screen
    {

        private Texture2D background;
        private Rectangle background_Rect;
        private enum Levels { Egypt, Space, Dino, UCI, WWII };
        private Levels selected_Level;
        private ArrayList buttons;

        private Button egyptButton;
        private Button prehistoricButton;

        Game1 gameRef;

        public LevelSelectScreen(Game1 game)
        {
            gameRef = game;

            background = Game1.content_Manager.Load<Texture2D>("Backgrounds/Menus/LevelSelectTemp");
            background_Rect = new Rectangle(0, 0, background.Width, background.Height);

            buttons = new ArrayList();

            egyptButton = new Button(35, 320, 150, 150);
            buttons.Add(egyptButton);

            prehistoricButton = new Button(305, 195, 150, 150);
            buttons.Add(prehistoricButton);
        }


        private void loadLevel()
        {
            switch(selected_Level)
            {
                case Levels.Egypt :
                    Game1.addScreenToStack(new GameScreen(gameRef, new AncientEgyptLevel(gameRef)));
                    break;

                case Levels.Dino :
                    Game1.addScreenToStack(new GameScreen(gameRef, new PrehistoricLevel(gameRef)));
                    break;

            }

        }

        public override void Update(GameTime gameTime, InputHandler handler)
        {
            if (egyptButton.buttonPressed(handler))
            {
                selected_Level = Levels.Egypt;
                loadLevel();
            }

            if (prehistoricButton.buttonPressed(handler))
            {
                selected_Level = Levels.Dino;
                loadLevel();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, background_Rect, Color.White);

            foreach (Button button in buttons)
                button.Draw(spriteBatch);
        }
    }
}
