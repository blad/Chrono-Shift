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

        public AssaultRifle(Game1 game, Character character, bool target, Vector2 position)
            : base(game, character, target, position)
        {
            volume = 0.5f;
            weapon_Sound = Game1.content_Manager.Load<SoundEffect>("SoundEffects/WeaponSounds/AssaultFire");
            weapon_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Weapons/AssaultRifle");
            weapon_Rect = new Rectangle((int)position.X, (int)position.Y, weapon_Texture.Width/2, weapon_Texture.Height);
            sprite_Rect = new Rectangle(0, 0, weapon_Texture.Width / 2, weapon_Texture.Height);
            left_Sprite_Position = 100;
            
            texture_Offset = 75;

            startPosition = Vector2.Zero;

            attack_Speed = 50;
            bullet_Speed = 25;
            fireType = FireType.Auto;

            DrawOrder = 500;
            ammo = 150;

            this.bulletType = BulletType.RIFLE;

        }
    }
}
