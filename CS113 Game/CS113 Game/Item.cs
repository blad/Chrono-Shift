using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public abstract class Item : DrawableGameComponent
    {
        protected Rectangle item_Rect;
        protected Texture2D item_Texture;
        protected Vector2 position;

        protected Game1 gameRef;

        public Item(Game1 game, Vector2 position)
            : base(game)
        {
            game = gameRef;
        }


        public void Update()
        {
            if (item_Rect.Intersects(Level.current_Character.getCharacterRect()))
            {
                pickUp(Level.current_Character);
            }

            if (position.Y < Level.ground_Rect.Y - item_Texture.Height)
            {
                position.Y += 5;
                item_Rect.Y = (int)position.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(item_Texture, item_Rect, Color.White);
        }

        public abstract void pickUp(MainCharacter character);
    }
}
