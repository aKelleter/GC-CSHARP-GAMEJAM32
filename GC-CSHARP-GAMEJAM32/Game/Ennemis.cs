﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    public class Ennemis : Sprite
    {
        public Ennemis(Texture2D pTexture) : base(pTexture)
        {
            // Initialisations
            Energy = 100.0f;

            // Vitesse de déplacement aléatoire sur l'axe des Y
            do
            {
                Velocity_Y = (float)Tools.RandomInt(5, 10) / 5;
            } while (Velocity_Y == 0);

        }
        
        public override void DamageOnSprite(ISpriteManager pSprite)
        {
            if (pSprite is Hero)
            {
                pSprite.Energy -= 10.0f;
            }
            
        }

    }
}
