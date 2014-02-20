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
        protected Vector2 origin;
        protected Rectangle sprite_Rect;
        protected bool moving = false;

        //the size of the character that we need to accomdate for the sprite sheet
        private int character_Width = 90;
        private int character_Height = 210;
        private int sprite_Count = 10;
        private int current_Sprite_Count = 0;
        private int time_Per_Animation = 90; //every 90 ms we change animation
        private int time_Passed = 0;

        private direction facing;

        public PlayableCharacter(Game1 game)
            : base(game)
        {
            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width, character_Height);
            texture_Offset = character_Height;

            DrawOrder = 100;
            movement_Speed = 6;

            facing = direction.right;
        }

        public Rectangle movement(InputHandler handler, GameTime gameTime)
        {
            time_Passed += gameTime.ElapsedGameTime.Milliseconds;

            //if we have pressed space and we are on the ground
            if (handler.keyPressed(Keys.Space)  && grounded)
            {
                jump_Speed = 20;
                jumping = true;
                grounded = false;
            }

            //if we press A or D then we should move the character appropriately 
            if (InputHandler.Current_Keyboard_State().IsKeyDown(Keys.A) || InputHandler.Current_GamePad_State().ThumbSticks.Left.X < 0)
            {
                moveLeft();

                sprite_Rect.Y = character_Height * 2;
                sprite_Rect.X = character_Width * current_Sprite_Count;

                //if enough time has passed, move on to the next animation
                if (time_Passed > time_Per_Animation)
                {
                    current_Sprite_Count++;
                    time_Passed = 0;
                }

                if (current_Sprite_Count > sprite_Count)
                    current_Sprite_Count = 0;

                if (position.X < 0)
                    position.X = 0;

                facing = direction.left;

                return sprite_Rect;
            }
            else if (InputHandler.Current_Keyboard_State().IsKeyDown(Keys.D) || InputHandler.Current_GamePad_State().ThumbSticks.Left.X > 0)
            {
                moveRight();

                sprite_Rect.Y = character_Height;
                sprite_Rect.X = character_Width * current_Sprite_Count;

                if (time_Passed > time_Per_Animation)
                {
                    current_Sprite_Count++;
                    time_Passed = 0;
                }

                if (current_Sprite_Count > sprite_Count)
                    current_Sprite_Count = 0;

                facing = direction.right;

                return sprite_Rect;
            }

            // if we are just standing reset our offsets to the standing animations of the sprite sheet
            //change these hard numbers to variables
            //REMINDER TO CHANGE THE SHIT IN THE PART OF CODE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            sprite_Rect.Y = 0;
            current_Sprite_Count = 0;

            if (facing == direction.left)
                sprite_Rect.X = 110;
            else
                sprite_Rect.X = 0;

            return sprite_Rect;
        }

        public void fireWeapon(Point mousePoint, int offset)
        {
            equipped_Weapon.fire(mousePoint, offset);
        }


        public void Update(GameTime gameTime, InputHandler handler)
        {
            activeGravity();
            movement(handler, gameTime);
            character_Rect.X = (int) position.X;
            character_Rect.Y = (int) position.Y;

            //if (!position.Equals(previousPosition))
            equipped_Weapon.changePosition(position);

            if ((InputHandler.Current_Mouse_State().LeftButton == ButtonState.Pressed && equipped_Weapon.fireType == Weapon.FireType.Auto)
                || handler.leftMouseClicked() && equipped_Weapon.fireType == Weapon.FireType.SingleShot)
            {
                fireWeapon(handler.mousePosition(), Level.screen_Offset);
            }

            equipped_Weapon.Update(gameTime, handler);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(character_Texture, character_Rect, sprite_Rect, Color.White, 0.0f, origin, SpriteEffects.None, 1.0f);
            equipped_Weapon.Draw(spriteBatch);
        }

    }
}
