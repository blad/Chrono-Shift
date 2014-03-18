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
    public class BobbaBubble : Enemy
    {
        private int Damage = 15;

        public BobbaBubble(Game1 game, Vector2 position)
            : base(game)
        {
            this.position = position;

            base_Speed = 6;
            Speed = 6;
            health = 10;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Projectiles/boba_bubble_spritesheet");

            facing = direction.left;

            character_Width = 236;
            character_Height = 136;
            sprite_Count = 7;
            current_Sprite_Count = 0;
            time_Per_Animation = 50; //every 250 ms we change animation
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width, character_Height);
            texture_Offset = character_Height;

            has_Weapon = false;
            has_Gravity = false;
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
            current_Attack_Time += current_Game_Time.ElapsedGameTime.Milliseconds;
            time_Passed += gameTime.ElapsedGameTime.Milliseconds;

            foreach (MainCharacter c in Level.playerList)
                if (character_Rect.Intersects(c.getCharacterRect()))
                {
                    Attack(c);
                }

            if (!attacking)
            {
                if (character_To_Attack.position.X > position.X)
                {
                    moveRight();
                }
                else if (character_To_Attack.position.X < position.X)
                {
                    moveLeft();
                }


                if (character_To_Attack.position.Y > position.Y)
                {
                    position.Y += 5;
                }
                else if (character_To_Attack.position.Y < position.Y)
                {
                    position.Y -= 5;
                }
            }
        }
    }
}
