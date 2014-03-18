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
        private int time_Per_Animation_Jump = 50;

        private Texture2D handsTexture;
        private Rectangle handsRect;
        private Rectangle handsSpriteRect;

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

            character_Width = 185;
            character_Height = 395;
            spriteRectOffset = character_Height;
            sprite_Count = 11;
            current_Sprite_Count = 0;
            time_Per_Animation = 90; //every 90 ms we change animation
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, (int)(character_Width * .75f),  (int)(character_Height * .75f));
            texture_Offset = (int)(character_Height * .75f);

            handsTexture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/akirahands");
            handsRect = new Rectangle((int)position.X, (int)position.Y + 50, (int)((handsTexture.Width/2)*.75f), (int)(handsTexture.Height * .75f));
            handsSpriteRect = new Rectangle(0, 0, handsTexture.Width / 2, handsTexture.Height);
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

                if (this.characterNumber == 1)
                    HUDManager.player1Gem = Level.text_Editor.updateWord(HUDManager.player1Gem, currentGem.gemName.ToString());
                else if (this.characterNumber == 2)
                    HUDManager.player2Gem = Level.text_Editor.updateWord(HUDManager.player2Gem, currentGem.gemName.ToString());
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

                if (this.characterNumber == 1)
                    HUDManager.player1Gem = Level.text_Editor.updateWord(HUDManager.player1Gem, currentGem.gemName.ToString());
                else if (this.characterNumber == 2)
                    HUDManager.player2Gem = Level.text_Editor.updateWord(HUDManager.player2Gem, currentGem.gemName.ToString());
            }

            if (currentGem != null && currentGem.Power != Gem.AbilityPower.SPEED)
                Speed = base_Speed;
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

            if (!grounded && time_Passed >= time_Per_Animation_Jump)
            {
                if (current_Sprite_Count < 11)
                    current_Sprite_Count = 11;

                else if (current_Sprite_Count < 16)
                    current_Sprite_Count += 1;

                time_Passed = 0;
                sprite_Rect.X = current_Sprite_Count * character_Width;
                
            }

            //if we press A or D then we should move the character appropriately 
            if (myHandler.Current_Keyboard_State().IsKeyDown(Keys.A) || myHandler.Current_GamePad_State().ThumbSticks.Left.X < 0)
            {
                moveLeft();
                if (weaponEffect == Effect.SPEED && power >= currentGem.Cost)
                    changePower(-CurrentGem.Cost);

                return;

            }
            else if (myHandler.Current_Keyboard_State().IsKeyDown(Keys.D) || myHandler.Current_GamePad_State().ThumbSticks.Left.X > 0)
            {
                moveRight();
                if (weaponEffect == Effect.SPEED && power >= currentGem.Cost)
                    changePower(-CurrentGem.Cost);

                return;

            }

            if (grounded && facing == direction.left)
            {
                sprite_Rect.X = 0;
                sprite_Rect.Y = 0;
                current_Sprite_Count = 0;
            }
            else if (grounded && facing == direction.right)
            {
                sprite_Rect.X = 0;
                sprite_Rect.Y = character_Texture.Height/2;
                current_Sprite_Count = 0;
            }

            
        }

        //the playable character needs their own movement methods to accomodate for the sprite sheet
        
        //we will apply damage to players differently 
        //we need to adjust the health bar when a playable character is hit
        public void takeDamage(float damage)
        {
            health = health - damage;
            Level.HUD.shortenHealthBar(health, this.characterNumber);

            if (!effect_Active)
                color_Tint = Color.OrangeRed;
        }


        public void addHealth(float added)
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

            

            if (myHandler.Current_GamePad_State().ThumbSticks.Right.X < 0)
            {
                facing = direction.left;
                handsSpriteRect.X = handsTexture.Width / 2;

            }
            else if (myHandler.Current_GamePad_State().ThumbSticks.Right.X > 0)
            {
                facing = direction.right;
                handsSpriteRect.X = 0;
            }

            if (facing == direction.left)
            {
                handsSpriteRect.X = handsTexture.Width / 2;
                handsRect.X = (int)position.X + 50;
                equipped_Weapon.textureXOffset = 25;
            }
            else
            {
                handsSpriteRect.X = 0;
                handsRect.X = (int)position.X + 25;
                equipped_Weapon.textureXOffset = 50;
            }

            handsRect.Y = (int)position.Y + 125;

            equipped_Weapon.texture_Offset = 125;
            
            equipped_Weapon.changePosition(position);

            if ((myHandler.Current_GamePad_State().IsButtonDown(Buttons.RightTrigger) && equipped_Weapon.fireType == Weapon.FireType.Auto)
                || myHandler.buttonPressed(Buttons.RightTrigger) && equipped_Weapon.fireType == Weapon.FireType.SingleShot)
            {
                fireWeapon();
            }

            if (weaponEffect == Effect.SPEED && power < 0.5f)
                Speed = base_Speed;
            else if (weaponEffect == Effect.SPEED && power > 0.5f)
                currentGem.ApplyAbility(this);

            equipped_Weapon.Update(gameTime, myHandler);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 handsOrigin;
            float handsAngle = equipped_Weapon.theta;

            if (facing == direction.left)
            {
                handsOrigin = new Vector2(handsRect.Width, 0);
                handsRect.X += handsRect.Width - 10;

                if (handsAngle < 0)
                    handsAngle = handsAngle + MathHelper.Pi;
                else if (handsAngle > 0)
                    handsAngle = handsAngle - MathHelper.Pi;
            }
            else
            {
                handsOrigin = Vector2.Zero;
            }
            

            spriteBatch.Draw(character_Texture, character_Rect, sprite_Rect, Color.White, 0.0f, origin, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(identifyArrow, arrowRect, Color.White);
            equipped_Weapon.Draw(spriteBatch);
            spriteBatch.Draw(handsTexture, handsRect, handsSpriteRect, Color.White, handsAngle/4, handsOrigin, SpriteEffects.None, 1.0f);
        }

    }
}
