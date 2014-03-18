using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace CS113_Game
{
    public class LaserGun : Gun
    {

        public LaserGun(Game1 game, Character character, bool target, Vector2 position)
            : base(game, character, target, position)
        {
            volume = 1.0f;
            weapon_Sound = Game1.content_Manager.Load<SoundEffect>("SoundEffects/WeaponSounds/laser_shot");
            weapon_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Weapons/LaserRifle");
            weapon_Rect = new Rectangle((int)position.X, (int)position.Y, weapon_Texture.Width/2, weapon_Texture.Height);
            sprite_Rect = new Rectangle(0, 0, weapon_Texture.Width / 2, weapon_Texture.Height);
            left_Sprite_Position = 100;
 
            texture_Offset = 75;

            attack_Speed = 200;
            bullet_Speed = 40;
            fireType = FireType.Auto;

            DrawOrder = 500;

            ammo = 50;
            this.bulletType = BulletType.LASER;
        }

    }
}
