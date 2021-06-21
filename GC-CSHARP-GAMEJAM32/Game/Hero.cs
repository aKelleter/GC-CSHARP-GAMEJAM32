using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    public class Hero : Sprite
    {
        public float energy;
        public Hero(Texture2D pTexture) : base(pTexture) 
        {
            energy = 100;
        }
        public override void TouchedBy(IActor pActor)   
        {
            if(pActor is Ennemis)
            {
                energy -= 10.0f;
            }
        }
    }
}
