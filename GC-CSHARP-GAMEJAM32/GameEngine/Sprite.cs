using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    public class Sprite : ISpriteManager
    {
        // Membres /////////////////////////////////////////////////////////////
        public Vector2 position { get; set; }           // IActor IMPLEMENTATION
        public Rectangle boundingBox { get; set; }      // IActor IMPLEMENTATION
        public bool ToRemove { get; set; }              // IActor IMPLEMENTATION
        public float Energy { get; set; }              // IActor IMPLEMENTATION

        public Texture2D texture { get; }               // Sprite IMPLEMENTATION
        public float Velocity_X { get; set; }           // Sprite IMPLEMENTATION
        public float Velocity_Y { get; set; }           // Sprite IMPLEMENTATION

        // Methodes /////////////////////////////////////////////////////////////
        public virtual void Draw(SpriteBatch pSpriteBatch)      // IActor IMPLEMENTATION
        {
        
            pSpriteBatch.Draw(texture, position, Color.White);

        }
        public virtual void Update(GameTime pGameTime)          // IActor IMPLEMENTATION
        {
            Move(Velocity_X, Velocity_Y);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height );
        }

        public virtual void DamageOnSprite(ISpriteManager pSprite)     // IActor IMPLEMENTATION
        {

        }


        // -------------------------------------

        // CONSTRUCTOR
        public Sprite(Texture2D pTexture)                        // Sprite IMPLEMENTATION
        {
            texture = pTexture;
            ToRemove = false;
        }
        public void Move (float pX, float pY)                   // Sprite IMPLEMENTATION
        {
            position = new Vector2(position.X + pX, position.Y + pY);
        }
        


    }
}
