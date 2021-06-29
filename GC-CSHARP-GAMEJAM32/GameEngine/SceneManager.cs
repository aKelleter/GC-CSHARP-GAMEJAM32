using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    abstract public class SceneManager
    {
        protected MainGame mainGame;
        protected List<ISpriteManager> listSprites;
        protected List<ISpriteManager> listTirs;

        public SceneManager (MainGame pGame)
        {
            mainGame = pGame;
            listSprites = new List<ISpriteManager>();
            listTirs = new List<ISpriteManager>();
        }

        public virtual void Load()
        {

        }

        public virtual void UnLoad()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (ISpriteManager actor in listSprites)
            {
                actor.Update(gameTime);
            }

            foreach (Tirs tir in listTirs)
            {
                tir.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (ISpriteManager actor in listSprites)
            {
                actor.Draw(mainGame._spriteBatch);
            }

            foreach (Tirs tir in listTirs)
            {
                tir.Draw(mainGame._spriteBatch);                                   
            }
        }

        public void CleanSprites()
        {
            listSprites.RemoveAll(item => item.ToRemove == true);
        }

        public void CleanTirs()
        {
            listTirs.RemoveAll(item => item.ToRemove == true);
        }
    }
}
