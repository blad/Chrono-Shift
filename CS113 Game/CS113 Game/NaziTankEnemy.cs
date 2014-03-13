﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CS113_Game
{
    public class NaziTankEnemy : Enemy
    {

        public NaziTankEnemy(Game1 game, Spawner spawner, Vector2 position)
            : base(game, spawner)
        {
            this.position = position;
            gameRef = game;

            base_Speed = 1;
            Speed = 1;
            health = 450;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/nazi_tank");

            facing = direction.left;

            character_Width = 619;
            character_Height = 279;
            sprite_Count = 1;
            current_Sprite_Count = 0;
            time_Per_Animation = 100;
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width, character_Height);
            texture_Offset = character_Height;

            attack_Time = 500;
            has_Weapon = false;
            has_Gravity = false;
        }

        //the plane attacks enemies by dropping bombs on them
        public override void Attack()
        {

        }

        //Enemy plane will move a certain distance past the player before it changes direction
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