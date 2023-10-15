using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SharedLibrary.Scene
{
    public class OnSceneLoadedArgs : System.EventArgs
    {
        public string Name { get; set; }
        public OnSceneLoadedArgs(string name) { this.Name = name; }
    }

    //this class must be instatiated only once!
    public class SceneManager
    {
        private List<BaseScene> scenes = new List<BaseScene>();
        private BaseScene currentScene;

        private GameWindow gameWindow;
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;

        public SceneManager(GameWindow gameWindow, ContentManager contentManager, GraphicsDevice graphicsDevice) 
        {
            this.gameWindow = gameWindow;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
        }

        public SceneManager(IEnumerable<BaseScene> scenes, GameWindow gameWindow, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            foreach (BaseScene baseScene in scenes)
                this.scenes.Add(baseScene);

            this.gameWindow = gameWindow;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
        }

        public event EventHandler<OnSceneLoadedArgs>? OnSceneLoaded;

        public void LoadSceneByName(string name) 
        { 
            foreach (BaseScene scene in scenes)
            {
                if (scene.Name == name)
                {
                    currentScene = scene;
                    currentScene.LoadContent(contentManager, graphicsDevice);

                    OnSceneLoaded?.Invoke(this, new OnSceneLoadedArgs(currentScene.Name));
                    break;
                }
            }
        }

        public void AddScene(BaseScene scene)
        {
            scene.SetSceneManager(this);
            scene.SetWindow(this.gameWindow);
            scenes.Add(scene);
        }

        public void RemoveScene(BaseScene scene)
        {
            scenes.Remove(scene);
        }

        public void Update(GameTime gameTime)
        {
            currentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            currentScene.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
