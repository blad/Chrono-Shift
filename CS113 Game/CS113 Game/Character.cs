using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CS113_Game
{
    public abstract class Character : DrawableGameComponent 
    {
        protected Vector2 previousPosition;
        protected Rectangle character_Rect;
        protected Texture2D character_Texture;
        protected enum direction { left, right };
        protected Color color_Tint;
        protected int movement_Speed;
        protected int gravity = 1;
        protected int jump_Speed = 30;
        protected int texture_Offset;
        
        public Vector2 position;
        public Rectangle standing_Platform;
        public int health;
        public bool grounded = false;
        public bool jumping = false;
        public bool falling = false;



        public Character(Game game)
            : base(game)
        {

        }

        //every character in the game should be affected by gravity
        public void activeGravity()
        {
            if (!grounded)
            {
                //decrease the jump speed by gravity and change the y position based off this speed
                jump_Speed = jump_Speed - gravity;
                previousPosition.Y = position.Y;
                position.Y = position.Y - jump_Speed;

                if (jump_Speed < 0)
                {
                    falling = true;
                    jumping = false;
                }
            }
            //if we are not longer on the platform then we should fall
            if (standing_Platform != null && ((position.X + character_Rect.Width < standing_Platform.X)
                                                || (position.X > standing_Platform.X + standing_Platform.Width)
                                                && !falling
                                                && !jumping))
            {
                grounded = false;
                falling = true;
            }
        }

        
        //methods that affect the character
        public void moveLeft()
        {
            position.X = position.X - movement_Speed;
        }

        public void moveRight()
        {
            position.X = position.X + movement_Speed;
        }

        public void takeDamage(int damage)
        {
            health = health - damage;
            color_Tint = Color.Red;
        }



        //get methods
        public Vector2 getPosition()
        {
            return position;
        }

        public int getSpeed()
        {
            return movement_Speed;
        }

        public Rectangle getCharacterRect()
        {
            return character_Rect;
        }

        public Texture2D getCharacterTexture()
        {
            return character_Texture;
        }

        //if the sprite has landed on somethng they will be grounded at the position passed
        //and have all gravity variables reset
        public void ground(float positionY)
        {
            grounded = true;
            jumping = false;
            falling = false;
            jump_Speed = 0;
            position.Y = positionY - texture_Offset;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
