using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public abstract class Bullet : DrawableGameComponent
    {
        protected Texture2D bullet_Texture;

        protected Rectangle bullet_Rect;
        protected Rectangle sprite_Rect;
        protected Vector2 origin;
        protected Vector2 direction;
        protected Gun source_Weapon;
        protected Color bullet_Color;
        protected Character.Effect bullet_Effect;

        protected int image_Location;

        //we need to know if this bullet was fired by an enemy
        //if it was then the bullet needs to do damage to the player and not other enemies
        protected bool player_Target;


        protected float angle;
        protected int damage;
        protected int list_Position;


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


        public Bullet(Game game, Gun source_Weapon, bool target, Vector2 position,
                        Vector2 direction, float theta, bool inversion,
                        int list_Position, Character.Effect effect)
            : base(game)
        {

            this.source_Weapon = source_Weapon;
            this.position = position;
            this.list_Position = list_Position;
            this.direction = direction;

            player_Target = target;

            angle = theta;

            inverted = inversion;

            bullet_Effect = effect;

            DrawOrder = 350;

            //depending on what the fired effect was, we will adjust the properties of the bullet
            switch (bullet_Effect)
            {
                case (Character.Effect.NORMAL):
                    bullet_Color = Color.White;
                    break;

                case (Character.Effect.SPEED):
                    bullet_Color = Color.White;
                    break;

                case (Character.Effect.FIRE):
                    bullet_Color = Color.Red;
                    break;

                case (Character.Effect.ICE):
                    bullet_Color = Color.DarkCyan;
                    break;

            }

        }


        //fill this out later
        //this method should be different per type of bullet
        //different bullets have different effects:
        //pistol bullets - regular hit effect/animation
        //lasers - penetrate enemies
        //rockets - create explosion and splash damage
        //etc.
       

        //this will need to be adjusted based on whether or not the bullet hits and enemy or character
        public override void Update(GameTime gameTime)
        {
            position += direction;
            bullet_Rect.X = (int)position.X;
            bullet_Rect.Y = (int)position.Y;

            if (!player_Target)
            {
                foreach (Enemy enemy in Level.enemyList)
                {
                    if (enemy != null)
                    {
                        if (bullet_Rect.Intersects(enemy.getCharacterRect()) && bullet_Rect.Center.X > enemy.getCharacterRect().Center.X - 50 
                                                                           && bullet_Rect.Center.X < enemy.getCharacterRect().Center.X + 50 )
                        {
                            onCollisionEffect(enemy);
                        }
                    }
                }
            }
            else
            {
                foreach (MainCharacter c in Level.playerList)
                {
                    if (bullet_Rect.Intersects(c.getCharacterRect()))
                    {
                        c.takeDamage(damage);
                        source_Weapon.getBullets()[list_Position] = null;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bullet_Texture, bullet_Rect, sprite_Rect, bullet_Color, angle, origin, SpriteEffects.None, 1.0f);
        }

        public abstract void onCollisionEffect(Enemy enemy);
          
    }
}
