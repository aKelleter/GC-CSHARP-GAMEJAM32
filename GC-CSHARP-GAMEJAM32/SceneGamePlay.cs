﻿using Microsoft.Xna.Framework;
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
        private Tirs MTir;
        private Sprite World;
        private Rectangle Screen;
        private Song music;
        private SoundEffect sndExplode;

        private float elapsedTime;
        public float sequenceFireEnnemy;

       
        public SceneGamePlay(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneGamePlay");
        }

        public override void Load()
        {

            elapsedTime = 0.0f;
            sequenceFireEnnemy = 2.0f; // secondes

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
            World = new Sprite(mainGame.Content.Load<Texture2D>("_Images_/world-background"));
            World.position = new Vector2(1, 1);

            // Création du héro (car)
            MyHero = new Hero(mainGame.Content.Load<Texture2D>("_Images_/hero"));
            MyHero.position = new Vector2((Screen.Width/2) - MyHero.texture.Width/2, (Screen.Height-100) - MyHero.texture.Height/2);
            // Ajouter à la liste de Sprites
            listActors.Add(MyHero);

            for (int i = 0; i < 20; i++)
            {
                // Création des Ennemis 
                // Instanciation de l'objet
                MyEnnemi = new Ennemis(mainGame.Content.Load<Texture2D>("_Images_/ennemis"));

                // Initialisation de la position
                MyEnnemi.position = new Vector2(
                        Tools.RandomInt(230, Screen.Width - MyEnnemi.texture.Width - 230),
                        Tools.RandomInt(50, 65));

                // Ajoute l'ennemi à la liste
                listActors.Add(MyEnnemi);

                MTir = new Tirs(mainGame.Content.Load<Texture2D>("_Images_/tir-e"));
                MTir.position = new Vector2(MyEnnemi.position.X, MyEnnemi.position.Y);
                MTir.Velocity_X = 0;
                MTir.Velocity_Y = 5;
                 
                //Debug.WriteLine("Chronotir IN : " + ennemi.chronotir);
                // Ajoute l'ennemi à la liste
                listTirs.Add(MTir);
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
                if(MyHero.position.X > 220)
                    MyHero.Move(-5, 0); 
            }
            if (newKBState.IsKeyDown(Keys.Right))
            {
                if (MyHero.position.X < Screen.Width - MyHero.texture.Width -220)
                    MyHero.Move(+5, 0);
            }
            if (newKBState.IsKeyDown(Keys.Up))
            {
                if (MyHero.position.Y > 70)
                    MyHero.Move(0, -5);
            }
            if (newKBState.IsKeyDown(Keys.Down))
            {
                if (MyHero.position.Y < Screen.Height - MyHero.texture.Height - 20)
                    MyHero.Move(0, +5);
            }



            // Parcours la boucle des Actors pour gérer les interactions avec les ennemis
            // -------------------------------------------------------------------------
            foreach (IActor actor in listActors)
            {

                // Si l'actor est un ennemi
                if (actor is Ennemis ennemi)
                {
                    elapsedTime += (((float)gameTime.ElapsedGameTime.Milliseconds)/1000)/20;
                    //Debug.WriteLine("Time {0} / Sequence {1} / ElapsedGameTime {2}", elapsedTime, sequenceFireEnnemy, gameTime.ElapsedGameTime.Seconds);

                    if (elapsedTime >= sequenceFireEnnemy)
                    {
                        Debug.WriteLine("Fire !");
                        MTir = new Tirs(mainGame.Content.Load<Texture2D>("_Images_/tir-e"));
                        MTir.position = new Vector2(ennemi.position.X, ennemi.position.Y);
                        MTir.Velocity_X = 0;
                        MTir.Velocity_Y = 5;

                        // Ajoute l'ennemi à la liste
                        listTirs.Add(MTir);
                        // Remise è 0 du temps écoulé
                        elapsedTime = 0.0f;
                    }

                    // Si l'ennemi arrive en bas de l'écran on le repositionne et on l'indique comme à supprimer
                    if (ennemi.position.Y > Screen.Height - ennemi.texture.Height - 25)
                    {
                        ennemi.position = new Vector2(ennemi.position.X, Screen.Height + ennemi.texture.Height);
                        ennemi.ToRemove = true;
                    }                   
                    
                    // Test la collision entre un ennemi et le héro
                    if (Tools.CollideByBox(ennemi, MyHero))
                    {
                        Debug.WriteLine("Ennemy Collision");
                        MyHero.TouchedByActors(ennemi);
                        ennemi.TouchedByActors(MyHero);
                        ennemi.ToRemove = true; 
                        sndExplode.Play();                        
                    }
                }           
            } // end foreach
            // ----------------------------------------------------------------------

            // Parcours de la liste des tirs pour mettre à jour la position / les collisions et les suppressions
            // -------------------------------------------------------------------------------------------------
            foreach (Tirs tir in listTirs)
            {
                // Application du mouvement
                tir.Move(0, tir.Velocity_Y);

                // Si il y a collision avec un tir et le héro
                if (Tools.CollideByBox(tir, MyHero))
                {
                    Debug.WriteLine("Tir Collision");
                    MyHero.TouchedByTirs();
                    tir.TouchedByActors(MyHero);
                    tir.ToRemove = true;
                    sndExplode.Play();
                }
               

                // Si le tir arrive en bas de l'écran
                if (tir.position.Y > Screen.Height)
                {
                    tir.ToRemove = true;
                    Debug.WriteLine("Tir ToRemove");
                }
            }            
            // ----------------------------------------------------------------------------------------------------

            


            
            // Suppression des Sprites marqués comme à supprimer
            // -------------------------------------------------
            CleanActors(); // ou => listActors.RemoveAll(actor => actor.ToRemove == true); 
            CleanTirs();

            // Si le hero n'a plus d'énergie => Game Over
            // ------------------------------------------
            if (MyHero.energy <= 0)
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Gameover);
            }

            // On sauvegarde le nouvel état du clavier dans l'ancien
            oldKBState = newKBState;

            base.Update(gameTime);
        }

        

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Draw(World.texture, new Vector2(World.position.X, World.position.Y), Color.White);
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "Energy:  "+MyHero.energy, new Vector2(50, 20), Color.White);  
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "V " + mainGame.strVersion, new Vector2(Screen.Width-140, Screen.Height - 50), new Color(.2f, .4f, .3f, .1f));
            base.Draw(gameTime);
        }
    }
}
