using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlphaKilo_GameJam32
{
    public class SceneGameOver : Scene
    {
        private Rectangle Screen;
        public SceneGameOver(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneGameOver");
        }

        public override void Load()
        {
            Debug.WriteLine("SceneGameOver.Load");

            // Taille de l'écran
            Screen = mainGame.Window.ClientBounds;

            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("SceneGameOver.Unload");
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
         
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "Game Over !", new Vector2((Screen.Width/2) - 60, Screen.Height / 2), Color.White);

            base.Draw(gameTime);
        }
    }
}
