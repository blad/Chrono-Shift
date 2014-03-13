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
        private Button WWIIButton;

        private int ButtonWidth = 145;
        private int ButtonHeight = 195;

        public static LinkedList<Gem> characterGems = new LinkedList<Gem>();

        Game1 gameRef;

        public LevelSelectScreen(Game1 game)
        {
            gameRef = game;

            characterGems.AddLast(new TimeGem());

            background = Game1.content_Manager.Load<Texture2D>("Backgrounds/Menus/stage_select_bg");
            background_Rect = new Rectangle(0, 0, background.Width, background.Height);

            buttons = new ArrayList();

            egyptButton = new Button(85, 95, ButtonWidth, ButtonHeight);
            buttons.Add(egyptButton);

            prehistoricButton = new Button(295, 440, ButtonWidth, ButtonHeight);
            buttons.Add(prehistoricButton);

            WWIIButton = new Button(745, 440, ButtonWidth, ButtonHeight);
            buttons.Add(WWIIButton);
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
                case Levels.WWII :
                    Game1.addScreenToStack(new GameScreen(gameRef, new WWIILevel(gameRef)));
                    break;
            }

        }

        public override void Update(GameTime gameTime, InputHandler handler)
        {
            gameRef.IsMouseVisible = true;

            if (egyptButton.buttonPressed(handler))
            {
                selected_Level = Levels.Egypt;
                loadLevel();
            }

            else if (prehistoricButton.buttonPressed(handler))
            {
                selected_Level = Levels.Dino;
                loadLevel();
            }
            else if (WWIIButton.buttonPressed(handler))
            {
                selected_Level = Levels.WWII;
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
