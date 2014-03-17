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

        protected Rectangle character_Portrait_Rect_Player1;
        protected Rectangle portrait_Sprite_Rect_Player1;
        protected Rectangle health_Bar_Rect_Player1;
        protected Rectangle hb_Sprite_Rect_Player1;
        protected Rectangle power_Bar_Rect_Player1;
        protected Rectangle pb_Sprite_Rect_Player1;

        protected Rectangle character_Portrait_Rect_Player2;
        protected Rectangle portrait_Sprite_Rect_Player2;
        protected Rectangle health_Bar_Rect_Player2;
        protected Rectangle hb_Sprite_Rect_Player2;
        protected Rectangle power_Bar_Rect_Player2;
        protected Rectangle pb_Sprite_Rect_Player2;

        protected Vector2 gemWheel_Position = new Vector2(1050.0f, 650.0f);

        //we need to know where the HUD items are located on the image
        //as well as their sizes
        protected Vector2 portrait_Position_Player1 = new Vector2(25.0f, 25.0f);
        protected Vector2 portrait_Position_Player2 = new Vector2(800.0f, 25.0f);
        protected Vector2 portrait_Sprite_Location = Vector2.Zero;
        protected int portrait_Width = 86;
        protected int portrait_Height = 86;

        protected Vector2 health_Bar_Position_Player1 = new Vector2(200.0f, 25.0f);
        protected Vector2 health_Bar_Position_Player2 = new Vector2(975.0f, 25.0f);
        protected Vector2 hb_Sprite_Location = new Vector2(86.0f, 0.0f);
        protected int health_Bar_Width = 190;
        protected int health_Bar_Height = 24;

        protected Vector2 power_Bar_Position_Player1 = new Vector2(200.0f, 75.0f);
        protected Vector2 power_Bar_Position_Player2 = new Vector2(975.0f, 75.0f);
        protected Vector2 pb_Sprite_Location = new Vector2(86.0f, 24.0f);


        private TextEditor.Word ammoTextOne;
        private TextEditor.Word ammoTextTwo;
        public static TextEditor.Word AmmoCountOne;
        public static TextEditor.Word AmmoCountTwo;
        

        public int HealthWidth
        {
            get { return health_Bar_Width; }
            set { health_Bar_Width = value; }
        }

        public HUDManager()
        {
            hud_Items = Game1.content_Manager.Load<Texture2D>("Backgrounds/HUD/HUD_Items");
            portrait_Sprite_Rect_Player1 = new Rectangle(0, 0, portrait_Width, portrait_Height);
            character_Portrait_Rect_Player1 = new Rectangle((int)portrait_Position_Player1.X, (int)portrait_Position_Player1.Y, portrait_Width, portrait_Height);

            //we know the health bar is positioned right next to the character portrait
            hb_Sprite_Rect_Player1 = new Rectangle(portrait_Width, 0, health_Bar_Width, health_Bar_Height);
            health_Bar_Rect_Player1 = new Rectangle((int)health_Bar_Position_Player1.X, (int)health_Bar_Position_Player1.Y, 200, health_Bar_Height);

            pb_Sprite_Rect_Player1 = new Rectangle(portrait_Width, health_Bar_Height, health_Bar_Width, health_Bar_Height);
            power_Bar_Rect_Player1 = new Rectangle((int)power_Bar_Position_Player1.X, (int)power_Bar_Position_Player1.Y, 200, health_Bar_Height);


            portrait_Sprite_Rect_Player2 = new Rectangle(0, 0, portrait_Width, portrait_Height);
            character_Portrait_Rect_Player2 = new Rectangle((int)portrait_Position_Player2.X, (int)portrait_Position_Player2.Y, portrait_Width, portrait_Height);

            //we know the health bar is positioned right next to the character portrait
            hb_Sprite_Rect_Player2 = new Rectangle(portrait_Width, 0, health_Bar_Width, health_Bar_Height);
            health_Bar_Rect_Player2 = new Rectangle((int)health_Bar_Position_Player2.X, (int)health_Bar_Position_Player2.Y, 200, health_Bar_Height);

            pb_Sprite_Rect_Player2 = new Rectangle(portrait_Width, health_Bar_Height, health_Bar_Width, health_Bar_Height);
            power_Bar_Rect_Player2 = new Rectangle((int)power_Bar_Position_Player2.X, (int)power_Bar_Position_Player2.Y, 200, health_Bar_Height);


            gem_Wheel = Game1.content_Manager.Load<Texture2D>("Backgrounds/HUD/GemWheel");
            gem_Rect = new Rectangle((int)gemWheel_Position.X + gem_Wheel.Height/2, (int)gemWheel_Position.Y + gem_Wheel.Height/2, gem_Wheel.Width, gem_Wheel.Height);


            ammoTextOne = Level.text_Editor.word("AMMO", new Vector2(100, 680), 0.3f);
            ammoTextTwo = Level.text_Editor.word("AMMO", new Vector2(1000, 680), 0.3f);
            AmmoCountOne = Level.text_Editor.word(Level.player1.Weapon.Ammo.ToString(), new Vector2(100, 700), 0.5f);
            AmmoCountTwo = Level.text_Editor.word(Level.player2.Weapon.Ammo.ToString(), new Vector2(1000, 700), 0.5f);
        }

        //when we move across the game space, the hud needs to appear static relative to the camera
        public void translateHUD(int translation)
        {
            health_Bar_Position_Player1.X += translation;
            health_Bar_Rect_Player1.X += translation;

            power_Bar_Position_Player1.X += translation;
            power_Bar_Rect_Player1.X += translation;

            portrait_Position_Player1.X += translation;
            character_Portrait_Rect_Player1.X += translation;

            //translate player 2's hud as well
            health_Bar_Position_Player2.X += translation;
            health_Bar_Rect_Player2.X += translation;

            power_Bar_Position_Player2.X += translation;
            power_Bar_Rect_Player2.X += translation;

            portrait_Position_Player2.X += translation;
            character_Portrait_Rect_Player2.X += translation;

            gem_Rect.X += translation;

            Level.text_Editor.translateWord(ammoTextOne, translation, 0);
            Level.text_Editor.translateWord(ammoTextTwo, translation, 0);
            Level.text_Editor.translateWord(AmmoCountOne, translation, 0);
            Level.text_Editor.translateWord(AmmoCountTwo, translation, 0);
        }

        public void shortenHealthBar(int health, int playerNumber)
        {
            if (health > 100)
                health = 100;

            if (playerNumber == 1)
                health_Bar_Rect_Player1.Width = health * 2;
            else
                health_Bar_Rect_Player2.Width = health * 2;
        }

        public void shortenPowerBar(int power, int playerNumber)
        {
            if (power > 100)
                power = 100;

            if (playerNumber == 1)
                power_Bar_Rect_Player1.Width = power * 2;
            else
                power_Bar_Rect_Player2.Width = power * 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw player 1's hud
            spriteBatch.Draw(hud_Items, character_Portrait_Rect_Player1, portrait_Sprite_Rect_Player1, Color.Blue, 0.0f, portrait_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(hud_Items, health_Bar_Rect_Player1, hb_Sprite_Rect_Player1, Color.White, 0.0f, hb_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(hud_Items, power_Bar_Rect_Player1, pb_Sprite_Rect_Player1, Color.White, 0.0f, pb_Sprite_Location, SpriteEffects.None, 1.0f);

            //draw player 2's hud
            spriteBatch.Draw(hud_Items, character_Portrait_Rect_Player2, portrait_Sprite_Rect_Player2, Color.Red, 0.0f, portrait_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(hud_Items, health_Bar_Rect_Player2, hb_Sprite_Rect_Player2, Color.White, 0.0f, hb_Sprite_Location, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(hud_Items, power_Bar_Rect_Player2, pb_Sprite_Rect_Player2, Color.White, 0.0f, pb_Sprite_Location, SpriteEffects.None, 1.0f);


            //spriteBatch.Draw(gem_Wheel, gem_Rect, null, Color.White, 0.0f, new Vector2(gem_Wheel.Width/2, gem_Wheel.Height/2), SpriteEffects.None, 1.0f);
        }
    }
}
