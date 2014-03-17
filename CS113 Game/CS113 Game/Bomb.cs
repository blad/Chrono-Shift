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
    public class Bomb : Enemy
    {
        private int Damage = 15;

        public Bomb(Game1 game, Vector2 position)
            : base(game)
        {
            this.position = position;

            base_Speed = 0;
            Speed = 0;
            health = 10;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Projectiles/NaziBomb");

            facing = direction.left;

            character_Width = 40;
            character_Height = 50;
            sprite_Count = 0;
            current_Sprite_Count = 0;
            time_Per_Animation = 250; //every 250 ms we change animation
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width, character_Height);
            texture_Offset = character_Height;

            has_Weapon = false;
            has_Gravity = true;
        }

        public override void Attack()
        {
            //throw new NotImplementedException();
        }

        public void Attack(PlayableCharacter c)
        {
            health = 0;
            c.takeDamage(Damage);
            Damage = 0;// this to make sure we don't get hit more than once because of delays
        }

        public override void AIroutine(GameTime gameTime)
        {
            foreach (MainCharacter c in Level.playerList)
                if (character_Rect.Intersects(c.getCharacterRect()))
                {
                    Attack(c);
                }

            if (character_Rect.Intersects(Level.ground_Rect))
                health = 0;
        }
    }
}
