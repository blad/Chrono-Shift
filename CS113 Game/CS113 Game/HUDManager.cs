using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace CS113_Game
{
    public class HUDManager
    {
        private Texture2D hud_Items;
        private Texture2D gem_Wheel;
        private Rectangle gem_Rect;
        private Rectangle character_Portrait_Rect;
        private Rectangle portrait_Sprite_Rect;
        private Rectangle health_Bar_Rect;
        private Rectangle hb_Sprite_Rect;

        private Vector2 gemWheel_Position = new Vector2(1050.0f, 650.0f);

        //we need to know where the HUD items are located on the image
        //as well as their sizes
        private Vector2 portrait_Position = new Vector2(25.0f, 25.0f);
        private Vector2 portrait_Sprite_Location = Vector2.Zero;
        private int portrait_Width = 86;
        private int portrait_Height = 86;

        private Vector2 health_Bar_Position = new Vector2(200.0f, 25.0f);
        private Vector2 hb_Sprite_Location = new Vector2(86.0f, 0.0f);
        private int health_Bar_Width = 191;
        private int health_Bar_Height = 24;

        public HUDManager()
        {
            hud_Items = Game1.content_Manager.Load<Texture2D>("Backgrounds/HUD/HUD_Items");
            portrait_Sprite_Rect = new Rectangle(0, 0, portrait_Width, portrait_Height);
            character_Portrait_Rect = new Rectangle((int)portrait_Position.X, (int)portrait_Position.Y, portrait_Width, portrait_Height);

            //we know the health bar is positioned right next to the character portrait
            hb_Sprite_Rect = new Rectangle(portrait_Width, 0, health_Bar_Width, health_Bar_Height);
            health_Bar_Rect = new Rectangle((int)health_Bar_Position.X, (int)health_Bar_Position.Y, health_Bar_Width, health_Bar_Height);


            gem_Wheel = Game1.content_Manager.Load<Texture2D>("Backgrounds/HUD/GemWheel");
            gem_Rect = new Rectangle((int)gemWheel_Position.X, (int)gemWheel_Position.Y, gem_Wheel.Width, gem_Wheel.Height);

        }

        //when we move across the game space, the hud needs to appear static relative to the camera
        public void translateHUD(int translation)
        {
            health_Bar_Position.X += translation;
            health_Bar_Rect.X += translation;

            portrait_Position.X += translation;
            character_Portrait_Rect.X += translation;

            gem_Rect.X += translation;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hud_Items, character_Portrait_Rect, portrait_Sprite_Rect, Color.White, 0.0f, portrait_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(hud_Items, health_Bar_Rect, hb_Sprite_Rect, Color.White, 0.0f, hb_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(gem_Wheel, gem_Rect, Color.White);
        }
    }
}
