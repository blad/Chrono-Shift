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
    public abstract class PlayableCharacter : Character
    {
        protected String character_Name;
        
        protected Weapon equipped_Weapon;
        protected bool moving = false;
      

        public PlayableCharacter(Game1 game)
            : base(game)
        {
            DrawOrder = 100;
            movement_Speed = 4;
        }

        public void movement(InputHandler handler)
        {

            //if we have pressed space and we are on the ground
            if (handler.keyReleased(Keys.Space) && grounded)
            {
                jump_Speed = 20;
                jumping = true;
                grounded = false;
            }

            //if we press A or D then we should move the character appropriately 
            if (InputHandler.Current_Keyboard_State().IsKeyDown(Keys.A))
            {
                previousPosition.X = position.X;
                position.X = position.X - movement_Speed;

                if (position.X < 0)
                    position.X = 0;
            }
            else if (InputHandler.Current_Keyboard_State().IsKeyDown(Keys.D))
            {
                previousPosition.X = position.X;
                position.X = position.X + movement_Speed;
            }
        }

        public void fireWeapon(Point mousePoint, int offset)
        {
            equipped_Weapon.fire(mousePoint, offset);
        }


        public void Update(GameTime gameTime, InputHandler handler, int offset)
        {
            activeGravity();
            movement(handler);
            character_Rect.X = (int) position.X;
            character_Rect.Y = (int) position.Y;

            //if (!position.Equals(previousPosition))
            equipped_Weapon.changePosition(position);

            if (handler.leftMouseClicked())
                fireWeapon(handler.mousePosition(), offset);

            equipped_Weapon.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(character_Texture, character_Rect, Color.White);
            equipped_Weapon.Draw(spriteBatch);
        }

    }
}
