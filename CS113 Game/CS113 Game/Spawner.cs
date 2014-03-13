using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace CS113_Game
{
    //this class will spawn enemies off the screen
    public class Spawner
    {
        private Character character_To_Watch = Level.current_Character;

        private ArrayList enemies;
        private int max_Enemies;
        private int spawn_Time;

        //these three private variables should remain untouched by any outside class
        private int enemy_Count;
        private int current_Time;
        private bool active = false;
        

        public Vector2 position;
        public enum EnemyType { basic, segway, mummy, naziPlane,naziTank, Sphinx,Robama, FireRex}
        public EnemyType typeToSpawn;
        Game1 gameRef;

        public EnemyType TypeToSpawn
        {
            get { return typeToSpawn; }
            set { typeToSpawn = value; }
        }

        public int Max_Enemies
        {
            get { return max_Enemies; }
            set { max_Enemies = value; }
        }

        public int Spawn_Time
        {
            get { return spawn_Time; }
            set { spawn_Time = value; }
        }

        public ArrayList Enemies
        {
            get { return enemies; }
        }

        public Spawner(Game1 game, Vector2 position, EnemyType typeToSpawn)
        {
            gameRef = game;
            max_Enemies = 10;
            current_Time = 0;
            spawn_Time = 3000; //we will wait 3000 milliseconds (3 seconds) to spawn enemies

            this.position = position;
            this.typeToSpawn = typeToSpawn;

            enemies = new ArrayList() ;
        }

        public void spawn()
        {
            current_Time = 0;

            if (active && enemy_Count < max_Enemies)
            {
                Enemy enemy;

                switch (typeToSpawn)
                {
                    case (EnemyType.basic):
                        enemy = new BasicSoldierEnemy(gameRef, this, this.position);
                        break;
                    case (EnemyType.mummy):
                        enemy = new MummyEnemy(gameRef, this, this.position);
                        break;
                    case (EnemyType.naziPlane):
                        enemy = new NaziPlaneEnemy(gameRef, this, this.position);
                        break;
                    case (EnemyType.naziTank):
                        enemy = new NaziTankEnemy(gameRef, this, this.position);
                        break;
                    case (EnemyType.Robama):
                        enemy = new RobamaBoss(gameRef, this, this.position);
                        break;
                    case (EnemyType.segway):
                        enemy = new SegwayEnemy(gameRef, this, this.position);
                        break;
                    case (EnemyType.Sphinx):
                        enemy = new SphinxBoss(gameRef, this, this.position);
                        break;
                    case (EnemyType.FireRex):
                        enemy = new FireRexBoss(gameRef, this, this.position);
                        break;
                    default: //we should never reach this, this is just so we can allow ourselves to add an enemy without getting an unassigned element error
                        enemy = new BasicSoldierEnemy(gameRef, this, this.position);
                        break;
                }

                enemies.Add(enemy);
                enemy_Count++;
                Level.characterList.Add(enemy);
                Level.enemyList.Add(enemy);

            }
        }

        public void Update(GameTime gameTime)
        {
            current_Time += gameTime.ElapsedGameTime.Milliseconds;

            //when the player has come close enough to the spawner, the spawner becomes active
            if (position.X - character_To_Watch.position.X < 500)
                active = true;

            try
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy != null)
                        enemy.Update(gameTime);
                }
            }
            catch (Exception e)
            {
                //do nothing, we just need to make sure that the game doesn't crash (this code sucks, i know)
            }

            if (current_Time - gameTime.ElapsedGameTime.Milliseconds >= spawn_Time)
            {
                spawn();
            }
        }
    }
}
