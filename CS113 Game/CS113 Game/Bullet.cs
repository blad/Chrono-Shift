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
        private Gun source_Weapon;
        private float angle;
        private int damage = 5;
        private int list_Position;

        public Vector2 position;
        public bool inverted;

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }


        public Bullet(Game game, Gun source_Weapon, Vector2 position, Vector2 direction, float theta, bool inversion, int list_Position)
            : base(game)
        {
            bullet_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Projectiles/SimpleBullet");

            this.source_Weapon = source_Weapon;
            this.position = position;
            this.list_Position = list_Position;
            this.direction = direction;

            angle = theta;

            inverted = inversion;

            //must fix the hard numbers in this code, change to variables
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


        //fill this out later
        //this method should be different per type of bullet
        //different bullets have different effects:
        //pistol bullets - regular hit effect/animation
        //lasers - penetrate enemies
        //rockets - create explosion and splash damage
        //etc.
        public void onCollisionEffect()
        {
        }

        public override void Update(GameTime gameTime)
        {
            position += direction;
            bullet_Rect.X = (int)position.X;
            bullet_Rect.Y = (int)position.Y;

            foreach (Enemy enemy in Level.enemyList)
            {
                if (enemy != null)
                {
                    if (bullet_Rect.Intersects(enemy.getCharacterRect()))
                    {
                        enemy.takeDamage(damage);
                        source_Weapon.getBullets()[list_Position] = null;
                    }
                }     
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bullet_Texture, bullet_Rect, sprite_Rect, Color.White, angle, origin , SpriteEffects.None, 1.0f);
        }
    }
}
