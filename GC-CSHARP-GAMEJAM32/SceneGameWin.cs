using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlphaKilo_GameJam32
{
    public class SceneGameWin : Scene
    {
        private Rectangle Screen;
        public SceneGameWin(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneGameWin");
        }

        public override void Load()
        {
            Debug.WriteLine("SceneGameWin.Load");

            // Taille de l'écran
            Screen = mainGame.Window.ClientBounds;

            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("SceneGameWin.Unload");
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "Congratulation YOU WIN !", new Vector2((Screen.Width / 2) - 140, Screen.Height / 2), Color.White);

            base.Draw(gameTime);
        }
    }
}
