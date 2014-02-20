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
    public class Enemy : Character
    {
        private MainCharacter character_To_Attack = Level.current_Character;
        private int list_Position;

        public Enemy(Game game, Vector2 startPosition)
            : base(game)
        {
            health = 15;
            movement_Speed = 2;
            position = startPosition;
            character_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/TestEnemy");
            character_Rect = new Rectangle((int)position.X, (int)position.Y, character_Texture.Width, character_Texture.Height);
            color_Tint = Color.White;

            texture_Offset = character_Texture.Height;
        }

        public void AIroutine()
        {
            if (character_To_Attack.position.X > position.X)
            {
                moveRight();
            }
            else if (character_To_Attack.position.X < position.X)
            {
                moveLeft();
            }

            if (health <= 0)
            {
                Level.enemyList.Remove(this);
            }

        }

        public void Update()
        {
            activeGravity();
            AIroutine();

            character_Rect.X = (int)position.X;
            character_Rect.Y = (int)position.Y;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(character_Texture, character_Rect, color_Tint);

            color_Tint = Color.White;
        }
    }
}
