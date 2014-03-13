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
        protected Texture2D hud_Items;
        protected Texture2D gem_Wheel;
        protected Rectangle gem_Rect;
        protected Rectangle character_Portrait_Rect;
        protected Rectangle portrait_Sprite_Rect;
        protected Rectangle health_Bar_Rect;
        protected Rectangle hb_Sprite_Rect;
        protected Rectangle power_Bar_Rect;
        protected Rectangle pb_Sprite_Rect;

        protected Vector2 gemWheel_Position = new Vector2(1050.0f, 650.0f);

        //we need to know where the HUD items are located on the image
        //as well as their sizes
        protected Vector2 portrait_Position = new Vector2(25.0f, 25.0f);
        protected Vector2 portrait_Sprite_Location = Vector2.Zero;
        protected int portrait_Width = 86;
        protected int portrait_Height = 86;

        protected Vector2 health_Bar_Position = new Vector2(200.0f, 25.0f);
        protected Vector2 hb_Sprite_Location = new Vector2(86.0f, 0.0f);
        protected int health_Bar_Width = 190;
        protected int health_Bar_Height = 24;

        protected Vector2 power_Bar_Position = new Vector2(200.0f, 75.0f);
        protected Vector2 pb_Sprite_Location = new Vector2(86.0f, 24.0f);

        public static TextEditor.Word AmmoWord;

        public int HealthWidth
        {
            get { return health_Bar_Width; }
            set { health_Bar_Width = value; }
        }

        public HUDManager()
        {
            hud_Items = Game1.content_Manager.Load<Texture2D>("Backgrounds/HUD/HUD_Items");
            portrait_Sprite_Rect = new Rectangle(0, 0, portrait_Width, portrait_Height);
            character_Portrait_Rect = new Rectangle((int)portrait_Position.X, (int)portrait_Position.Y, portrait_Width, portrait_Height);

            //we know the health bar is positioned right next to the character portrait
            hb_Sprite_Rect = new Rectangle(portrait_Width, 0, health_Bar_Width, health_Bar_Height);
            health_Bar_Rect = new Rectangle((int)health_Bar_Position.X, (int)health_Bar_Position.Y, 200, health_Bar_Height);

            pb_Sprite_Rect = new Rectangle(portrait_Width, health_Bar_Height, health_Bar_Width, health_Bar_Height);
            power_Bar_Rect = new Rectangle((int)power_Bar_Position.X, (int)power_Bar_Position.Y, 200, health_Bar_Height);


            gem_Wheel = Game1.content_Manager.Load<Texture2D>("Backgrounds/HUD/GemWheel");
            gem_Rect = new Rectangle((int)gemWheel_Position.X + gem_Wheel.Height/2, (int)gemWheel_Position.Y + gem_Wheel.Height/2, gem_Wheel.Width, gem_Wheel.Height);

            AmmoWord = Level.text_Editor.word(Level.current_Character.Weapon.Ammo.ToString(), new Vector2(1000, 25), 0.5f);
        }

        //when we move across the game space, the hud needs to appear static relative to the camera
        public void translateHUD(int translation)
        {
            health_Bar_Position.X += translation;
            health_Bar_Rect.X += translation;

            power_Bar_Position.X += translation;
            power_Bar_Rect.X += translation;

            portrait_Position.X += translation;
            character_Portrait_Rect.X += translation;

            gem_Rect.X += translation;

            Level.text_Editor.translateWord(AmmoWord, translation, 0);
        }

        public void shortenHealthBar(int health)
        {
            if (health > 100)
                health = 100;

            health_Bar_Rect.Width = health * 2;
        }

        public void shortenPowerBar(int power)
        {
            if (power > 100)
                power = 100;

            power_Bar_Rect.Width = power * 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hud_Items, character_Portrait_Rect, portrait_Sprite_Rect, Color.White, 0.0f, portrait_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(hud_Items, health_Bar_Rect, hb_Sprite_Rect, Color.White, 0.0f, hb_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(hud_Items, power_Bar_Rect, pb_Sprite_Rect, Color.White, 0.0f, pb_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(gem_Wheel, gem_Rect, null, Color.White, 0.0f, new Vector2(gem_Wheel.Width/2, gem_Wheel.Height/2), SpriteEffects.None, 1.0f);
        }
    }
}
