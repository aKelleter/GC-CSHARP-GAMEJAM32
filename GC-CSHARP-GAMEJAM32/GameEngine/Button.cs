using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{

    public delegate void OnClick(Button pSender);
    public class Button : Sprite
    {

        public bool isHover { get; private set; }
        private MouseState oldMSState;
        public OnClick onClick { get; set; }
        
        public Button(Texture2D pTexture) : base(pTexture)
        {

        }

        public override void Update(GameTime pGameTime)
        {
            MouseState newMSState = Mouse.GetState();
            Point mousePos = newMSState.Position;

            
            // Si le bouton détecte la souris
            if(boundingBox.Contains(mousePos))
            { 
                // Si il n'est pas survolé
                if(!isHover)
                {
                    isHover = true;
                    Debug.WriteLine("Button is now hover");
                }
            }
            else
            {
                if(isHover == true)
                {
                    Debug.WriteLine("Button is no more hover"); 
                }
                isHover = false;
            }

            
            if(isHover)
            {
                if(newMSState.LeftButton == ButtonState.Pressed && oldMSState.LeftButton == ButtonState.Pressed)
                {
                    Debug.WriteLine("Button is Clicked");
                    if(onClick != null)
                    {
                        onClick(this);
                    }
                }
            }

            oldMSState = newMSState;

            base.Update(pGameTime);
        }

    }
}
