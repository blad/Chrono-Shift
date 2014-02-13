using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS113_Game
{
    public class InputHandler
    {
         //we must keep track of state changes for keyboard and mouse inputs
        private static KeyboardState current_Keyboard_State;
        private static KeyboardState previous_Keyboard_State;
        private static MouseState current_Mouse_State;
        private static MouseState previous_Mouse_State;

        //get and set methods
        public static KeyboardState Current_Keyboard_State()
        {
            return current_Keyboard_State;
        }

        public static KeyboardState Previous_Keyboard_State()
        {
            return previous_Keyboard_State;
        }

        public static MouseState Current_Mouse_State()
        {
            return current_Mouse_State;
        }

        public static MouseState Previous_Mouse_State()
        {
            return previous_Mouse_State;
        }

        //constructor
        void InputHanlder()
        {
            current_Keyboard_State = Keyboard.GetState();
            current_Mouse_State = Mouse.GetState();
        }

        public void Update(GameTime gameTime)
        {
            previous_Keyboard_State = current_Keyboard_State;
            current_Keyboard_State = Keyboard.GetState();
            previous_Mouse_State = current_Mouse_State;
            current_Mouse_State = Mouse.GetState();
        }

        public bool keyReleased(Keys key)
        {
            return (previous_Keyboard_State.IsKeyDown(key) && current_Keyboard_State.IsKeyUp(key));
        }

        public bool keyPressed(Keys key)
        {
            return (previous_Keyboard_State.IsKeyUp(key) && current_Keyboard_State.IsKeyDown(key));
        }

        public Point mousePosition()
        {
            return new Point(current_Mouse_State.X, current_Mouse_State.Y);
        }

        public bool leftMouseClicked()
        {
            return (previous_Mouse_State.LeftButton == ButtonState.Pressed) && 
                (current_Mouse_State.LeftButton == ButtonState.Released);
        }
    }
}
