﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace CS113_Game
{
    public class RobamaBoss : Enemy
    {
        public RobamaBoss(Game1 game, Spawner spawner, Vector2 position)
            : base(game, spawner)
        {
            this.position = position;

            base_Speed = 2;
            Speed = 2;
            health = 1000;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/robama_sprite");
            spriteRectOffset = character_Texture.Height / 2;

            facing = direction.left;

            character_Width = 235;
            character_Height = 600;
            sprite_Count = 5;
            current_Sprite_Count = 0;
            time_Per_Animation = 100; //every 90 ms we change animation
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width, character_Height);
            texture_Offset = character_Height;

            has_Weapon = true;

            EmptyGun gun = new EmptyGun(gameRef, this, true, this.position);
            gun.bulletType = Gun.BulletType.BOSSLASER;
            attack_Time = 4000;
            equipped_Weapon = gun;
        }


        public override void Attack()
        {
            equipped_Weapon.fire(character_To_Attack);
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
                    if (gem.Power == Gem.AbilityPower.ICE)
                        AddGem = false;
                }

                if (AddGem)
                    LevelSelectScreen.characterGems.AddLast(new FreezeGem());

                Game1.popScreenStack();
            }
        }


    }
}
