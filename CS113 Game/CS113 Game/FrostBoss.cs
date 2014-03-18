using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public class FrostBoss : Enemy
    {
        private LinkedList<Weapon> weaponList;
        bool flying = false;
        Vector2 positionToFlyTo;

        public FrostBoss(Game1 game, Spawner spawner, Vector2 position)
            : base(game, spawner)
        {
            this.position = position;
            positionToFlyTo = position;

            base_Speed = 5;
            Speed = 5;
            health = 750;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/Prof_Frost");

            facing = direction.left;

            character_Width = character_Texture.Width;
            character_Height = character_Texture.Height;
            sprite_Count = 0;
            current_Sprite_Count = 0;
            time_Per_Animation = 100; //every 90 ms we change animation
            attack_Time = 5000;
            time_Passed = 0;

            origin = Vector2.Zero;
            sprite_Rect = new Rectangle((int)origin.X, (int)origin.Y, character_Width, character_Height);
            character_Rect = new Rectangle((int)position.X, (int)position.Y, (int)(character_Width * 0.65f) , (int)(character_Height * 0.65f));
            texture_Offset = character_Height;

            has_Gravity = false;
            has_Weapon = false;


        }


        public override void Attack()
        {
            //boss will do nothing for now
        }

        public void FlyToPoint()
        {
            Random rng1 = new Random();
            Random rng2 = new Random();

            int PointX = rng1.Next(Level.screen_Offset, Level.screen_Offset + Game1.screen_Width);
            int PointY = rng2.Next(0, (Game1.screen_Height - character_Height)  );

            positionToFlyTo = new Vector2(PointX, PointY);

            flying = true;
        }


        public override void AIroutine(GameTime gameTime)
        {
            current_Attack_Time += current_Game_Time.ElapsedGameTime.Milliseconds;
            time_Passed += gameTime.ElapsedGameTime.Milliseconds;

            if (current_Attack_Time >= attack_Time)
            {
                current_Attack_Time = 0;
                FlyToPoint();
                Attack();
            }

            if (flying)
            {
                if (position != positionToFlyTo)
                {

                    if (position.X < positionToFlyTo.X - movement_Speed)
                        position.X += movement_Speed;
                    else if (position.X > positionToFlyTo.X + movement_Speed)
                        position.X -= movement_Speed;

                    if (position.Y < positionToFlyTo.Y - movement_Speed)
                        position.Y += movement_Speed;
                    else if (position.Y > position.Y + movement_Speed)
                        position.Y -= movement_Speed;
                }
                else
                    flying = false;
            }

            //if this boss dies we will add the fire gem to list of all the gems available to the character
            //we will also pop the screen stack and go back to the level select screen
            if (health <= 0)
            {
                bool AddGem = true;

                foreach (Gem gem in LevelSelectScreen.characterGems)
                {
                    if (gem.Power == Gem.AbilityPower.ARMOR)
                        AddGem = false;
                }

                if (AddGem)
                    LevelSelectScreen.characterGems.AddLast(new ArmorGem());

                Game1.popScreenStack();
            }
        }
    }
}
