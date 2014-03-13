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
    public class NaziPlaneEnemy : Enemy
    {

        public NaziPlaneEnemy(Game1 game, Spawner spawner, Vector2 position)
            : base(game, spawner)
        {
            this.position = position;
            gameRef = game;

            base_Speed = 8;
            Speed = 8;
            health = 200;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/nazi_plane");

            facing = direction.left;

            character_Width = 437;
            character_Height = 165;
            sprite_Count = 3;
            current_Sprite_Count = 0;
            time_Per_Animation = 25;
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width/2, character_Height/2);
            texture_Offset = character_Height/2;

            attack_Time = 500;
            has_Weapon = false;
            has_Gravity = false;
        }

        //the plane attacks enemies by dropping bombs on them
        public override void Attack()
        {
            Bomb bomb = new Bomb(gameRef, new Vector2((float)character_Rect.Center.X, (float)character_Rect.Center.Y + 25));
            Level.characterList.Add(bomb);
            Level.enemyList.Add(bomb);
            Level.bombList.Add(bomb);
        }
        
        //Enemy plane will move a certain distance past the player before it changes direction
        public override void AIroutine(GameTime gameTime)
        {
            current_Attack_Time += current_Game_Time.ElapsedGameTime.Milliseconds;
            time_Passed += gameTime.ElapsedGameTime.Milliseconds;

            if ((character_Rect.Center.X < character_To_Attack.getCharacterRect().Center.X + 5
                && character_Rect.Center.X > character_To_Attack.getCharacterRect().Center.X - 5) 
                && current_Attack_Time >= attack_Time)
            {
                current_Attack_Time = 0;
                Attack();
            }


            if (!attacking)
            {
                if (character_Rect.Center.X < character_To_Attack.position.X - 300
                    || (character_Rect.Center.X < character_To_Attack.position.X + 300 && facing == direction.right))
                {
                     moveRight();
                }
                else
                {
                    moveLeft();
                }
            }
        }
    }
}
