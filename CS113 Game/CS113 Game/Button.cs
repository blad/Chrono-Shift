using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS113_Game
{
    public class Button
    {
        private Texture2D button_Texture;

        private Vector2 position;

        private int button_Width;
        private int button_Height;

        private Rectangle button_Rect;

        private bool has_Texture;

        public Rectangle Rect
        {
            get { return button_Rect; }
        }

        //first constructor that takes a texture
        public Button(Texture2D texture, int positionX, int positionY)
        {
            button_Texture = texture;
            position = new Vector2(positionX, positionY);
            button_Width = texture.Width;
            button_Height = texture.Height;

            button_Rect= new Rectangle(positionX, positionY, button_Width, button_Height);

            has_Texture = true;
        }

        //not all buttons have to have textures so this constructor allows us to have button without a texture
        public Button(int positionX, int positionY, int width, int height)
        {
            position = new Vector2(positionX, positionY);
            button_Width = width;
            button_Height = height;

            button_Rect = new Rectangle(positionX, positionY, button_Width, button_Height);

            has_Texture = false;
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool buttonPressed(InputHandler handler)
        {
            return (handler.leftMouseClicked() && button_Rect.Contains(handler.mousePosition()) || handler.buttonPressed(Buttons.A));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (has_Texture)
                spriteBatch.Draw(button_Texture, button_Rect, Color.White);
        }
    }
}
