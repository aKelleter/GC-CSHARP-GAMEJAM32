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
        public float life;
        public int frame;
        public int maxFrame; 
        public List<int> listFrameTirs;
        public Tirs(Texture2D pTexture) : base(pTexture)
        {
            frame = 1;
            maxFrame = 1;
            listFrameTirs = new List<int>();
        }
        public override void TouchedBy(IActor pActor)
        {
            if (pActor is Ennemis)
            {
                life -= 10.0f;
            }
        }
    }
}
