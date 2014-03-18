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
    class EmptyGun : Gun
    {
        public EmptyGun(Game1 game, Character character, bool target, Vector2 position)
            : base(game, character, target, position)
        {
            volume = 1.0f;
            hasTexture = false;
            weapon_Sound = Game1.content_Manager.Load<SoundEffect>("SoundEffects/WeaponSounds/boss_laser");

            left_Sprite_Position = 100;
 
            texture_Offset = 75;

            attack_Speed = 200;
            bullet_Speed = 40;
            fireType = FireType.Auto;

            DrawOrder = 500;

            ammo = 25;
        }
    }
}
