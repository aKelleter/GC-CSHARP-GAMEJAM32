using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    public class GameState
    {
        public enum SceneType
        {
            Menu,
            Gameplay,
            Gameover,
            Gamewin
        }

        protected MainGame mainGame;
        public SceneManager CurrentScene { get; set; }
        public GameState(MainGame pGame)
        {
            mainGame = pGame;
        }

        public void ChangeScene(SceneType pSceneType)
        {
            // Déchargement et réinitialisation de la CurrentScene si existante
            if(CurrentScene != null)
            {
                CurrentScene.UnLoad();
                CurrentScene = null;  
            }

            // Instanciation de la scène à charger dans CurrentScene
            switch (pSceneType)
            {
                case SceneType.Menu:
                    CurrentScene = new SceneMenu(mainGame);
                    break;
                case SceneType.Gameplay:
                    CurrentScene = new SceneGamePlay(mainGame);
                    break;
                case SceneType.Gameover:
                    CurrentScene = new SceneGameOver(mainGame);
                    break;
                case SceneType.Gamewin:
                    CurrentScene = new SceneGameWin(mainGame);
                    break;
                default:
                    break;
            } 
            
            // Chargement de la CurrentScene
            CurrentScene.Load();

        }


    }
}
