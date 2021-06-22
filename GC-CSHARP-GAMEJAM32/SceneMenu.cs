using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlphaKilo_GameJam32
{
    public class SceneMenu : Scene
    {

        private KeyboardState oldKBState;
        private GamePadState oldGPADState;
        private Button buttonGamePlay;
        private Sprite MenuBackgr;
        private int ScreenWidth, ScreenHeight;
        private Song music;

        public SceneMenu(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneMenu");
        }

        public void onCLickPlay(Button pSender)
        {
            mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay); 
        }

        public override void Load()
        {
            Debug.WriteLine("SceneMenu.Load");

            // Chargement de la musique
            music = mainGame.Content.Load<Song>("_Sounds_/cool");
                //MediaPlayer.Play(music);
                //MediaPlayer.IsRepeating = true;

            // Sauvegardes des Etats
            oldKBState = Keyboard.GetState();
            oldGPADState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);

            // Taille de l'écran
            ScreenWidth = Tools.ScreenSize(mainGame.Window.ClientBounds, Tools.SizeScreenType.Width);
            ScreenHeight = Tools.ScreenSize(mainGame.Window.ClientBounds, Tools.SizeScreenType.Height);

            // Création du background de l'interface Utilisateur
            MenuBackgr = new Sprite(mainGame.Content.Load<Texture2D>("_Images_/menu-background"));
            MenuBackgr.position = new Vector2(1, 1);

            // Initialisation du bouton de GamePlay
            buttonGamePlay = new Button(mainGame.Content.Load<Texture2D>("_Images_/btn-play"));

            // Positionnement du bouton
            buttonGamePlay.position = new Vector2(50, (ScreenHeight/2) - buttonGamePlay.texture.Height/2);
            
            // Action sur le click du bouton
            buttonGamePlay.onClick = onCLickPlay;

            // Ajout du bouton à la liste des Sprites
            listActors.Add(buttonGamePlay);

            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("SceneMenu.Unload");
            MediaPlayer.Stop();
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            // Sauvegarde le nouvel état du clavier
            KeyboardState newKBState = Keyboard.GetState();

            // Permet d'accéder aux fonctionnalités du Pad et entre autre permettra de vérifier si un pad est branché sur le port one
            GamePadCapabilities padCapabilities = GamePad.GetCapabilities(PlayerIndex.One);

            // Déclare un objet de type: GamePadState pour sauvegarder le nouvel état du Pad
            GamePadState newGamePadState;

            // Sauvegarde l'état de la souris
            MouseState newMSState = Mouse.GetState();

            // Test souris
            if (newMSState.LeftButton == ButtonState.Pressed)
                Debug.WriteLine("Bouton gauche de la souris enfoncé");


            // Sauvegarde l'état du bouton A du Pad
            bool bButtonA = false;

            // Si le Pad est connecté
            if (padCapabilities.IsConnected)
            {
                // Initialise l'état newGamePadState pour sauvegarder le nouvel état du Pad
                newGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);

                // SI le bouton A est enfoncé et qu'il ne l'était pas avant
                if (newGamePadState.IsButtonDown(Buttons.A) && !oldGPADState.IsButtonDown(Buttons.A))
                {
                    bButtonA = true;
                }
            }
            else
                newGamePadState = GamePadState.Default;

            // Si la touche ENTER ou le bouton A du Pad est enfoncé on charge le Gameplay (jeu)
            if ((newKBState.IsKeyDown(Keys.Enter) && !oldKBState.IsKeyDown(Keys.Enter)) || bButtonA)
            {
                Debug.WriteLine("Chargement du Gameplay");
                mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);
            }

            // Si la touche ESCAPE est enfoncée sort du jeu
            if (newKBState.IsKeyDown(Keys.Escape))
            {
                Debug.WriteLine("Quit Game");
                mainGame.Exit();
            }

            // On sauvegarde le nouvel état du clavier dans l'ancien
            oldKBState = newKBState;
            
            // On sauvegarde le nouvel état du Pad dans l'ancien
            oldGPADState = newGamePadState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Draw(MenuBackgr.texture, new Vector2(MenuBackgr.position.X, MenuBackgr.position.Y), Color.White);
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "Or press Enter key", new Vector2(70, ScreenHeight / 2 + 20), new Color(.5f, .7f, .1f, 1.0f));
            mainGame._spriteBatch.DrawString(AssetManager.mainFont, "V " + mainGame.strVersion, new Vector2(ScreenWidth-100, ScreenHeight - 50), new Color(.2f, .3f, .4f, 1.0f));
            base.Draw(gameTime);
           
        }
    }
}
