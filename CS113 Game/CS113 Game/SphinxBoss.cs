﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public class SphinxBoss : Enemy
    {
        public SphinxBoss(Game1 game, Spawner spawner, Vector2 position)
            : base(game, spawner)
        {
            this.position = position;

            base_Speed = 1;
            Speed = 1;
            health = 100;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/sphinx-sprite");
            spriteRectOffset = character_Texture.Height / 2;

            facing = direction.left;

            character_Width = 465;
            character_Height = 300;
            sprite_Count = 3;
            current_Sprite_Count = 0;
            time_Per_Animation = 100; //every 90 ms we change animation
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width * 2 , character_Height * 2);
            texture_Offset = character_Height * 2;

            has_Weapon = false;
        }


        public override void Attack()
        {
            //boss will do nothing for now
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

            if (health <= 0)
            {
                bool AddGem = true;

                foreach (Gem gem in LevelSelectScreen.characterGems)
                {
                    if (gem.Power == Gem.AbilityPower.SPEED)
                        AddGem = false;
                }

                if (AddGem)
                    LevelSelectScreen.characterGems.AddLast(new SpeedGem());

                Game1.popScreenStack();
            }
        }

    }
}
