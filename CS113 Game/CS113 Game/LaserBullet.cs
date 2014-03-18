using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public class LaserBullet : Bullet
    {
        Enemy lastEnemyHit;
        PlayableCharacter lastPlayerHit;

        public LaserBullet(Game game, Gun source_Weapon, bool target, Vector2 position,
                        Vector2 direction, float theta, bool inversion,
                        int list_Position, Character.Effect effect)
            : base(game, source_Weapon, target, position, direction, theta, inversion, list_Position, effect)
        {
            this.bullet_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Projectiles/SmallLaser");
            this.image_Location = 20;

            if (inverted)
            {
                origin = new Vector2(image_Location, 0.0f);
                this.sprite_Rect = new Rectangle(image_Location, 0, bullet_Texture.Width / 2, bullet_Texture.Height);
            }
            else
            {
                origin = Vector2.Zero;
                this.sprite_Rect = new Rectangle(0, 0, bullet_Texture.Width / 2, bullet_Texture.Height);
            }

            this.bullet_Rect = new Rectangle((int)position.X, (int)position.Y, bullet_Texture.Width / 2, bullet_Texture.Height);
            this.damage = 10;
        }

        public override void onCollisionEffect(PlayableCharacter c)
        {
            if (lastEnemyHit == null)
            {
                c.takeDamage(damage);
                c.applyEffectDamage(bullet_Effect);
                lastPlayerHit = c;
            }
            else
            {
                if (!c.Equals(lastEnemyHit))
                {
                    c.takeDamage(damage);
                    c.applyEffectDamage(bullet_Effect);
                    lastPlayerHit = c;
                }
            }
        }

        public override void onCollisionEffect(Enemy enemy)
        {
            if (lastEnemyHit == null)
            {
                enemy.takeDamage(damage);
                enemy.applyEffectDamage(bullet_Effect);
                lastEnemyHit = enemy;
            }
            else
            {
                if (!enemy.Equals(lastEnemyHit))
                {
                    enemy.takeDamage(damage);
                    enemy.applyEffectDamage(bullet_Effect);
                    lastEnemyHit = enemy;
                }
            }
        }
    }
}
