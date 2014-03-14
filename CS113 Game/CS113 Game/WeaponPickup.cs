﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public class WeaponPickup : Item
    {
        int selectWeapon;
        //add an int to the constructor. This int will determint wich weapon to give the player
        //the weapon given to the player will depend on the int
        public WeaponPickup(Game1 game, Vector2 position, int selectWeapon)
            :base (game, position)
        {
            this.position = position;
            this.selectWeapon = selectWeapon;

            if (selectWeapon >= 75 && selectWeapon <= 100)
                item_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Pickups/Assault_Drop");

            else if (selectWeapon > 100 && selectWeapon <= 200)
                item_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Pickups/Laser_Drop");

            else
                item_Texture = Game1.content_Manager.Load<Texture2D>("Sprites/Pickups/Assault_Drop");


            
            item_Rect = new Rectangle((int)position.X, (int)position.Y, item_Texture.Width, item_Texture.Height);
        }

        public override void pickUp(MainCharacter character)
        {
            Weapon weapon;

            if (selectWeapon >= 75 && selectWeapon <= 100)
                weapon = new AssaultRifle(gameRef, character, false, character.position);
            else if (selectWeapon > 100 && selectWeapon <= 200)
                weapon = new LaserGun(gameRef, character, false, character.position);
            else
                weapon = new AssaultRifle(gameRef, character, false, character.position);

            character.switchWeapon(weapon);
            Level.itemList.Remove(this);
        }
    }
}
