using SharedLibrary.BaseGameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SharedLibrary.Scene
{
    public abstract class BaseScene
    {
        protected string name;
        protected SceneManager sceneManager; //must be registered by SceneManager.AddScene(scene)
        protected GameWindow gameWindow; //must be registered by SceneManager.AddScene(scene)

        protected GameObjectManager manager;
        public string Name { get { return name; } }
        public BaseScene(string name)
        {
            this.name = name;
            this.sceneManager = null;
            this.manager = new GameObjectManager(this);
        }
        public BaseScene(string name, SceneManager sceneManager)
        {
            this.name = name;
            this.sceneManager = sceneManager;
            this.manager = new GameObjectManager(this);
        }

        public abstract void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Destroy();

        public SceneManager GetSceneManager()
        {
            return sceneManager;
        }
        public void SetSceneManager(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public void SetWindow(GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;
        }

        public GameObjectManager GetObjectManager()
        {
            return manager;
        }
    }
}
