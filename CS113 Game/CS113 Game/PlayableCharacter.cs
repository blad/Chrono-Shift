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
    public abstract class PlayableCharacter : Character
    {
        
        protected String character_Name;
        protected LinkedList<Gem> GemList;
        protected int abilityCount = 0;
        protected bool moving = false;

        public LinkedList<Gem> Gems
        {
            get { return GemList; }
            set { GemList = value; }
        }

        public PlayableCharacter(Game1 game)
            : base(game)
        {

            DrawOrder = 100;

            health = 100;
            base_Speed = 6;
            movement_Speed = base_Speed;

            facing = direction.right;

            character_Width = 90;
            character_Height = 210;
            sprite_Count = 10;
            current_Sprite_Count = 0;
            time_Per_Animation = 90; //every 90 ms we change animation
            time_Passed = 0;
            standing_X = 110;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width, character_Height);
            texture_Offset = character_Height;

            //the effecst will be normal, fire, and ice
        }

        //only the playable character should be able to mess with the effects
        public void Input(InputHandler handler)
        {
            //if we press E or Q we switch between our effects (gems)
            if ((handler.keyPressed(Keys.E) || handler.buttonPressed(Buttons.RightShoulder)) && GemList.Count > 0)
            {
                if (currentGem == null)
                {
                    currentGem = GemList.First.Value;
                }
                else
                {
                    if (GemList.Find(currentGem).Next == null)
                        currentGem = GemList.First.Value;
                    else
                        currentGem = GemList.Find(currentGem).Next.Value;
                }
            }

            else if ((handler.keyPressed(Keys.Q) || handler.buttonPressed(Buttons.LeftShoulder)) && GemList.Count > 0)
            {
                if (currentGem == null)
                {
                    currentGem = GemList.First.Value;
                }
                else
                {
                    if (GemList.Find(currentGem).Previous == null)
                        currentGem = GemList.Last.Value;
                    else
                        currentGem = GemList.Find(currentGem).Previous.Value;
                }
            }
        }

        //this method will take care of all the movement for the playable characters
        //if we press A we go to the left, D moves to the right
        //if we are falling or we press SPACE we must move vertically accordingly
        public void movement(InputHandler handler, GameTime gameTime)
        {
            time_Passed += gameTime.ElapsedGameTime.Milliseconds;

            //if we have pressed space and we are on the ground
            if (handler.keyPressed(Keys.Space)  && grounded)
            {
                jump_Speed = base_Jump_Speed;
                jumping = true;
                grounded = false;
            }

            //if we press A or D then we should move the character appropriately 
            if (InputHandler.Current_Keyboard_State().IsKeyDown(Keys.A) || InputHandler.Current_GamePad_State().ThumbSticks.Left.X < 0)
            {
                moveLeft();
                return;

            }
            else if (InputHandler.Current_Keyboard_State().IsKeyDown(Keys.D) || InputHandler.Current_GamePad_State().ThumbSticks.Left.X > 0)
            {
                moveRight();
                return;

            }


            // if we are just standing reset our offsets to the standing animations of the sprite sheet
            //change these hard numbers to variables
            //REMINDER TO CHANGE THE SHIT IN THE PART OF CODE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            sprite_Rect.Y = 0;
            current_Sprite_Count = 0;

            if (facing == direction.left)
                sprite_Rect.X = standing_X;
            else
                sprite_Rect.X = 0;
        }

        //the playable character needs their own movement methods to accomodate for the sprite sheet
        public new Rectangle moveLeft()
        {
            position.X = position.X - movement_Speed;
            facing = direction.left;

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

            return sprite_Rect;
        }

        public new Rectangle moveRight()
        {
            position.X = position.X + movement_Speed;
            facing = direction.right;

            sprite_Rect.Y = character_Height;
            sprite_Rect.X = character_Width * current_Sprite_Count;

            if (time_Passed > time_Per_Animation)
            {
                current_Sprite_Count++;
                time_Passed = 0;
            }

            if (current_Sprite_Count > sprite_Count)
                current_Sprite_Count = 0;

            return sprite_Rect;
        }

        //we will apply damage to players differently 
        //we need to adjust the health bar when a playable character is hit
        public new void takeDamage(int damage)
        {
            health = health - damage;
            Level.HUD.shortenHealthBar(health);

            if (!effect_Active)
                color_Tint = Color.OrangeRed;
        }

        //this will handler all other types of input aside from moving the character
        //things like changing weapons or abilities


        public void fireWeapon(Point mousePoint)
        {
            equipped_Weapon.fire(mousePoint);
        }

        //
        public void Update(GameTime gameTime, InputHandler handler)
        {

            previous_Game_Time = current_Game_Time;
            current_Game_Time = gameTime;

            if (currentGem != null && power >= currentGem.Cost)
                currentGem.ApplyAbility();
            else
                weaponEffect = Effect.NORMAL;

            if (health > 100)
                health = 100;

            activeGravity();
            movement(handler, gameTime);
            Input(handler);

            character_Rect.X = (int) position.X;
            character_Rect.Y = (int) position.Y;

            equipped_Weapon.changePosition(position);

            if ((InputHandler.Current_Mouse_State().LeftButton == ButtonState.Pressed && equipped_Weapon.fireType == Weapon.FireType.Auto)
                || handler.leftMouseClicked() && equipped_Weapon.fireType == Weapon.FireType.SingleShot)
            {
                fireWeapon(handler.mousePosition());
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
