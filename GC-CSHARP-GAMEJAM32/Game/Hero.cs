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
        public Hero(Texture2D pTexture) : base(pTexture) 
        {
            Energy = 100;
        }
                
        public override void DamageOnSprite(ISpriteManager pSprite)   
        {
            if(pSprite is Ennemis)
            {
                pSprite.Energy -= 100.0f;
            }
        }

    }
}
