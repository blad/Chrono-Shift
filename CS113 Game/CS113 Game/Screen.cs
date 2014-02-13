using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace CS113_Game
{
    public abstract class Screen
    {
        public abstract void Update(GameTime gameTime, InputHandler handler);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
