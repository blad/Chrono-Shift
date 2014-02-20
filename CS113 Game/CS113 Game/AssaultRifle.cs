using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace CS113_Game
{
    public class AssaultRifle : Gun
    {

        public AssaultRifle(Game1 game, Vector2 position)
            : base(game)
        {
            weapon_Sound = Game1.content_Manager.Load<SoundEffect>("SoundEffects/WeaponSounds/AssaultFire");
            weapon_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Weapons/AssaultRifle");
            weapon_Rect = new Rectangle((int)position.X, (int)position.Y, weapon_Texture.Width, weapon_Texture.Height);
            this.position = position;
            texture_Offset = 60;

            bullet_List = new Bullet[max_Bullets_To_Draw];
            startPosition = Vector2.Zero;

            attack_Speed = 50;
            bullet_Speed = 25;
            theta = 90.0f;
            current_Ammo_Count = max_Ammo_Count = 200;
            fireType = FireType.Auto;

            DrawOrder = 500;
            

        }
    }
}
