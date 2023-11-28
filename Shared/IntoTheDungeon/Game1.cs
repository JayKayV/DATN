using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedLibrary.Scene;
using IntoTheDungeon.Scenes;
using SharedLibrary.Input;

namespace IntoTheDungeon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SceneManager _sceneManager;

        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;

        private const int DESIGNED_RESOLUTION_WIDTH = 1280;
        private const int DESIGNED_RESOLUTION_HEIGHT = 720;
        private const float DESIGNED_RESOLUTION_ASPECT_RATIO = DESIGNED_RESOLUTION_WIDTH / (float)DESIGNED_RESOLUTION_HEIGHT;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; 
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Into the dungeon";
            _graphics.PreferredBackBufferHeight = DESIGNED_RESOLUTION_HEIGHT;
            _graphics.PreferredBackBufferWidth = DESIGNED_RESOLUTION_WIDTH;
            _graphics.ApplyChanges();

            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice,
                        DESIGNED_RESOLUTION_WIDTH, 
                        DESIGNED_RESOLUTION_HEIGHT,
                        false,
                        SurfaceFormat.Color, 
                        DepthFormat.None, 
                        0,
                        RenderTargetUsage.DiscardContents);
            _renderScaleRectangle = GetScaleRectangle();

            _sceneManager = new SceneManager(this.Window, this.Content, this.GraphicsDevice);
            _sceneManager.Init();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _sceneManager.Load();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseHandler.Update();
            KeyboardHandler.Update();
            _sceneManager.Update(gameTime);

            if (_sceneManager.Quit)
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _sceneManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        private Rectangle GetScaleRectangle()
        {
            float variance = 0.5f;
            float actualAspectRatio = Window.ClientBounds.Width / (float) Window.ClientBounds.Height;
            Rectangle scaleRectangle;

            if (actualAspectRatio <= DESIGNED_RESOLUTION_ASPECT_RATIO)
            {
                int presentHeight = (int)(Window.ClientBounds.Width / DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
                int barHeight = (Window.ClientBounds.Height - presentHeight) / 2;
                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                int presentWidth = (int)(Window.ClientBounds.Height * DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
                int barWidth = (Window.ClientBounds.Width - presentWidth) / 2;
                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }
            return scaleRectangle;
        }
    }
}
