using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace AlphaKilo_GameJam32
{
    public class SceneGamePlay : Scene
    {
        private KeyboardState oldKBState;
        private Hero MyHero;
        private Ennemis MyEnnemi;
        private Sprite World;
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
                //MediaPlayer.Play(music);
                //MediaPlayer.IsRepeating = true;

            // Chargement des sons
            sndExplode = mainGame.Content.Load<SoundEffect>("_Sounds_/explode");

            // Création du background de l'interface Utilisateur
            World = new Sprite(mainGame.Content.Load<Texture2D>("_Images_/ui-background"));
            World.position = new Vector2(1, 1);

            // Création du héro (car)
            MyHero = new Hero(mainGame.Content.Load<Texture2D>("_Images_/car"));
            MyHero.position = new Vector2((Screen.Width/2) - MyHero.texture.Width/2, (Screen.Height-100) - MyHero.texture.Height/2);
            // Ajouter à la liste de Sprites
            listActors.Add(MyHero);

            for (int i = 0; i < 20; i++)
            {
                // Création des Ennemis 
                // Instanciation de l'objet
                MyEnnemi = new Ennemis(mainGame.Content.Load<Texture2D>("_Images_/Ennemis"));

                // Initialisation de la position
                MyEnnemi.position = new Vector2(
                        Tools.RandomInt(230, Screen.Width - MyEnnemi.texture.Width - 230),
                        Tools.RandomInt(50, 50));
                // Ajoute l'ennemi à la liste
                listActors.Add(MyEnnemi);
            }


            

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

            // Contrôler les mouvements de MyHero
            if(newKBState.IsKeyDown(Keys.Left))
            {
                if(MyHero.position.X > 0)
                    MyHero.Move(-5, 0); 
            }
            if (newKBState.IsKeyDown(Keys.Right))
            {
                if (MyHero.position.X < Screen.Width - MyHero.texture.Width)
                    MyHero.Move(+5, 0);
            }
            if (newKBState.IsKeyDown(Keys.Up))
            {
                if (MyHero.position.Y > 0)
                    MyHero.Move(0, -5);
            }
            if (newKBState.IsKeyDown(Keys.Down))
            {
                if (MyHero.position.Y < Screen.Height - MyHero.texture.Height)
                    MyHero.Move(0, +5);
            }

            // Ajoute des ennemis
           

            if (MyHero.energy > 30)
            {
                                
                

                
            }

            // Empêche les Ennemis de l'aire de jeu
            foreach (IActor actor in listActors)
            {

                //Debug.WriteLine("Type {0}", item.GetType());
               
                if (actor is Ennemis m)
                {
                    /*
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
                    */

                    if (m.position.Y > Screen.Height - m.texture.Height - 25)
                    {
                        //m.Velocity_Y = 0 - m.Velocity_Y;
                        m.position = new Vector2(m.position.X, Screen.Height + m.texture.Height);

                    }

                    if (m.position.Y < 50)
                    {
                        m.Velocity_Y = 0 - m.Velocity_Y;
                        m.position = new Vector2(m.position.X, 0);
                    }

                    if (Tools.CollideByBox(m, MyHero))
                    {
                        MyHero.TouchedBy(m);
                        m.TouchedBy(MyHero);
                        m.ToRemove = true;
                        sndExplode.Play();
                        
                    }                   
                }

                if (MyHero.energy <= 0)
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

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Debug.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Draw(World.texture, new Vector2(World.position.X, World.position.Y), Color.White);
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "Energy:  "+MyHero.energy, new Vector2(50, 20), Color.White);            
            base.Draw(gameTime);
        }
    }
}
