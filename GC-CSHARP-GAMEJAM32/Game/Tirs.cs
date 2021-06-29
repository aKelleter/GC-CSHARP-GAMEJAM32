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
        
        public Tirs(Texture2D pTexture) : base(pTexture)
        {
            Energy = 100.0f;            
        }
        public override void DamageOnSprite(ISpriteManager pSprite)
        {
            
            if (pSprite is Hero)
            {
                pSprite.Energy -= 10.0f;
            }

            if (pSprite is Ennemis)
            {
                pSprite.Energy -= 50.0f;
            }
            
        }
    }
}
