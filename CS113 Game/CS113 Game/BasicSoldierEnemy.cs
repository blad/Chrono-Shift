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
    public class BasicSoldierEnemy : Enemy
    {
        public BasicSoldierEnemy(Game1 game, Spawner spawner, Vector2 position) :
            base(game, spawner)
        {
            this.position = position;

            base_Speed = 2;
            Speed = 2;
            health = 30;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/TestCharacter");
            spriteRectOffset = character_Texture.Height / 2;

            facing = direction.left;

            character_Width = 50;
            character_Height = 75;
            sprite_Count = 0;
            current_Sprite_Count = 0;
            time_Per_Animation = 250; //every 250 ms we change animation
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Width*2, character_Height*2);
            texture_Offset = character_Height*2;

            equipped_Weapon = new AssaultRifle(game, this, true, this.position);
            has_Weapon = true;
        }

        //this is the basic soldier attack
        //the soldier will fire 3 shots
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
        }

    }
}
