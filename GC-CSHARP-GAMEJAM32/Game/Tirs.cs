using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    public class Tirs : Sprite
    {
        public float energy;               
        public List<int> listFrameTirs;

        public Tirs(Texture2D pTexture) : base(pTexture)
        {
           listFrameTirs = new List<int>();
           energy = 100.0f;            
        }
        public override void TouchedByActors(IActor pActor)
        {
            if (pActor is Hero)
            {
                energy -= 100.0f;
            }
        }
    }
}
