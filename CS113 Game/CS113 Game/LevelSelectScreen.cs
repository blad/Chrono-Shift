using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace CS113_Game
{
    public class LevelSelectScreen : Screen
    {

        private Texture2D background;
        private Rectangle background_Rect;
        private Texture2D arrowSelect;
        private int arrowX = 50;
        private int arrowY = 30;
        private Rectangle arrowRect;
        private enum Levels { Egypt, Space, Dino, UCI, WWII };
        private Levels selected_Level;
        private LinkedList<Button> buttons;
        private LinkedListNode<Button> currentButton;

        private Button egyptButton;
        private Button prehistoricButton;
        private Button FutureEarthButton;
        private Button WWIIButton;
        private Button UCIButton;

        private int ButtonWidth = 145;
        private int ButtonHeight = 195;

        public static LinkedList<Gem> characterGems = new LinkedList<Gem>();

        Game1 gameRef;

        public LevelSelectScreen(Game1 game)
        {
            gameRef = game;
            Game1.backgroundMusic.Volume = 1.0f;
            characterGems.AddLast(new TimeGem());

            background = Game1.content_Manager.Load<Texture2D>("Backgrounds/Menus/stage_select_bg");
            background_Rect = new Rectangle(0, 0, background.Width, background.Height);

            

            buttons = new LinkedList<Button>();

            egyptButton = new Button(85, 95, ButtonWidth, ButtonHeight);
            buttons.AddLast(egyptButton);

            prehistoricButton = new Button(295, 440, ButtonWidth, ButtonHeight);
            buttons.AddLast(prehistoricButton);

            FutureEarthButton = new Button(560, 95, ButtonWidth, ButtonHeight);
            buttons.AddLast(FutureEarthButton);

            WWIIButton = new Button(745, 440, ButtonWidth, ButtonHeight);
            buttons.AddLast(WWIIButton);

            UCIButton = new Button(1005, 95, ButtonWidth, ButtonHeight);
            buttons.AddLast(UCIButton);

            arrowSelect = Game1.content_Manager.Load<Texture2D>("Sprites/Buttons/SelectArrow");
            arrowRect = new Rectangle(egyptButton.Rect.X + arrowX, egyptButton.Rect.Y - arrowY, arrowSelect.Width, arrowSelect.Height);

            currentButton = buttons.First;
        }


        private void loadLevel()
        {
            switch(selected_Level)
            {
                case Levels.Egypt :
                    Game1.addScreenToStack(new GameScreen(gameRef, new AncientEgyptLevel(gameRef, "ANCIENT EGYPT")));
                    break;

                case Levels.Dino :
                    Game1.addScreenToStack(new GameScreen(gameRef, new PrehistoricLevel(gameRef, "PREHISTORIC EARTH")));
                    break;
                case Levels.Space:
                    Game1.addScreenToStack(new GameScreen(gameRef, new FutureEarth(gameRef, "FUTURE EARTH")));
                    break;
                case Levels.WWII :
                    Game1.addScreenToStack(new GameScreen(gameRef, new WWIILevel(gameRef, "NORMANDY")));
                    break;
                case Levels.UCI :
                    Game1.addScreenToStack(new GameScreen(gameRef, new UCILevel(gameRef, "UC IRVINE")));
                    break;
            }

        }

        public override void Update(GameTime gameTime, InputHandler handler)
        {
            gameRef.IsMouseVisible = true;

            if (handler.buttonPressed(Buttons.DPadRight))
            {
                if (currentButton.Next != null)
                    currentButton = currentButton.Next;
                else
                    currentButton = buttons.First;

                arrowRect.X = currentButton.Value.Rect.X + arrowX;
                arrowRect.Y = currentButton.Value.Rect.Y - arrowY;
            }

            else if (handler.buttonPressed(Buttons.DPadLeft))
            {
                if (currentButton.Previous != null)
                    currentButton = currentButton.Previous;
                else
                    currentButton = buttons.Last;

                arrowRect.X = currentButton.Value.Rect.X + arrowX;
                arrowRect.Y = currentButton.Value.Rect.Y - arrowY;
            }

            if (handler.buttonPressed(Buttons.A))
            {
                if (currentButton.Value == egyptButton)
                {
                    selected_Level = Levels.Egypt;
                }
                else if (currentButton.Value == prehistoricButton)
                {
                    selected_Level = Levels.Dino;
                }
                else if (currentButton.Value == FutureEarthButton)
                {
                    selected_Level = Levels.Space;
                }
                else if (currentButton.Value == WWIIButton)
                {
                    selected_Level = Levels.WWII;
                }
                else if (currentButton.Value == UCIButton)
                {
                    selected_Level = Levels.UCI;
                }


                loadLevel();
            }
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(arrowSelect, arrowRect, Color.White);
            spriteBatch.Draw(background, background_Rect, Color.White);
            

            foreach (Button button in buttons)
                button.Draw(spriteBatch);
        }
    }

}
