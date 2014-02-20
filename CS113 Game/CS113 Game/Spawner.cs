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
    //this class will spawn enemies off the screen
    public class Spawner
    {
        private Enemy[] enemies;
        private int max_Enemies;
        private int enemy_Count;
        private int current_Time;
        private int spawn_Time;

        public Vector2 position;
        public Enemy enemyToSpawn;

        Game1 gameRef;

        public Spawner(Game1 game, Vector2 position)
        {
            gameRef = game;
            max_Enemies = 20;
            current_Time = 0;
            spawn_Time = 1000; //we will wait 1000 milliseconds (1 seconds) to spawn enemies

            this.position = position;

            enemies = new Enemy[max_Enemies];
        }

        public void spawn()
        {
            current_Time = 0;

            if (enemy_Count < max_Enemies)
            {
                Enemy enemy = new Enemy(gameRef, position);
                enemies[enemy_Count] = enemy;
                enemy_Count++;

                Level.characterList.Add(enemy);
                Level.enemyList.Add(enemy);
            }
        }

        public void Update(GameTime gameTime)
        {
            current_Time += gameTime.ElapsedGameTime.Milliseconds;

            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                    enemy.Update();
            }

            if (current_Time - gameTime.ElapsedGameTime.Milliseconds >= spawn_Time)
            {
                spawn();
            }
        }
    }
}
