using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlphaKilo_GameJam32
{
    public class SceneGamePlay : Scene
    {
        private KeyboardState oldKBState;
        private Hero MyShip;
        private Meteor meteor;
        private Rectangle Screen;
        private Song music;
        private SoundEffect sndExplode;
        public SceneGamePlay(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneGamePlay");
            
        }

        public override void Load()
        {
            Debug.WriteLine("SceneGamePlay.Load");
            oldKBState = Keyboard.GetState();

            // Taille de l'écran
            Screen = mainGame.Window.ClientBounds;

            // Chargement de la musisque
            music = AssetManager.musicGamePlay;
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

            // Chargement des sons
            sndExplode = mainGame.Content.Load<SoundEffect>("_Sounds_/explode");

            // Création d'une liste de Météors
            for (int i = 0 ; i < 20 ; i++)
            {
                // Instanciation de l'objet
                meteor = new Meteor(mainGame.Content.Load<Texture2D>("_Images_/meteor"));

                // Initialisation de la position
                meteor.position = new Vector2(
                    Tools.RandomInt(1 , Screen.Width - meteor.texture.Width),
                    Tools.RandomInt(1, Screen.Height - meteor.texture.Height));

                // Ajouter à la liste de Sprites
                listActors.Add(meteor);
            }

            // Création du héro (vaisseau / ship)
            MyShip = new Hero(mainGame.Content.Load<Texture2D>("_Images_/ship"));
            MyShip.position = new Vector2((Screen.Width/2) - MyShip.texture.Width/2, (Screen.Height/2) - MyShip.texture.Height/2);

            // Ajouter à la liste de Sprites
            listActors.Add(MyShip);

            // Chargement du Load() de Scene
            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("SceneGamePlay.Unload");
            MediaPlayer.Stop();
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            // Sauvegarde le nouvel état du clavier
            KeyboardState newKBState = Keyboard.GetState();

            // Si la touche LEFTCONTROL est enfoncée on Accélère
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                Debug.WriteLine("LeftControl Key is down = Acceleration");
            }

            // Si la touche M est enfoncée et qu'elle ne l'était pas avant on charge le Menu
            if (newKBState.IsKeyDown(Keys.M) && !oldKBState.IsKeyDown(Keys.M))
            {
                Debug.WriteLine("M Key is down, back to Menu");
                mainGame.gameState.ChangeScene(GameState.SceneType.Menu);
            }

            // Contrôler les mouvements de MyShip
            if(newKBState.IsKeyDown(Keys.Left))
            {
                if(MyShip.position.X > 0)
                    MyShip.Move(-5, 0); 
            }
            if (newKBState.IsKeyDown(Keys.Right))
            {
                if (MyShip.position.X < Screen.Width - MyShip.texture.Width)
                    MyShip.Move(+5, 0);
            }
            if (newKBState.IsKeyDown(Keys.Up))
            {
                if (MyShip.position.Y > 0)
                    MyShip.Move(0, -5);
            }
            if (newKBState.IsKeyDown(Keys.Down))
            {
                if (MyShip.position.Y < Screen.Height - MyShip.texture.Height)
                    MyShip.Move(0, +5);
            }

            // Empêche les Météors de sortir du jeu
            foreach (IActor actor in listActors)
            {

                //Debug.WriteLine("Type {0}", item.GetType());
               
                if (actor is Meteor m)
                {
                    if (m.position.X > Screen.Width - m.texture.Width)
                    {
                        m.Velocity_X = 0 - m.Velocity_X;
                        m.position = new Vector2(Screen.Width - m.texture.Width, m.position.Y); // repostionnement du météor pour éviter des bugs d'affichage
                    }   

                    if (m.position.X < 0)
                    {
                        m.Velocity_X = 0 - m.Velocity_X;
                        m.position = new Vector2(0, m.position.Y);
                    }

                    if (m.position.Y > Screen.Height - m.texture.Height)
                    {
                        m.Velocity_Y = 0 - m.Velocity_Y;
                        m.position = new Vector2(m.position.X, Screen.Height - m.texture.Height);

                    }

                    if (m.position.Y < 0)
                    {
                        m.Velocity_Y = 0 - m.Velocity_Y;
                        m.position = new Vector2(m.position.X, 0);
                    }

                    if (Tools.CollideByBox(m, MyShip))
                    {
                        MyShip.TouchedBy(m);
                        m.TouchedBy(MyShip);
                        m.ToRemove = true;
                        sndExplode.Play();
                        
                    }                   
                }

                if (MyShip.energy <= 0)
                {
                    mainGame.gameState.ChangeScene(GameState.SceneType.Gameover);
                }

            } // end foreach

            // Supprime dans la liste d'Actors les item qui ont la propriété ToRemove à true
            // listActors.RemoveAll(item => item.ToRemove == true);
            // OU
            CleanActors();




            // On sauvegarde le nouvel état du clavier dans l'ancien
            oldKBState = newKBState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "AlphaKilo Template - Energy:  "+MyShip.energy, new Vector2((Screen.Width/2) - 200, 20), Color.White);

            base.Draw(gameTime);
        }
    }
}
