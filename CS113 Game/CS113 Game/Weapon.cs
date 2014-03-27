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
        protected Character source_Character;
        protected Rectangle weapon_Rect;
        protected Rectangle sprite_Rect;
        protected Vector2 position;
        protected SoundEffect weapon_Sound;
        protected int ammo;
        protected int left_Sprite_Position;
        public int attack_Speed;
        protected int time_Passed;

        public int textureXOffset = 0;
        public int texture_Offset;

        protected bool player_Target;
        public float theta;

        public enum FireType { Auto, SingleShot };
        public FireType fireType;

        public int Ammo
        {
            get { return ammo; }
        }

        public int Offset
        {
            get { return texture_Offset; }
            set { texture_Offset = value; }
        }

        public Weapon(Game1 game, Character character, bool target)
            : base(game)
        {
            source_Character = character;
            player_Target = target;
        }

        

        public void changePosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        //one fire method is for players, the other for enemies
        public abstract void fire(InputHandler handler);
        public abstract void fire(Character characterToAttack);
        public abstract void Update(GameTime gameTime, InputHandler handler);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
