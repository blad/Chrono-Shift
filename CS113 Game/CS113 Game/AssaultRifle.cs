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
    public class AssaultRifle : Weapon
    {
        private Texture2D weapon_Texture;

        private ArrayList bullet_List;
        private int bullet_Speed = 20;
        private int bulletsFired = 0;
        public int texture_Offset = 30;

        Game1 gameRef;

        public AssaultRifle(Game1 game, Vector2 position)
            : base(game)
        {
            weapon_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Weapons/AssaultRifle");
            weapon_Rect = new Rectangle((int)position.X, (int)position.Y, weapon_Texture.Width, weapon_Texture.Height);

            gameRef = game;
            DrawOrder = 500;
            bullet_List = new ArrayList(20);
            current_Ammo_Count = max_Ammo_Count = 200;
        }

        public ArrayList getBullets()
        {
            return bullet_List;
        }

        public override void fire(Point mousePoint, int offset)
        {
            Vector2 startPosition = position + (new Vector2(10.0f, 20.0f));

            int distanceX = mousePoint.X + offset - (int)startPosition.X;
            int distanceY = mousePoint.Y - (int)startPosition.Y;

            float hypotnuse = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            float theta = (float)Math.Asin(distanceY/hypotnuse);

            float ySpeed = bullet_Speed * (float)Math.Sin(theta);
            float xSpeed = bullet_Speed * (float)Math.Cos(theta);

            bool invert = false;

            if (mousePoint.X + offset < position.X)
            {
               xSpeed = -xSpeed;
               theta = -theta;
               invert = true;
            }

            Bullet bullet = new Bullet(gameRef, this, startPosition, new Vector2(xSpeed, ySpeed), theta, invert);

            bullet_List.Insert(bulletsFired, bullet);
            bulletsFired++;

            if (bulletsFired >= 20)
                bulletsFired = 0;
        }

        public override void Update(GameTime gameTime)
        {
            weapon_Rect.X = (int)position.X;
            weapon_Rect.Y = (int)position.Y + texture_Offset;
            
            foreach (Bullet b in bullet_List)
            {
                b.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(weapon_Texture, weapon_Rect, Color.White);

            foreach (Bullet b in bullet_List)
            {   
                if (b != null)
                    b.Draw(spriteBatch);
            }
        }
    }
}
