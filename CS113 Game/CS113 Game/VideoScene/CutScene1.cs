using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CS113_Game.VideoScene
{
    class CutScene1: Screen
    {
        //SpriteBatch spriteBatch;
        Video video;
        VideoPlayer player;
        Texture2D videoTexture;

        protected Game gameRef; 
        
        public CutScene1(Game1 game)
        {
            gameRef = game;
            LoadContent();
        }

        protected void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            video = gameRef.Content.Load<Video>("Videos/MetalSlug");
            player = new VideoPlayer();
        }

        public override void Update(GameTime gameTime, InputHandler handler)
        {
            if (player.State == MediaState.Stopped)
            {
                player.IsLooped = true;
                player.Play(video);
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gameRef.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Only call GetTexture if a video is playing or paused
            if (player.State != MediaState.Stopped)
                videoTexture = player.GetTexture();

            // Drawing to the rectangle will stretch the 
            // video to fill the screen
            Rectangle screen = new Rectangle(gameRef.GraphicsDevice.Viewport.X,
                gameRef.GraphicsDevice.Viewport.Y,
                gameRef.GraphicsDevice.Viewport.Width,
                gameRef.GraphicsDevice.Viewport.Height);

            // Draw the video, if we have a texture to draw.
            if (videoTexture != null)
            {
                spriteBatch.Draw(videoTexture, screen, Color.White);
            }
        }
    }
}
