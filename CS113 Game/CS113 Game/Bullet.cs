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
    public class Bullet : DrawableGameComponent
    {
        private Texture2D bullet_Texture;

        private Rectangle bullet_Rect;
        private Rectangle sprite_Rect;
        private Vector2 origin;
        private Vector2 direction;
        private AssaultRifle originWeapon;
        private float angle;
        private int damage = 10;

        public Vector2 position;
        public bool inverted;
       


        public Bullet(Game game, AssaultRifle thisWeapon, Vector2 startingPosition, Vector2 direction, float theta, bool inversion)
            : base(game)
        {
            bullet_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Projectiles/SimpleBullet");
            originWeapon = thisWeapon;
            position = startingPosition;
            this.direction = direction;

            angle = theta;

            inverted = inversion;

            if (inverted)
            {
                origin = new Vector2(10.0f, 0.0f);
                sprite_Rect = new Rectangle(10, 0, bullet_Texture.Width/2, bullet_Texture.Height);
            }
            else
            {
                origin = Vector2.Zero;
                sprite_Rect = new Rectangle(0, 0, bullet_Texture.Width / 2, bullet_Texture.Height);
            }
                

            bullet_Rect = new Rectangle((int)position.X, (int)position.Y, bullet_Texture.Width/2, bullet_Texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            position += direction;
            bullet_Rect.X = (int)position.X;
            bullet_Rect.Y = (int)position.Y;

            foreach (Enemy enemy in Level.enemyList)
            {
                if (bullet_Rect.Intersects(enemy.getCharacterRect()))
                {
                    enemy.health = enemy.health - damage;
                    //originWeapon.getBullets().Remove(this);
                }
                        
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bullet_Texture, bullet_Rect, sprite_Rect, Color.White, angle, origin , SpriteEffects.None, 1.0f);
        }
    }
}
