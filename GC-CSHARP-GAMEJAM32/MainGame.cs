using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    public class MainGame: Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public GameState gameState;
        public String strVersion = "0.0.2";

        public RenderTarget2D render;
        private int TargetWidth = 1024;
        private int TargetHeight = 768;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);    
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
             
            gameState = new GameState(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = TargetWidth;
            _graphics.PreferredBackBufferHeight = TargetHeight;
            _graphics.IsFullScreen = false;

            PresentationParameters pp = _graphics.GraphicsDevice.PresentationParameters;
            render = new RenderTarget2D(_graphics.GraphicsDevice,
                TargetWidth, TargetHeight,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                pp.MultiSampleCount,
                RenderTargetUsage.DiscardContents);

            _graphics.ApplyChanges();

            gameState.ChangeScene(GameState.SceneType.Menu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AssetManager.Load(Content);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            render.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit();
                gameState.ChangeScene(GameState.SceneType.Menu);
            
            // TODO: Add your update logic here
            if (gameState.CurrentScene != null)
            {
                gameState.CurrentScene.Update(gameTime);
            }

            //_graphics.ApplyChanges();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.SetRenderTarget(render);
            GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 1);
            _spriteBatch.Begin();
            
            // TODO: Add your drawing code here
            if (gameState.CurrentScene != null)
            {
                gameState.CurrentScene.Draw(gameTime);
            }

            _spriteBatch.End();
            //GraphicsDevice.SetRenderTarget(null);


            base.Draw(gameTime);
        }
    }
}
