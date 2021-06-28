using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    abstract public class Scene
    {
        protected MainGame mainGame;
        protected List<IActor> listActors;
        protected List<IActor> listTirs;

        public Scene (MainGame pGame)
        {
            mainGame = pGame;
            listActors = new List<IActor>();
            listTirs = new List<IActor>();
        }

        public virtual void Load()
        {

        }

        public virtual void UnLoad()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IActor actor in listActors)
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
            foreach (IActor actor in listActors)
            {
                actor.Draw(mainGame._spriteBatch);
            }

            foreach (Tirs tir in listTirs)
            {
                tir.Draw(mainGame._spriteBatch);                                   
            }
        }

        public void CleanActors()
        {
            listActors.RemoveAll(item => item.ToRemove == true);
        }

        public void CleanTirs()
        {
            listTirs.RemoveAll(item => item.ToRemove == true);
        }
    }
}
