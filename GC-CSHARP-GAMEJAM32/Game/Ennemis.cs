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
        public bool endormi;  
        public int chronotir;
        public float energy;
        public Ennemis(Texture2D pTexture) : base(pTexture)
        {
            // Initialisations
            endormi = true;       
            chronotir = 0;
            energy = 100;

            // Vitesse de déplacement aléatoire sur l'axe des X
            do
            {
                Velocity_Y = (float)Tools.RandomInt(1, 6) / 5;
            } while (Velocity_Y == 0);

        }

        public override void TouchedBy(IActor pActor)
        {
            if (pActor is Hero)
            {
                energy -= 10.0f;
            }
        }

    }
}
