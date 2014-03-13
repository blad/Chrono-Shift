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
    public class SegwayEnemy : Enemy
    {
        public SegwayEnemy(Game1 game, Spawner spawner, Vector2 position)
            : base(game, spawner)
        {
            this.position = position;

            base_Speed = 6;
            Speed = 6;
            health = 25;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/segway_enemy_sprite");

            facing = direction.left;

            character_Width = 190;
            character_Height = 300;
            sprite_Count = 2;
            current_Sprite_Count = 0;
            time_Per_Animation = 50; //every 90 ms we change animation
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width, character_Height);
            texture_Offset = character_Height;

            has_Weapon = false;
        }

        public override void Attack()
        {
            
        }

        public override void AIroutine(GameTime gameTime)
        {
            current_Attack_Time += current_Game_Time.ElapsedGameTime.Milliseconds;
            time_Passed += gameTime.ElapsedGameTime.Milliseconds;

            if (current_Attack_Time >= attack_Time)
            {
                current_Attack_Time = 0;
                Attack();
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
            }
        }

    }
}
