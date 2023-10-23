using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

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
        private List<Scene> scenes = new List<Scene>();
        private Scene currentScene;

        private GameWindow gameWindow;
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;

        private XmlElement scenesNode;

        public SceneManager(GameWindow gameWindow, ContentManager contentManager, GraphicsDevice graphicsDevice) 
        {
            this.gameWindow = gameWindow;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
        }

        public void Init()
        {
            XmlDocument doc = SceneLoader.LoadAll();
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

            XmlNodeList? uiNodes = sceneNode.SelectNodes("descendant::tg:ui_object", nsmgr);
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

        public void LoadSceneByName(string name) 
        {
            Scene? nextScene = scenes.Find(s => s.Name == name);
            if (nextScene != null)
            {
                currentScene.Destroy();
                currentScene = nextScene;

                OnSceneLoaded?.Invoke(this, new OnSceneLoadedArgs(currentScene.Name));
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
            if (currentScene != null) 
                currentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (currentScene != null)
                currentScene.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
