﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace CS113_Game
{
    public abstract class Level : DrawableGameComponent
    {
        private Texture2D reticle;
        private Rectangle reticle_Rect;
        private Camera2D cam;
        private int distance_From_Camera = 250;

        protected Texture2D level_Texture;
        protected Texture2D ground_Texture;
        protected Texture2D platform_Texture;
        protected Rectangle level_Rect;
        protected SpriteBatch spriteBatch;
        protected ArrayList platformList;
        protected ArrayList spawners;
        protected Game1 gameRef;

        public static Rectangle ground_Rect;

        public static HUDManager HUD;
        public static TextEditor text_Editor;
        public static MainCharacter current_Character;
        public static ArrayList characterList;
        public static ArrayList enemyList;
        public static ArrayList itemList;
        public static ArrayList bombList; //only needed for WWII
        public static int screen_Offset;


        public Level(Game1 game)
            : base(game)
        {
            
            gameRef = game;
            gameRef.IsMouseVisible = false;
            DrawOrder = 50;

            cam = new Camera2D();
            cam.position = new Vector2((float)-Game1.screen_Width / 2, (float)-Game1.screen_Height / 2);

            platformList = new ArrayList();
            characterList = new ArrayList();
            enemyList = new ArrayList();
            itemList = new ArrayList();
            bombList = new ArrayList();
            spawners = new ArrayList();


            Texture2D characterTexture = Game1.content_Manager.Load<Texture2D>("Sprites/Characters/AkiraSpriteSheet");

            current_Character = new MainCharacter(game, characterTexture, new Vector2(100.0f, 600.0f));
            current_Character.Gems = LevelSelectScreen.characterGems;
            characterList.Add(current_Character);

            text_Editor = new TextEditor();
            HUD = new HUDManager();

            reticle = Game1.content_Manager.Load<Texture2D>("Sprites/Projectiles/Reticle");
            reticle_Rect = new Rectangle(0, 0, reticle.Width, reticle.Height);

            screen_Offset = 0;
        }

        public void Update(GameTime gameTime, InputHandler handler)
        {
            Point mousePosition = handler.mousePosition();

            reticle_Rect.X = screen_Offset + mousePosition.X - reticle.Width/2;
            reticle_Rect.Y = mousePosition.Y - reticle.Height/2;


            current_Character.Update(gameTime, handler);
            
            //update all the spawners
            foreach (Spawner spawner in spawners)
                spawner.Update(gameTime);



            //we need try/catch blocks just in case the list is empty and the game wants to crash (lame)
            try
            {
                foreach (Bomb bomb in bombList)
                {
                    if (bomb != null)
                        bomb.Update(gameTime);
                }

                //update all the items in the level

                foreach (Item item in itemList)
                {
                    if (item != null)
                        item.Update();
                }
            }
            catch (Exception e)
            {
                //do nothing
                //code is getting weird and redundant. fix this
            }

            //each character in the list needs to be checked to see if they are any of the platforms
            foreach (Character character in characterList)
            {
                Rectangle characterRectangle = character.getCharacterRect();

                if (characterRectangle.Intersects(ground_Rect)
                    && characterRectangle.Bottom > ground_Rect.Top
                    && !character.jumping
                    && !character.grounded)
                {
                    character.ground(ground_Rect.Y + ground_Rect.Height / 2);
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
            if (screen_Offset < level_Texture.Width - Game1.screen_Width && current_Character.getPosition().X > (-cam.position.X + Game1.screen_Width / 2 - distance_From_Camera))
            {
                cam.Move(new Vector2(-(float)current_Character.getSpeed(), 0.0f));
                screen_Offset += current_Character.getSpeed();
                HUD.translateHUD(current_Character.getSpeed());
            }

            else if (screen_Offset > 0 && current_Character.getPosition().X < (-cam.position.X - Game1.screen_Width / 2 + distance_From_Camera))
            {
                cam.Move(new Vector2((float)current_Character.getSpeed(), 0.0f));
                screen_Offset -= current_Character.getSpeed();
                HUD.translateHUD(-current_Character.getSpeed());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //everything is drawn in a layer going down
            // Everything drawn first is in the backmost layer

            spriteBatch.Begin(SpriteSortMode.Deferred, 
                                BlendState.AlphaBlend, 
                                null, null, null, null, 
                                cam.getTransformation(gameRef.GraphicsDevice));
           
            spriteBatch.Draw(level_Texture, level_Rect, Color.White);
            //spriteBatch.Draw(ground_Texture, ground_Rect, Color.White);

            foreach (Rectangle rect in platformList)
            {
                spriteBatch.Draw(platform_Texture, rect, Color.White);
            }

            current_Character.Draw(gameTime, spriteBatch);

            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            try
            {
                foreach (Item item in itemList)
                {
                    if (item != null)
                        item.Draw(spriteBatch);
                }
            }
            catch (Exception e)
            {
                //do nothing
                //code is getting weird and redundant. fix this
            }

            HUD.Draw(spriteBatch);
            text_Editor.Draw(spriteBatch);

            spriteBatch.Draw(reticle, reticle_Rect, Color.White);

            spriteBatch.End();
        }
    }
}
