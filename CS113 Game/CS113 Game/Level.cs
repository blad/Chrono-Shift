using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CS113_Game
{
    public abstract class Level : DrawableGameComponent
    {
        protected Texture2D level_Texture;
        protected Texture2D ground_Texture;
        protected Texture2D platform_Texture;
        protected Rectangle level_Rect;
        protected Rectangle ground_Rect;
        protected SpriteBatch spriteBatch;
        protected ArrayList platformList;
        protected Game1 gameRef;
        
        private int distance_From_Camera = 200;

        public static MainCharacter current_Character;
        public static ArrayList enemyList;
        public static ArrayList characterList;
        public static int screen_Offset = 0;


        public Level(Game game)
            : base(game)
        {
            DrawOrder = 50;
            platformList = new ArrayList();
            characterList = new ArrayList();
            enemyList = new ArrayList();
        }

        public void Update(GameTime gameTime, InputHandler handler)
        {
            current_Character.Update(gameTime, handler, screen_Offset);
            //Rectangle characterRect = current_Character.getCharacterRect();

            foreach (Enemy enemy in enemyList)
                enemy.Update();

            foreach (Character character in characterList)
            {
                Rectangle characterRectangle = character.getCharacterRect();

                if (characterRectangle.Intersects(ground_Rect)
                    && characterRectangle.Bottom > ground_Rect.Top
                    && !character.jumping
                    && !character.grounded)
                {
                    character.ground(ground_Rect.Y + ground_Rect.Height / 3);
                    character.standing_Platform = ground_Rect;
                }

                //check if we are on any of the platforms
                foreach (Rectangle rect in platformList)
                {
                    if (characterRectangle.Intersects(rect)
                        && characterRectangle.Bottom > rect.Top
                        && characterRectangle.Bottom < rect.Bottom
                        && character.falling
                        && !character.grounded)
                    {
                        character.ground(rect.Y + rect.Height / 2);
                        character.standing_Platform = rect;
                    }
                }
            }

            //if the character is past a certain point on the camera's view, move the camera at the same speed the character moves
            if (current_Character.getPosition().X > (-Game1.cam.position.X + GraphicsDeviceManager.DefaultBackBufferWidth / 2 - distance_From_Camera))
            {
                Game1.cam.Move(new Vector2(-(float)current_Character.getSpeed(), 0.0f));
                screen_Offset += current_Character.getSpeed();
            }

            else if (screen_Offset > 0 && current_Character.getPosition().X < (-Game1.cam.position.X - GraphicsDeviceManager.DefaultBackBufferWidth / 2 + distance_From_Camera))
            {
                Game1.cam.Move(new Vector2((float)current_Character.getSpeed(), 0.0f));
                screen_Offset -= current_Character.getSpeed();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, 
                                BlendState.AlphaBlend, 
                                null, null, null, null, 
                                Game1.cam.getTransformation(gameRef.GraphicsDevice));
           
            spriteBatch.Draw(level_Texture, level_Rect, Color.White);
            spriteBatch.Draw(ground_Texture, ground_Rect, Color.White);

            foreach (Rectangle rect in platformList)
            {
                spriteBatch.Draw(platform_Texture, rect, Color.White);
            }

            current_Character.Draw(gameTime, spriteBatch);
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(gameTime, spriteBatch);
            }


            spriteBatch.End();
        }
    }
}
