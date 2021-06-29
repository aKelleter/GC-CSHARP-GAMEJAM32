using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlphaKilo_GameJam32
{
    public class SceneGameOver : SceneManager
    {
        private Rectangle Screen;
        private KeyboardState oldKBState;
        public SceneGameOver(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneGameOver");
        }

        public override void Load()
        {
            Debug.WriteLine("SceneGameOver.Load");

            // Taille de l'écran
            Screen = mainGame.Window.ClientBounds;

            // Sauvegardes des Etats
            oldKBState = Keyboard.GetState();

            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("SceneGameOver.Unload");
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            // Sauvegarde le nouvel état du clavier
            KeyboardState newKBState = Keyboard.GetState();

            // Si la touche R on recharge le Menu 
            if ((newKBState.IsKeyDown(Keys.R) && !oldKBState.IsKeyDown(Keys.R)))
            {
                Debug.WriteLine("Rechargement du Gameplay");
                mainGame.gameState.ChangeScene(GameState.SceneType.Menu);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
         
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "Game Over !", new Vector2((Screen.Width/2) - 60, Screen.Height / 2), Color.Red);
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "\n Click on \"R\" key to restart Game", new Vector2((Screen.Width / 2) - 200, Screen.Height / 2), Color.White);

            base.Draw(gameTime);
        }
    }
}
