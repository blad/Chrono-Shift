using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace CS113_Game
{
    public abstract class Weapon : DrawableGameComponent
    {
        protected Rectangle weapon_Rect;
        protected Vector2 position;
        protected SoundEffect weapon_Sound;
        protected int max_Ammo_Count;
        protected int current_Ammo_Count;
        protected int attack_Speed;
        protected int time_Passed;

        public enum FireType { Auto, SingleShot };
        public FireType fireType;

        public Weapon(Game1 game)
            : base(game)
        {
            
        }

        public void changePosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public abstract void fire(Point mousePoint,  int offset);
        public abstract void Update(GameTime gameTime, InputHandler handler);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
