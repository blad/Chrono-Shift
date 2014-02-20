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

        public int texture_Offset = 60;

        Game1 gameRef;

        public Gun(Game1 game)
            : base(game)
        {

            gameRef = game;
        }

        //when we fire we need the location of the mouse pointer to find the angle at whcih we shoot
        //and an offset so that the point is relative to the position of the mouse in the game space and not the screen
        //our shooting will be limited to the 
        public override void fire(Point mousePoint, int offset)
        {
            time_Passed += current_Time.ElapsedGameTime.Milliseconds;

            if (time_Passed - current_Time.ElapsedGameTime.Milliseconds >= attack_Speed || fireType == FireType.SingleShot)
            {
                time_Passed = 0;

                weapon_Sound.Play();
                //find the correct X and Y components based on the angle at which we are aiming
                float ySpeed = bullet_Speed * (float)Math.Sin(theta);
                float xSpeed = bullet_Speed * (float)Math.Cos(theta);

                bool invert = false;

                //the 45 here may change
                if (mousePoint.X + offset < position.X + 45)
                {
                    xSpeed = -xSpeed;
                    theta = -theta;
                    invert = true;
                }

                //create a new bullet with this weapon passed as its parent weapon
                Bullet bullet = new Bullet(gameRef, this, startPosition, new Vector2(xSpeed, ySpeed), theta, invert, bulletsFired);

                bullet_List[bulletsFired] = bullet;
                bulletsFired++;

                if (bulletsFired >= max_Bullets_To_Draw)
                {
                    bulletsFired = 0;
                }
            }
        }


        //when we fire the weapon, we need a point plus the screen offset to know where we clicked in the gamespace
        public override void Update(GameTime gameTime, InputHandler handler)
        {
            current_Time = gameTime;

            startPosition = position + (new Vector2((float)weapon_Texture.Width - 15, texture_Offset));

            //find where the mouse is based on the gamespace and find the angle at which we are aiming
            Point mousePoint = handler.mousePosition();

            int distanceX = mousePoint.X + Level.screen_Offset - (int)startPosition.X;
            int distanceY = mousePoint.Y - (int)startPosition.Y;

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
            spriteBatch.Draw(weapon_Texture, weapon_Rect, null, Color.White, theta, Vector2.Zero, SpriteEffects.None, 1.0f);

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
