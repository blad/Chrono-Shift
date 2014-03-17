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
    public abstract class Enemy : Character
    {
        protected MainCharacter character_To_Attack;

        protected int attack_Time = 200; //the amount of time we will wait to attempt an attack
        protected int current_Attack_Time = 0; //this will be compared with attack_Time to see if enough time has passed to attack
        protected bool attacking = false;
        protected Spawner spawner;

        protected bool has_Gravity = true;

        public Enemy(Game1 game, Spawner spawner)
            : base(game)
        {
            gameRef = game;
            this.spawner = spawner;

            Random rand = new Random();
            int r = rand.Next(1,3);

           foreach (MainCharacter c in Level.playerList)
           {
               if (c.characterNumber == r)
                   character_To_Attack = c;
           }

        }

        public Enemy(Game1 game)
            : base(game)
        {
            DrawOrder = 450;
        }
   

        public override void Update(GameTime gameTime)
        {
            if (health <= 0)
            {
                Level.enemyList.Remove(this);
                Level.characterList.Remove(this);

                if (spawner != null)
                    spawner.Enemies.Remove(this);
                else
                    Level.bombList.Remove(this); //only needed for bombs, this code is starting to be weird. need to revise the way bombs work


                Random rng = new Random();

                int random = rng.Next(500);

                if (random < 75) //we have a 15% chance of dropping a health pack
                    Level.itemList.Add(new HealthPack(gameRef, this.position));
                else if (random >= 75 && random < 200)
                    Level.itemList.Add(new WeaponPickup(gameRef, this.position, random));
                else if (random >= 200 && random <= 500)
                    Level.itemList.Add(new PowerPack(gameRef, this.position));
            }

            previous_Game_Time = current_Game_Time;
            current_Game_Time = gameTime;

            if (has_Gravity)
            {
                activeGravity();
            }

            AIroutine(gameTime);

            character_Rect.X = (int)position.X;
            character_Rect.Y = (int)position.Y;

            if (currentEffect == Effect.FIRE)
            {
                takeFireDOT(gameTime);
            }

            if (has_Weapon)
            {
                equipped_Weapon.changePosition(position);
                equipped_Weapon.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(character_Texture, character_Rect, sprite_Rect, color_Tint, 0.0f, origin, SpriteEffects.None, 1.0f);

            if (has_Weapon)
            {
                equipped_Weapon.Draw(spriteBatch);
            }

            if (!effect_Active)
                color_Tint = Color.White;
        }

        //Abstract methods that every enemy must have
        public abstract void Attack();
        public abstract void AIroutine(GameTime gameTime);
    
    }
}
