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
    public class GameScreen : Screen
    {
        private Level current_Level;
        
        
        Game1 gameRef;

        //adds the InputHanlder to the class
        //creates a list of characters
        //adds a level and ground platform texture
        public GameScreen(Game1 game, Level level)
        {
            gameRef = game;
            current_Level = level;
            
        }

        //updates all the characters in the character list
        public override void Update(GameTime gameTime, InputHandler handler)
        {
            current_Level.Update(gameTime, handler);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            current_Level.Draw(gameTime);
        }
    }
}
