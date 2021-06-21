using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    public class Meteor : Sprite
    {
        
        public Meteor(Texture2D pTexture) : base(pTexture)
        {
            
            // Initialisation de la vélocité 
            /*
            do
            {
                Velocity_X = (float)Tools.RandomInt(-3, 3) / 5;
            } while (Velocity_X == 0);
            */
            do
            {
                Velocity_Y = (float)Tools.RandomInt(3, 6) / 5;
            } while (Velocity_Y == 0);

        }
    }
}
