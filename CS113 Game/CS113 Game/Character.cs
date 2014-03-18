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
        protected GameTime current_Game_Time;
        protected GameTime previous_Game_Time;
        protected Vector2 previousPosition;
        protected Rectangle character_Rect;
        protected Rectangle sprite_Rect;
        protected Texture2D character_Texture;
        protected Color color_Tint = Color.White;
        protected Weapon equipped_Weapon;
        protected Gem currentGem;

        protected int base_Speed;
        protected int movement_Speed;
        protected int gravity = 1;
        protected int base_Jump_Speed = 20;
        protected int jump_Speed = 0;
        protected int texture_Offset;
        protected int spriteRectOffset;
        protected float power = 100;

        protected bool has_Weapon = false;
        protected bool effect_Active = false;
        protected bool takingDOT = false;
        protected int dotTime = 0;

        //the direction the sprite is facing
        public enum direction { left, right };
        public direction facing;

        //the size of the character that we need to accomdate for the sprite sheet
        protected Vector2 origin;
        protected int standing_X; //the position in the sprite sheet where we are standing and facing to the left
        protected int character_Width;
        protected int character_Height;
        protected int sprite_Count;
        protected int current_Sprite_Count;
        protected int time_Per_Animation; //every 90 ms we change animation
        protected int time_Passed;
        

        public Vector2 position;
        public Rectangle standing_Platform;
        public float health;
        public int characterNumber = 0;
        public bool grounded = false;
        public bool jumping = false;
        public bool falling = false;
        public bool isPlayer = false;

        //these will be the different kinds of effects that can be applied to characters
        public enum Effect { NORMAL, DAMAGE, ARMOR, FIRE, ICE, SPEED }
        public Effect currentEffect = Effect.NORMAL;
        public Effect weaponEffect = Effect.NORMAL;
        public int currentFireTime = 0; 
        public int timeToLive_FIRE = 5000; //fire effects will last for 5 seconds
        public int fireTick = 500; //take damage every half second 
        public int fireDamage = 3;
        public int armorModifier = 1;
        public int damageModifier = 1;

        protected Game1 gameRef;

        public int BaseSpeed
        {
            get { return base_Speed; }
        }

        public int Speed
        {
            get { return movement_Speed; }
            set { movement_Speed = value; }
        }

        public Weapon Weapon
        {
            get { return equipped_Weapon; }
        }

        public float Power
        {
            get { return power; }
            set { power = value; }
        }

        public void changePower(float additional)
        {
            power += additional;
            Level.HUD.shortenPowerBar(power, this.characterNumber);
        }

        public Gem CurrentGem
        {
            get { return currentGem; }
        }



        public Character(Game1 game)
            : base(game)
        {
            
        }

        public void switchWeapon(Weapon weapon)
        {
            equipped_Weapon = weapon;
            
            if (characterNumber == 1)
                HUDManager.AmmoCountOne = Level.text_Editor.updateWord(HUDManager.AmmoCountOne, weapon.Ammo.ToString());
            else if (characterNumber == 2)
                HUDManager.AmmoCountTwo = Level.text_Editor.updateWord(HUDManager.AmmoCountTwo, weapon.Ammo.ToString());
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
        public Rectangle moveLeft()
        {
            position.X = position.X - movement_Speed;
            facing = direction.left;

            sprite_Rect.Y = 0;
            sprite_Rect.X = character_Width * current_Sprite_Count;

            //if enough time has passed, move on to the next animation
            if (time_Passed > time_Per_Animation && grounded)
            {
                current_Sprite_Count++;
                time_Passed = 0;
            }

            if (current_Sprite_Count > sprite_Count && grounded)
                current_Sprite_Count = 0;

            if (position.X < 0)
                position.X = 0;

            return sprite_Rect;
        }

        public Rectangle moveRight()
        {
            position.X = position.X + movement_Speed;
            facing = direction.right;

            sprite_Rect.Y = spriteRectOffset;
            sprite_Rect.X = character_Width * current_Sprite_Count;

            if (time_Passed > time_Per_Animation && grounded)
            {
                current_Sprite_Count++;
                time_Passed = 0;
            }

            if (current_Sprite_Count > sprite_Count && grounded)
                current_Sprite_Count = 0;

            return sprite_Rect;
        }

        //characters will take damage and turn orange for a frame
        public void takeDamage(int damage)
        {
            health = health - damage;

            if (!effect_Active)
                color_Tint = Color.OrangeRed;
        }


        public void takeFireDOT(GameTime gameTime)
        {
            currentFireTime += gameTime.ElapsedGameTime.Milliseconds;

            if (currentFireTime % fireTick <= 10)
            {
                health -= fireDamage;

                if (currentFireTime >= timeToLive_FIRE)
                {
                    currentEffect = Effect.NORMAL;
                    currentFireTime = 0;
                    effect_Active = false;
                    takingDOT = false;
                    color_Tint = Color.White;
                }
            }
        }

        //when a an effect hits the player, we must change variables to meet this effect
        public void applyEffectDamage(Effect effect)
        {
            
            //depending on what effect the character was hit with, change our effect status and color shader
            switch (effect)
            {
                //getting a fire effect will cause the character to take fire damage over time
                case Effect.FIRE :
                    currentEffect = Effect.FIRE;
                    color_Tint = Color.Red;
                    effect_Active = true;
                    takingDOT = true;
                    currentFireTime = 0; //reset the time to take damage
                    break;

                //getting an ice effect will slow the enemy
                case Effect.ICE :
                    currentEffect = Effect.ICE;
                    color_Tint = Color.DarkCyan;
                    effect_Active = true;
                    movement_Speed = 1;
                    break;
            }
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
