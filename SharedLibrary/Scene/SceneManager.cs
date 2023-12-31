using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.BaseGameObject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using SharedLibrary.Event;

namespace SharedLibrary.Scene
{
    public class OnSceneLoadedArgs : System.EventArgs
    {
        public string OldScene {  get; set; }
        public string NewScene { get; set; }
        public OnSceneLoadedArgs(string oldScene, string newScene) {
            OldScene = oldScene;
            NewScene = newScene;
        }
    }

    //this class must be instatiated only once!
    public class SceneManager
    {
        private List<Scene> scenes = new List<Scene>();
        private Scene currentScene;

        private GameWindow gameWindow;
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private GObjectStorage objectStorage;
        private EventBus eventBus;

        private XmlElement scenesNode;

        public bool ContinueUpdate { get; set; } = true;
        public bool Quit { get; set; } = false;

        public SceneManager(GameWindow gameWindow, ContentManager contentManager, GraphicsDevice graphicsDevice) 
        {
            this.gameWindow = gameWindow;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.objectStorage = new GObjectStorage();
            this.eventBus = new EventBus();
        }

        public void Init()
        {
            XmlDocument doc = SceneLoader.LoadAllFromEntry();
            if (doc != null)
            {
                scenesNode = doc.DocumentElement;
                if (scenesNode.ChildNodes.Count > 0)
                {
                    foreach (XmlNode sceneNode in scenesNode.ChildNodes)
                    {
                        Scene scene = new Scene(sceneNode.Attributes.GetNamedItem("name").Value);
                        AddScene(scene);
                    }
                    currentScene = scenes[0];
                }
            }
        }

        public void Load()
        {
            string currentSceneName = currentScene.Name;

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(scenesNode.OwnerDocument.NameTable);
            nsmgr.AddNamespace("tg", "http://testgame.com");

            XmlNode? sceneNode = scenesNode.SelectSingleNode($"descendant::tg:scene[@name=\'{currentSceneName}\']", nsmgr);
            if (sceneNode ==  null)
                throw new AccessViolationException("Scene information deleted while reading...");

            XmlNodeList? uiNodes = sceneNode.SelectNodes("tg:ui_object", nsmgr);
            XmlNodeList? scriptNodes = sceneNode.SelectNodes("descendant::tg:script", nsmgr);

            if (uiNodes != null)
                currentScene.LoadUiContent(uiNodes, contentManager, graphicsDevice);
            if (scriptNodes != null)
                currentScene.LoadScripts(scriptNodes, contentManager, graphicsDevice);
        }

        public SceneManager(IEnumerable<Scene> scenes, GameWindow gameWindow, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.scenes.AddRange(scenes);

            this.gameWindow = gameWindow;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
        }

        public event EventHandler<OnSceneLoadedArgs>? OnSceneLoaded;
        public event EventHandler<OnSceneLoadedArgs>? OnSceneDestroy;

        public void LoadSceneByName(string name) 
        {
            Scene? nextScene = scenes.Find(s => s.Name == name);
            if (nextScene != null)
            {
                string oldName = currentScene.Name;
                currentScene.Destroy();
                OnSceneDestroy?.Invoke(this, new OnSceneLoadedArgs("", oldName));
                currentScene = nextScene;

                ContinueUpdate = false;
                Load();
                OnSceneLoaded?.Invoke(this, new OnSceneLoadedArgs(oldName, currentScene.Name));
            }
            else
                throw new ArgumentException(string.Format("No scene named {0} in gameObjectManager")); 
        }

        public void AddScene(Scene scene)
        {
            scene.SetSceneManager(this);
            scene.SetWindow(this.gameWindow);
            scenes.Add(scene);
        }

        public void RemoveScene(Scene scene)
        {
            scenes.Remove(scene);
        }

        public void Update(GameTime gameTime)
        {
            if (!ContinueUpdate) 
                ContinueUpdate = true;
            if (currentScene != null) 
                currentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteSortMode can cause trouble
            spriteBatch.Begin();

            if (currentScene != null)
                currentScene.Draw(spriteBatch);

            spriteBatch.End();
        }
        public ContentManager ContentManager { get => contentManager; }
        public GraphicsDevice GraphicsDevice { get => graphicsDevice; }
        public GObjectStorage ObjectStorage { get => objectStorage; }
        public EventBus EventBus { get => eventBus; }

        public void QuitGame()
        {
            Quit = true;
            ContinueUpdate = false;

            scenes.Clear();
            objectStorage.Clear();
        }
    }
}
