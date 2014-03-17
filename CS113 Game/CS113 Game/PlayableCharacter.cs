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
        protected InputHandler myHandler;
        protected Texture2D identifyArrow;
        protected Rectangle arrowRect;

        public LinkedList<Gem> Gems
        {
            get { return GemList; }
            set { GemList = value; }
        }

        public PlayableCharacter(Game1 game, InputHandler myHandler)
            : base(game)
        {

            DrawOrder = 100;
            this.myHandler = myHandler;


            isPlayer = true;
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
        public void Input()
        {
            //if we press E or Q we switch between our effects (gems)
            if ((myHandler.keyPressed(Keys.E) || myHandler.buttonPressed(Buttons.RightShoulder)) && GemList.Count > 0)
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

            else if ((myHandler.keyPressed(Keys.Q) || myHandler.buttonPressed(Buttons.LeftShoulder)) && GemList.Count > 0)
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
        public void movement(GameTime gameTime)
        {
            time_Passed += gameTime.ElapsedGameTime.Milliseconds;

            //if we have pressed space and we are on the ground
            if ((myHandler.keyPressed(Keys.Space) || myHandler.buttonPressed(Buttons.A))  && grounded)
            {
                jump_Speed = base_Jump_Speed;
                jumping = true;
                grounded = false;
            }

            //if we press A or D then we should move the character appropriately 
            if (myHandler.Current_Keyboard_State().IsKeyDown(Keys.A) || myHandler.Current_GamePad_State().ThumbSticks.Left.X < 0)
            {
                moveLeft();
                return;

            }
            else if (myHandler.Current_Keyboard_State().IsKeyDown(Keys.D) || myHandler.Current_GamePad_State().ThumbSticks.Left.X > 0)
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
            Level.HUD.shortenHealthBar(health, this.characterNumber);

            if (!effect_Active)
                color_Tint = Color.OrangeRed;
        }


        public void addHealth(int added)
        {
            health += added;
            if (health > 100)
                health = 100;

            Level.HUD.shortenHealthBar(health, this.characterNumber);
        }

        //this will handler all other types of input aside from moving the character
        //things like changing weapons or abilities

        //seems pointless but we may need to add things here
        public void fireWeapon()
        {
            equipped_Weapon.fire(myHandler);
        }

        //
        public override void Update(GameTime gameTime)
        {

            previous_Game_Time = current_Game_Time;
            current_Game_Time = gameTime;

            if (currentGem != null && power >= currentGem.Cost)
                currentGem.ApplyAbility(this);
            else
                weaponEffect = Effect.NORMAL;

            if (health > 100)
                health = 100;

            activeGravity();
            movement(gameTime);
            Input();

            character_Rect.X = (int) position.X;
            character_Rect.Y = (int) position.Y;
            arrowRect.X = (int)position.X + character_Rect.Width/5;
            arrowRect.Y = (int)position.Y - 50;

            equipped_Weapon.changePosition(position);

            if (myHandler.Current_GamePad_State().ThumbSticks.Right.X < 0)
            {
                facing = direction.left;

            }
            else if (myHandler.Current_GamePad_State().ThumbSticks.Right.X > 0)
            {
                facing = direction.right;
            }


            if ((myHandler.Current_GamePad_State().IsButtonDown(Buttons.RightTrigger) && equipped_Weapon.fireType == Weapon.FireType.Auto)
                || myHandler.buttonPressed(Buttons.RightTrigger) && equipped_Weapon.fireType == Weapon.FireType.SingleShot)
            {
                fireWeapon();
            }

            equipped_Weapon.Update(gameTime, myHandler);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(character_Texture, character_Rect, sprite_Rect, Color.White, 0.0f, origin, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(identifyArrow, arrowRect, Color.White);
            equipped_Weapon.Draw(spriteBatch);
        }

    }
}
