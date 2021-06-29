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
    public class SceneGamePlay : SceneManager
    {
        private KeyboardState oldKBState;
        private Hero MyHero;
        private Ennemis MyEnnemi;
        private Tirs MTir;
        private Sprite World;
        private Rectangle Screen;
        private Song music;
        private SoundEffect sndExplode;
        private int CountEnnemis;
        private int CounterEnnemis;
        private int generateNbrEnnemies;

        private float elapsedTime;
        public float sequenceFireEnnemy;

       
        public SceneGamePlay(MainGame pGame) : base(pGame)
        {
            //Debug.WriteLine("New SceneGamePlay");
        }

        public override void Load()
        {

            // Initialisations
            elapsedTime = 0.0f;
            sequenceFireEnnemy = 1.0f; // secondes
            CountEnnemis = 0;
            CounterEnnemis = 0;
            generateNbrEnnemies = 135;

            //Debug.WriteLine("SceneGamePlay.Load");
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
            listSprites.Add(MyHero);

            // Création des Ennemis
            // ----------------------------------------------------------------------------------------------------
            for (int i = 0; i <= generateNbrEnnemies; i++)
            {
                // Création des Ennemis
                // ------------------------------------------------------------------------------------------------
                MyEnnemi = new Ennemis(mainGame.Content.Load<Texture2D>("_Images_/ennemis"));

                // Initialisation de la position
                MyEnnemi.position = new Vector2(
                        Tools.RandomInt(230, Screen.Width - MyEnnemi.texture.Width - 230),
                        Tools.RandomInt(50, 70));

                // Ajoute l'ennemi à la liste
                listSprites.Add(MyEnnemi);

                // Lance une salve de tir pour chacun des Ennemis
                // ------------------------------------------------------------------------------------------------
                MTir = new Tirs(mainGame.Content.Load<Texture2D>("_Images_/tir-e"));
                MTir.position = new Vector2(MyEnnemi.position.X, MyEnnemi.position.Y + MyEnnemi.texture.Height + 1);
                //Debug.WriteLine("Ennemi pos x {0} - Ennemi pos y {1} - Pos y tir {2} -", MyEnnemi.position.X, MyEnnemi.position.Y, MyEnnemi.position.Y + MyEnnemi.texture.Height + 5);
                MTir.Velocity_X = 0;
                MTir.Velocity_Y = 5;
                 
                // Ajoute l'ennemi à la liste
                listTirs.Add(MTir);
                
            }

            // Chargement du Load() de Scene
            base.Load();
        }

        public override void UnLoad()
        {
            //Debug.WriteLine("SceneGamePlay.Unload");
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
                //Debug.WriteLine("LeftControl Key is down = Acceleration");
            }

            // Si la touche M est enfoncée et qu'elle ne l'était pas avant on charge le Menu
            if (newKBState.IsKeyDown(Keys.M) && !oldKBState.IsKeyDown(Keys.M))
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Menu);
            }

            // Si la touche R on recharge le Gameplay (jeu)
            if ((newKBState.IsKeyDown(Keys.R) && !oldKBState.IsKeyDown(Keys.R)))
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);
            }

            // Contrôler les mouvements du héro
            if (newKBState.IsKeyDown(Keys.Left))
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

            if (newKBState.IsKeyDown(Keys.Space) && !oldKBState.IsKeyDown(Keys.Space))
            {
                MTir = new Tirs(mainGame.Content.Load<Texture2D>("_Images_/tir-e"));
                MTir.position = new Vector2(MyHero.position.X, MyHero.position.Y - MyHero.texture.Height);
                MTir.Velocity_X = 0;
                MTir.Velocity_Y = -5;

                // Ajoute l'ennemi à la liste
                listTirs.Add(MTir);               
            }

            // Parcours la boucle des Actors pour gérer les interactions avec les ennemis
            // ----------------------------------------------------------------------------------------------------
            foreach (ISpriteManager sprite in listSprites)
            {
                // Si le sprite est un ennemi
                if (sprite is Ennemis ennemi)
                {
                    elapsedTime += (((float)gameTime.ElapsedGameTime.Milliseconds)/1000)/20;
                    //Debug.WriteLine("Time {0} / Sequence {1} / ElapsedGameTime {2}", elapsedTime, sequenceFireEnnemy, gameTime.ElapsedGameTime.Seconds);

                    // Si le temps écoulé est supérieur à la séquence de mise à feu
                    // On Crée un tir avec ml'ennemi en cours dans la boucle
                    if (elapsedTime >= sequenceFireEnnemy)
                    {
                        //Debug.WriteLine("Fire !");
                        MTir = new Tirs(mainGame.Content.Load<Texture2D>("_Images_/tir-e"));
                        MTir.position = new Vector2(ennemi.position.X, ennemi.position.Y + ennemi.texture.Height + 1);
                        MTir.Velocity_X = 0;
                        MTir.Velocity_Y = 5;

                        // Ajoute l'ennemi à la liste
                        listTirs.Add(MTir);

                        // Applique la vélocité au tir
                        MTir.Move(0, MTir.Velocity_Y);

                        // Remise è 0 du temps écoulé
                        elapsedTime = 0.0f;
                    }

                    // Parcours de la liste des tirs pour mettre à jour la position / les collisions et les suppressions
                    // ----------------------------------------------------------------------------------------------------
                    foreach (Tirs tir in listTirs)
                    {
                        // Si il y a collision avec un tir et le héro
                        if (Tools.CollideByBox(tir, MyHero))
                        {
                            //Debug.WriteLine("Collision Tir/Hero");                    
                            if(tir.Energy > 0)
                            {
                                tir.DamageOnSprite(MyHero);
                                sndExplode.Play();
                            }                            
                            tir.ToRemove = true;
                            tir.Energy = 0;                            
                        }

                        if (Tools.CollideByBox(ennemi, tir))
                        {
                            //Debug.WriteLine("Collision Tir/ennemi");
                            if (tir.Energy > 0)
                            {
                                tir.DamageOnSprite(ennemi);
                                sndExplode.Play();
                            }
                            tir.ToRemove = true;
                            tir.Energy = 0;                                                 
                            ennemi.ToRemove = true;                                                        
                        }

                        // Si le tir arrive en bas de l'écran
                        if (tir.position.Y > Screen.Height)
                        {
                            tir.ToRemove = true;
                            //Debug.WriteLine("Tir ToRemove");
                        }
                    }
                    // ----------------------------------------------------------------------------------------------------
                   
                    // Test la collision entre un ennemi et le héro
                    if (Tools.CollideByBox(ennemi, MyHero))
                    {
                        //Debug.WriteLine("Ennemy Collision");
                        MyHero.DamageOnSprite(ennemi);
                        ennemi.DamageOnSprite(MyHero);
                        ennemi.ToRemove = true; 
                        sndExplode.Play();                        
                    }

                    // Si l'ennemi arrive en bas de l'écran on le repositionne et on l'indique comme à supprimer
                    if (ennemi.position.Y > Screen.Height - ennemi.texture.Height - 25)
                    {
                        //ennemi.position = new Vector2(ennemi.position.X, Screen.Height + ennemi.texture.Height);
                        ennemi.ToRemove = true;
                        // Crée un dégât au héro
                        ennemi.DamageOnSprite(MyHero);
                    }
                }               
            } // end foreach

            // Suppression des Sprites marqués comme à supprimer
            // ----------------------------------------------------------------------------------------------------
            CleanSprites(); // ou => listSprites.RemoveAll(sprite => sprite.ToRemove == true); 
            // ----------------------------------------------------------------------------------------------------          

                                    
            // Suppression des Tirs marqués comme à supprimer
            // ----------------------------------------------------------------------------------------------------            
            CleanTirs();
            // ----------------------------------------------------------------------------------------------------


            // FIN DU JEU -----------------------------------------------------------------------------------------
            // ----------------------------------------------------------------------------------------------------

            // Si le hero n'a plus d'énergie => Game Over
            // ----------------------------------------------------------------------------------------------------
            if (MyHero.Energy <= 0)
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Gameover);
            }

            // Si il n'y a plus d'ennemis => Game Win
            // ----------------------------------------------------------------------------------------------------
            foreach (ISpriteManager sprite in listSprites)
            {
                // Si le sprite est un ennemi
                if (sprite is Ennemis itemEnnemi)
                {
                    CountEnnemis++;
                    //Debug.WriteLine("Add ennemi to count");
                }                
            }

            // On enregistre le nbre d'ennemis dans compteur
            CounterEnnemis = CountEnnemis;
            CountEnnemis = 0;
            
            //Debug.WriteLine("Count Ennemi " + CounterEnnemis);

            // Si il n'y a plus d'ennemis on redirige vers la page de victoire
            if (CounterEnnemis <= 0)
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Gamewin);
            }
            // --------------------------------------------------------------------------------------------------


            // On sauvegarde le nouvel état du clavier dans l'ancien
            // --------------------------------------------------------------------------------------------------
            oldKBState = newKBState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Draw(World.texture, new Vector2(World.position.X, World.position.Y), Color.White);
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "Energy:  "+MyHero.Energy, new Vector2(50, 20), Color.White);  
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "Enemies:  "+ CounterEnnemis, new Vector2(50, 50), Color.GreenYellow);  
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "V " + mainGame.strVersion, new Vector2(Screen.Width-140, Screen.Height - 50), new Color(.2f, .4f, .3f, .1f));
            base.Draw(gameTime);
        }
    }
}
