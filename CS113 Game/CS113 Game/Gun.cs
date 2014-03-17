using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace CS113_Game
{
    public class Gun : Weapon
    {

        protected Texture2D weapon_Texture;
        protected Bullet[] bullet_List;
        protected Vector2 startPosition;
        protected GameTime current_Time;
        protected int bullet_Speed;
        protected int max_Bullets_To_Draw = 75;
        protected int bulletsFired = 0;
        protected float theta;
        protected float volume;

        protected enum BulletType { RIFLE, PISTOL, LASER }
        protected BulletType bulletType;

        Game1 gameRef;

        public Gun(Game1 game, Character character, bool target, Vector2 position)
            : base(game, character, target)
        {
            this.position = position;
            theta = 0.0f;
            bullet_List = new Bullet[max_Bullets_To_Draw];
            startPosition = Vector2.Zero;

            gameRef = game;

            current_Time = Game1.currentGameTime;
        }

        //when we fire we need the location of the mouse pointer to find the angle at whcih we shoot
        //and an offset so that the point is relative to the position of the mouse in the game space and not the screen
        //this is the fire method that players use
        public override void fire(InputHandler handler)
        {
            time_Passed += current_Time.ElapsedGameTime.Milliseconds;

            if (time_Passed - current_Time.ElapsedGameTime.Milliseconds >= attack_Speed || fireType == FireType.SingleShot)
            {
                time_Passed = 0;

                SoundEffectInstance instance = weapon_Sound.CreateInstance();
                instance.Volume = volume;
                instance.Play();
                //find the correct X and Y components based on the angle at which we are aiming
                float ySpeed = bullet_Speed * (float)Math.Sin(theta);
                float xSpeed = bullet_Speed * (float)Math.Cos(theta);

                bool invert = false;


                //the 45 here may change
                if (source_Character.facing == Character.direction.left && theta == 0)
                    xSpeed = -xSpeed;

                

                //create a new bullet with this weapon passed as its parent weapon
                Bullet bullet;


                switch (bulletType)
                {
                    case (BulletType.RIFLE):
                        bullet = new AssaultBullet(gameRef, this, player_Target, startPosition,
                                            new Vector2(xSpeed, ySpeed),
                                            theta, invert, bulletsFired, source_Character.weaponEffect);
                        break;

                    case (BulletType.LASER):
                        bullet = new LaserBullet(gameRef, this, player_Target, startPosition,
                                             new Vector2(xSpeed, ySpeed),
                                             theta, invert, bulletsFired, source_Character.weaponEffect);
                        break;

                    default:
                        bullet = new AssaultBullet(gameRef, this, player_Target, startPosition,
                                            new Vector2(xSpeed, ySpeed),
                                            theta, invert, bulletsFired, source_Character.weaponEffect);
                        break;

                }

                bullet_List[bulletsFired] = bullet;
                bulletsFired++;

                if (bulletsFired >= max_Bullets_To_Draw)
                {
                    bulletsFired = 0;
                }

                if (source_Character.weaponEffect != Character.Effect.NORMAL)
                {
                    source_Character.changePower(-source_Character.CurrentGem.Cost);
                }

                if (source_Character.characterNumber == 1)
                    HUDManager.AmmoCountOne = Level.text_Editor.updateWord(HUDManager.AmmoCountOne, (--ammo).ToString());
                else if (source_Character.characterNumber == 2)
                    HUDManager.AmmoCountTwo = Level.text_Editor.updateWord(HUDManager.AmmoCountTwo, (--ammo).ToString());
            }
        }

        //this is the fire method for enemies
        public override void fire(Character characterToAttack)
        {
            time_Passed += current_Time.ElapsedGameTime.Milliseconds;

            if (time_Passed - current_Time.ElapsedGameTime.Milliseconds >= attack_Speed || fireType == FireType.SingleShot)
            {
                time_Passed = 0;

                SoundEffectInstance instance = weapon_Sound.CreateInstance();
                instance.Volume = volume;
                instance.Play();
                //find the correct X and Y components based on the angle at which we are aiming
                float ySpeed = bullet_Speed * (float)Math.Sin(theta);
                float xSpeed = bullet_Speed * (float)Math.Cos(theta);

                bool invert = false;

                //the 45 here may change
                if (characterToAttack.position.X < position.X + 45)
                {
                    xSpeed = -xSpeed;
                    theta = -theta;
                    invert = true;
                }

                //create a new bullet with this weapon passed as its parent weapon
                Bullet bullet;


                //create a new bullet with this weapon passed as its parent weapon
                switch (bulletType)
                {
                    case (BulletType.RIFLE):
                        bullet = new AssaultBullet(gameRef, this, player_Target, startPosition,
                                            new Vector2(xSpeed, ySpeed),
                                            theta, invert, bulletsFired, source_Character.weaponEffect);
                        break;

                    case (BulletType.LASER):
                        bullet = new LaserBullet(gameRef, this, player_Target, startPosition,
                                             new Vector2(xSpeed, ySpeed),
                                             theta, invert, bulletsFired, source_Character.weaponEffect);
                        break;

                    default:
                        bullet = new AssaultBullet(gameRef, this, player_Target, startPosition,
                                            new Vector2(xSpeed, ySpeed),
                                            theta, invert, bulletsFired, source_Character.weaponEffect);
                        break;

                }

                bullet_List[bulletsFired] = bullet;
                bulletsFired++;

                if (bulletsFired >= max_Bullets_To_Draw)
                {
                    bulletsFired = 0;
                }

                ammo--;
            }
        }


        //when we fire the weapon, we need a point plus the screen offset to know where we clicked in the gamespace
        //this update will be used only for playable characters
        public override void Update(GameTime gameTime, InputHandler handler)
        {
            current_Time = gameTime;

            startPosition = position;// +(new Vector2((float)weapon_Texture.Width / 2 - 15, texture_Offset));
            startPosition.Y += texture_Offset;            

            //find where the mouse is based on the gamespace and find the angle at which we are aiming

            //this changest the characters direction based on where the player is aiming
            
            theta = handler.getRightThumbStickAngle();

            

            weapon_Rect.X = (int)position.X;
            weapon_Rect.Y = (int)position.Y + texture_Offset;

            foreach (Bullet b in bullet_List)
            {
                if (b != null)
                    b.Update(gameTime);
            }

            if (ammo <= 0)
            {
                source_Character.switchWeapon(new Pistol(gameRef, source_Character, false, source_Character.position));
            }
        }


        public override void Update(GameTime gameTime)
        {
            current_Time = gameTime;

            startPosition = position;// +(new Vector2((float)weapon_Texture.Width / 2 - 15, texture_Offset));
            startPosition.Y += texture_Offset;

            //we will point at the character, not the position of the mouse
            int distanceX = (int)Level.player1.getCharacterRect().Center.X - (int)startPosition.X;
            int distanceY = (int)Level.player1.getCharacterRect().Center.Y - (int)startPosition.Y;

            float hypotnuse = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            theta = (float)Math.Asin(distanceY / hypotnuse);

            weapon_Rect.X = (int)position.X;
            weapon_Rect.Y = (int)position.Y + texture_Offset;

            foreach (Bullet b in bullet_List)
            {
                if (b != null)
                    b.Update(gameTime);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            float weaponAngle = theta;

            if (source_Character.facing == Character.direction.left)
            {
                if (!source_Character.isPlayer)
                    weaponAngle = -weaponAngle;
                else
                    if (weaponAngle != 0)
                        weaponAngle =  weaponAngle - MathHelper.Pi;

                sprite_Rect.X = left_Sprite_Position;
            }
            else if (source_Character.facing == Character.direction.right)
            {
                sprite_Rect.X = 0;
            }

            weapon_Rect.X += weapon_Texture.Width/4;

            spriteBatch.Draw(weapon_Texture, weapon_Rect, sprite_Rect, Color.White, weaponAngle, 
                                new Vector2(sprite_Rect.Width/2, sprite_Rect.Height/2), SpriteEffects.None, 1.0f);

            foreach (Bullet b in bullet_List)
            {
                if (b != null)
                    b.Draw(spriteBatch);
            }
        }

        //return the list of bullets that we have fired
        public Bullet[] getBullets()
        {
            return bullet_List;
        }
    }
}
